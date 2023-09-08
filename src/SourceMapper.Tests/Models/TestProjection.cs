﻿namespace SourceMapper.Tests.Models;

[Map<Test>]
public partial class TestProjection
{
    public required string Name { get; set; }
    public string? Cool { get; set; }
}