using Database.SqlServer.Test.TestContexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Database.SqlServer.Test
{
  [TestClass]
  public class DataContextTest : BaseDataTest
  {
    private readonly GivenDataContextCreated _testContext = new ();

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
    public void CanConnect()
    {
      bool isDatabaseAvailable = _testContext.DataContext.Database.CanConnect();
      Assert.IsTrue(isDatabaseAvailable);
    }

    [TestMethod]
    public void GetDataBaseSchemeVersion()
    {
      string databaseSchemeVersion = _testContext.DataContext.GetDataBaseSchemeVersion();
      Assert.AreEqual("1.0.2", databaseSchemeVersion);
    }

    [TestMethod]
    public void GetDataContextSchemeVersion()
    {
      string dataContextSchemeVersion = _testContext.DataContext.GetDataContextSchemeVersion();
      Assert.AreEqual("1.0.2", dataContextSchemeVersion);
    }
  }
}
