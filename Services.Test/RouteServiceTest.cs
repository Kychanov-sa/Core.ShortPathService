using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Test.TestContexts;
using System;

namespace Services.Test
{
  [TestClass]
  public class RouteServiceTest : BaseServicesTest
  {
    private readonly GivenRouteServiceCreated _textContext = new ();

    [TestInitialize]
    public void InitializeTest()
    {
      _textContext.Initialize(DatabaseConnectionString);
    }

    [TestCleanup]
    public void CleanupTest()
    {
      _textContext.Cleanup();
    }

    [TestMethod]
    public void AddRoute()
    {
      string routeId = default;
      try
      {
        routeId = _textContext.RouteService.AddRoute(new Uri("https://www.youtube.com/watch?v=65AdFi86DMM"), null);
        Assert.IsNotNull(routeId);
        Assert.IsTrue(Guid.TryParse(routeId, out Guid _));
      }
      finally
      {
        if (!String.IsNullOrEmpty(routeId) && Guid.TryParse(routeId, out Guid nativeId))
        {
          _textContext.Repository.Delete(nativeId);
        }
      }
    }
  }
}
