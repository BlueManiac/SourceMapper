using SourceMapper.Tests.Flattening.Models;
using SourceMapper.Tests.Models;

namespace SourceMapper.Tests.Flattening;

[TestClass]
public class FlatteningTests
{
    [TestMethod]
    public void MapOne()
    {
        var model = new FlatteningTestData
        {
            Inner = new FlatteningTestDataInner
            {
                Name = "Foo",
                Cool = "Bar"
            }
        };

        var projection = model.Map().To<FlatteningTestDataProjection>();

        Assert.AreEqual(model.Inner.Name, projection.InnerName);
        Assert.AreEqual(model.Inner.Cool, projection.InnerCool);
    }
}