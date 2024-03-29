﻿namespace SourceMapper.Generator.Models;

public record MemberRecord : IMemberRecord
{
    public string Target { get; set; }
    public string Type { get; set; }
    public string Source { get; set; }
}
