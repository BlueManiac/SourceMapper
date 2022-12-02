using SourceMapper.Tests.Custom.Models;

namespace SourceMapper.Tests.Custom;

[TestClass]
public class CustomTests
{
    [TestMethod]
    public void MapOne()
    {
        var model = new CustomTestData
        {
            Name = "Foo",
            Cool = "Bar"
        };

        var projection = model.Map().To<CustomTestDataProjection>();

        Assert.AreEqual(model.Name, projection.Name);
        Assert.AreEqual(model.Cool + " custom", projection.Cool);
    }
}