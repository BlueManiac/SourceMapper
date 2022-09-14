using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SourceMapper.Tests.Models;

public partial class TestProjection3 : IMapping<Test2, TestProjection3>
{
    public string Name { get; set; }
}
