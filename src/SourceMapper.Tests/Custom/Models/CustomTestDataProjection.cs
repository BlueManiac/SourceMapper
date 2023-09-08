namespace SourceMapper.Tests.Custom.Models;
[Map<CustomTestData>]
public partial class CustomTestDataProjection
{
    public required string Name { get; set; }
    public string? Cool { get; set; }

    protected static void Configure(MappingContext<CustomTestData, CustomTestDataProjection> context)
    {
        context.Map(x => x.Cool, x => x.Cool + " custom");
    }
}