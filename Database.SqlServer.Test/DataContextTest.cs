using GlacialBytes.ShortPathService.Persistence.Database;
using GlacialBytes.ShortPathService.Persistence.Database.SqlServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Database.SqlServer.Test
{
  [TestClass]
  public class DataContextTest : BaseDataTest
  {
    private DataContext _dataContext;

    [TestInitialize]
    public void InitializeTest()
    {
      _dataContext = new SqlServerDataContext(DatabaseConnectionString, true);
    }

    [TestCleanup]
    public void CleanupTest()
    {
      _dataContext.Dispose();
    }

    [TestMethod]
    public void CanConnect()
    {
      bool isDatabaseAvailable = _dataContext.Database.CanConnect();
      Assert.IsTrue(isDatabaseAvailable);
    }

    [TestMethod]
    public void GetDataBaseSchemeVersion()
    {
      string databaseSchemeVersion = _dataContext.GetDataBaseSchemeVersion();
      Assert.AreEqual("1.0.2", databaseSchemeVersion);
    }

    [TestMethod]
    public void GetDataContextSchemeVersion()
    {
      string dataContextSchemeVersion = _dataContext.GetDataContextSchemeVersion();
      Assert.AreEqual("1.0.2", dataContextSchemeVersion);
    }
  }
}
