namespace SourceMapper.Tests.Models;

public partial class TestProjection : IMapping<Test, TestProjection>
{
    public required string Name { get; set; }
    public string? Cool { get; set; }
}