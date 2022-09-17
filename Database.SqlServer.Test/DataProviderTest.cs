using GlacialBytes.ShortPathService.Domain.Data.DataModels;
using GlacialBytes.ShortPathService.Persistence.Database;
using GlacialBytes.ShortPathService.Persistence.Database.SqlServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Database.SqlServer.Test
{
  [TestClass]
  public class DataProviderTest : BaseDataTest
  {
    private DataContext _dataContext;
    private DataProvider _dataProvider;

    [TestInitialize]
    public void InitializeTest()
    {
      _dataContext = new SqlServerDataContext(DatabaseConnectionString, true);
      _dataProvider = new DataProvider(_dataContext);
    }

    [TestCleanup]
    public void CleanupTest()
    {
      _dataProvider.Dispose();
      _dataContext.Dispose();
    }

    [TestMethod]
    public void GetRepository_ForRoutes()
    {
      var routesRepository = _dataProvider.GetRepository<Route>();
      Assert.IsNotNull(routesRepository);
    }

    [TestMethod]
    public void GetQuery_ForRoutes()
    {
      var routesQuery = _dataProvider.GetQuery<Route>();
      Assert.IsNotNull(routesQuery);
    }
  }
}
