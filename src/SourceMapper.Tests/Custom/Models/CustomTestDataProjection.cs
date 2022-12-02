namespace SourceMapper.Tests.Custom.Models;
public partial class CustomTestDataProjection : IMapping<CustomTestData, CustomTestDataProjection>
{
    public required string Name { get; set; }
    public string? Cool { get; set; }

    protected static void Configure(MappingContext<CustomTestData, CustomTestDataProjection> context)
    {
        context.Map(x => x.Cool, x => x.Cool + " custom");
    }
}