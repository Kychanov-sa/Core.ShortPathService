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
  }
}
