using Database.SqlServer.Test.TestContexts;
using GlacialBytes.ShortPathService.Domain.Data.DataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Database.SqlServer.Test
{
  [TestClass]
  public class DataProviderTest : BaseDataTest
  {
    private readonly GivenDataProviderCreated _testContext = new ();

    [TestInitialize]
    public void InitializeTest()
    {
      _testContext.Initialize(DatabaseConnectionString);
    }

    [TestCleanup]
    public void CleanupTest()
    {
      _testContext.Cleanup();
    }

    [TestMethod]
    public void GetRepository_ForRoutes()
    {
      var routesRepository = _testContext.DataProvider.GetRepository<Route>();
      Assert.IsNotNull(routesRepository);
    }

    [TestMethod]
    public void GetQuery_ForRoutes()
    {
      var routesQuery = _testContext.DataProvider.GetQuery<Route>();
      Assert.IsNotNull(routesQuery);
    }
  }
}
