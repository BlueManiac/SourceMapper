namespace SourceMapper.Tests.Flattening.Models
{
    [Map<FlatteningTestData>]
    public partial class FlatteningTestDataProjection
    {
        public required string InnerName { get; set; }
        public string? InnerCool { get; set; }
    }
}
