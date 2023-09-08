using SourceMapper.Tests.Models;

namespace SourceMapper.Tests;

[TestClass]
public class MappingTests
{
    [TestMethod]
    public void MapOne()
    {
        var model = new Test
        {
            Name = "cool"
        };

        var projection = TestProjection.MapFrom(model);

        Assert.AreEqual(model.Name, projection.Name);
    }

    [TestMethod]
    public void MapMany()
    {
        var model1 = new Test
        {
            Name = "cool"
        };
        var model2 = new Test
        {
            Name = "cool2"
        };
        var list = new List<Test> { model1, model2 };

        var projection = list.Map().To<TestProjection>().ToList();

        Assert.AreEqual(model1.Name, projection[0].Name);

        Assert.AreEqual(model2.Name, projection[1].Name);
    }

    [TestMethod]
    public void MapManyQueryable()
    {
        var model1 = new Test
        {
            Name = "cool",
            Cool = "dfsdf"
        };
        var model2 = new Test
        {
            Name = "cool2"
        };
        var list = new List<Test> { model1, model2 };

        var projection = list.AsQueryable().Map().To<TestProjection>().ToList();

        Assert.AreEqual(model1.Name, projection[0].Name);
        Assert.AreEqual(model1.Cool, projection[0].Cool);

        Assert.AreEqual(model2.Name, projection[1].Name);
    }

    [TestMethod]
    public void Test3()
    {
        var model1 = new Test2
        {
            Name = "cool"
        };

        var list = new List<Test2> { model1 };

        var projection = list.AsQueryable().Map().To<TestProjection2>().ToList();

        Assert.AreEqual(model1.Name, projection[0].Name);
    }
}