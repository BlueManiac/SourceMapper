﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SourceMapper.Tests.Models;

public partial class TestProjection : IMapping<Test, TestProjection>
{
    public required string Name { get; set; }
    public string? Cool { get; set; }
}
