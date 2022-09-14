namespace SourceMapper.Generator.Models;

public record MemberRecord
{
    public string Target { get; set; }
    public string Source { get; set; }
    public string Type { get; set; }
}
