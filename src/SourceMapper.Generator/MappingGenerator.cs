using Microsoft.CodeAnalysis;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SourceMapper.Generator;

[Generator]
public class MappingGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new MappingFinder());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var mappingFinder = (MappingFinder)context.SyntaxContextReceiver!;

        foreach (var map in mappingFinder.Maps)
        {
            var properties = map.Properties.Select(x => $"{x.Target} = x.{x.Source}");

            string source = $@"using System;
using System.Linq.Expressions;

namespace {map.Namespace}
{{
    public partial class {map.ClassName}
    {{
        public static Expression<Func<{map.From}, {map.To}>> MapFromExpression {{ get; }} = x => new {map.ToName}
        {{
            {string.Join(",\n            ", properties)}
        }};

        public static Func<{map.From}, {map.To}> MapFrom {{ get; }} = MapFromExpression.Compile();
    }}
}}
";

            context.AddSource($"{map.FullClassName}.g.cs", source);
        }
    }
}
