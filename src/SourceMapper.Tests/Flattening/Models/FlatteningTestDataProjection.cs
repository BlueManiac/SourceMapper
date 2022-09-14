using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceMapper.Tests.Flattening.Models
{
    public partial class FlatteningTestDataProjection : IMapping<FlatteningTestData, FlatteningTestDataProjection>
    {
        public string InnerName { get; set; }
        public string InnerCool { get; internal set; }
    }
}
