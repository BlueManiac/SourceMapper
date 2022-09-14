using System.Collections.Generic;

namespace SourceMapper.Generator.Models;

public record MapperRecord
{
    public string Namespace { get; set; }
    public string ClassName { get; set; }
    public string FullClassName { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public List<MemberRecord> Properties { get; set; }
    public string ToName { get; set; }
}
