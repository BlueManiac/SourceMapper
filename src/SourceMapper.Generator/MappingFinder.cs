using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SourceMapper.Generator.Models;
using System.Collections.Generic;
using System.Linq;

namespace SourceMapper.Generator;

public class MappingFinder : ISyntaxContextReceiver
{
    public List<MapperRecord> Maps { get; } = new();

    public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
    {
        if (context.Node is not ClassDeclarationSyntax syntax)
            return;

        if (context.SemanticModel.GetDeclaredSymbol(syntax) is not INamedTypeSymbol to)
            return;

        var attributeNames = syntax.AttributeLists
            .SelectMany(attributeList => attributeList.Attributes)
            .Select(attribute => attribute.Name)
            .OfType<GenericNameSyntax>();

        var attribute = attributeNames.FirstOrDefault(x => x.Identifier.ValueText == "Map");

        if (attribute is null)
            return;

        //Debugger.Launch();

        var typeArguments = attribute.TypeArgumentList.Arguments;

        if (typeArguments.Count == 0)
            return;

        var symbol = context.SemanticModel.GetSymbolInfo(typeArguments[0]).Symbol;

        if (symbol is null)
            return;

        var from = (INamedTypeSymbol)symbol;

        if (syntax.Parent is not BaseNamespaceDeclarationSyntax namespaceSyntax)
            return;

        var memberExpressions = ParseConfigure(to).ToDictionary(x => x.Target);

        Maps.Add(new MapperRecord
        {
            Namespace = namespaceSyntax.Name.ToString(),
            ClassName = syntax.Identifier.ToString(),
            FullClassName = namespaceSyntax.Name + "." + syntax.Identifier,
            From = from.ToString(),
            To = to.ToString(),
            ToName = to.Name,
            Properties = ParseMembers(from, to, memberExpressions).ToList()
        });
    }

    private IEnumerable<IMemberRecord> ParseMembers(INamedTypeSymbol from, INamedTypeSymbol to, Dictionary<string, MemberExpressionRecord> memberExpressions)
    {
        var fromProperties = from.GetMembers().Where(x => x.Kind == SymbolKind.Property).OfType<IPropertySymbol>().ToList();
        var toProperties = to.GetMembers().Where(x => x.Kind == SymbolKind.Property).OfType<IPropertySymbol>();

        foreach (var property in toProperties)
        {
            // Custom mapping
            if (memberExpressions.TryGetValue(property.Name, out var memberExpression))
            {
                memberExpression.Type = property.Type.Name;

                yield return memberExpression;

                continue;
            }

            // Basic mapping
            {
                if (fromProperties.Find(x => x.Name == property.Name) is IPropertySymbol symbol)
                {
                    yield return new MemberRecord
                    {
                        Target = property.Name,
                        Source = property.Name,
                        Type = property.Type.Name
                    };

                    continue;
                }
            }

            // Flattening
            {
                if (fromProperties.Find(x => property.Name.StartsWith(x.Name)) is IPropertySymbol symbol)
                {
                    var innerName = property.Name.Substring(symbol.Name.Length);
                    var innerProperties = symbol.Type.GetMembers().Where(x => x.Kind == SymbolKind.Property).OfType<IPropertySymbol>();

                    if (innerProperties.FirstOrDefault(x => innerName.StartsWith(x.Name)) is IPropertySymbol innerSymbol)
                    {
                        yield return new MemberRecord
                        {
                            Target = property.Name,
                            Source = symbol.Name + "." + innerSymbol.Name,
                            Type = property.Type.Name
                        };

                        continue;
                    }
                }
            }
        }
    }

    private IEnumerable<MemberExpressionRecord> ParseConfigure(INamedTypeSymbol to)
    {
        var symbol = to.GetMembers().OfType<IMethodSymbol>().FirstOrDefault(x => x.Name == "Configure");

        if (symbol is null)
            yield break;

        if (symbol.DeclaringSyntaxReferences[0].GetSyntax() is not MethodDeclarationSyntax syntax)
            yield break;

        var body = syntax.Body;

        if (body is null)
            yield break;

        foreach (var statementSyntax in body.Statements.OfType<ExpressionStatementSyntax>())
        {
            if (statementSyntax.Expression is not InvocationExpressionSyntax invocationSyntax)
                continue;

            var arguments = invocationSyntax.ArgumentList.Arguments;

            if (arguments.Count != 2)
                continue;

            var fromArgument = arguments[0];

            var fieldName = ((fromArgument.Expression as SimpleLambdaExpressionSyntax)?.Body as MemberAccessExpressionSyntax)?.Name.Identifier.ValueText;

            if (fieldName is null)
                continue;

            if (arguments[1].Expression is not SimpleLambdaExpressionSyntax toArgumentExpressionSyntax)
                continue;

            var fieldBody = toArgumentExpressionSyntax.Body.ToString();
            var argument = toArgumentExpressionSyntax.Parameter.Identifier.ValueText;

            yield return new()
            {
                Target = fieldName,
                SourceExpression = param => fieldBody.Replace(argument, param)
            };
        }
    }
}
