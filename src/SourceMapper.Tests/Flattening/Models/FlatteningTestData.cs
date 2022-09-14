using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceMapper.Tests.Flattening.Models
{
    public class FlatteningTestData
    {
        public FlatteningTestDataInner Inner { get; set; }
    }

    public class FlatteningTestDataInner
    {
        public string Name { get; set; }
        public string Cool { get; internal set; }
    }
}
