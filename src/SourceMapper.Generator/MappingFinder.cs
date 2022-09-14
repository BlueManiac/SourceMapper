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

        if (context.SemanticModel.GetDeclaredSymbol(syntax) is not INamedTypeSymbol classSymbol)
            return;

        if (classSymbol.AllInterfaces.FirstOrDefault(x => x.Name == "IMapping") is not INamedTypeSymbol classInterface)
            return;

        if (syntax.Parent is not BaseNamespaceDeclarationSyntax namespaceSyntax)
            return;

        var from = (INamedTypeSymbol)classInterface.TypeArguments[0];
        var to = (INamedTypeSymbol)classInterface.TypeArguments[1];

        Maps.Add(new MapperRecord
        {
            Namespace = namespaceSyntax.Name.ToString(),
            ClassName = syntax.Identifier.ToString(),
            FullClassName = namespaceSyntax.Name + "." + syntax.Identifier,
            From = from.ToString(),
            To = to.ToString(),
            ToName = to.Name,
            Properties = ParseMembers(from, to).ToList()
        });
    }

    private IEnumerable<MemberRecord> ParseMembers(INamedTypeSymbol from, INamedTypeSymbol to)
    {
        var fromProperties = from.GetMembers().Where(x => x.Kind == SymbolKind.Property).OfType<IPropertySymbol>().ToList();
        var toProperties = to.GetMembers().Where(x => x.Kind == SymbolKind.Property).OfType<IPropertySymbol>();

        foreach (var property in toProperties)
        {
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
}
