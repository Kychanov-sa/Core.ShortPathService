using Database.SqlServer.Test.TestContexts;
using GlacialBytes.ShortPathService.Domain.Data.DataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Database.SqlServer.Test
{
  [TestClass]
  public class RouteRepositoryTest : BaseDataTest
  {
    private readonly GivenRouteRepositoryAcquired _testContext = new ();

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
    public void Any()
    {
      bool result = _testContext.Repository.Any(r => r.BestBefore != null);
      Assert.IsTrue(result);

      result = _testContext.Repository.Any(r => r.FullUrl == "invalid uri");
      Assert.IsFalse(result);
    }  

    [TestMethod]
    public void Create_Delete()
    {
      Guid? routeId = null;
      try
      {
        var route = new Route()
        {
          BestBefore = DateTime.UtcNow.AddDays(1),
          FullUrl = "https://www.youtube.com/channel/UCxTclqPDlFzC6yMVtWYm_DA",
        };
        _testContext.Repository.Create(route);        
        int changedRows = _testContext.DataProvider.SaveChanges();
        Assert.AreEqual(1, changedRows);
        
        routeId = route.Id;
      }
      finally
      {
        if (routeId.HasValue)
        {
          _testContext.Repository.Delete(routeId.Value);
          int changedRows = _testContext.DataProvider.SaveChanges();
          Assert.AreEqual(1, changedRows);
        }
      }
    }

    [TestMethod]
    public void CreateMany_DeleteMany()
    {
      Guid? route1Id = null;
      Guid? route2Id = null;
      try
      {
        var route1 = new Route()
        {
          BestBefore = DateTime.UtcNow.AddDays(1),
          FullUrl = "https://docs.microsoft.com/en-us/azure/architecture/patterns/cache-aside",
        };

        var route2 = new Route()
        {
          BestBefore = DateTime.UtcNow.AddMonths(1),
          FullUrl = "https://midjourney.gitbook.io/docs/",
        };

        _testContext.Repository.CreateMany(new[]
        {
          route1,
          route2,
        });
        int changedRows = _testContext.DataProvider.SaveChanges();
        Assert.AreEqual(2, changedRows);

        route1Id = route1.Id;
        route2Id = route2.Id;
      }
      finally
      {
        if (route1Id.HasValue && route2Id.HasValue)
        {
          _testContext.Repository.DeleteMany(r => r.Id == route1Id.Value || r.Id == route2Id.Value);
          int changedRows = _testContext.DataProvider.SaveChanges();
          Assert.AreEqual(2, changedRows);
        }
      }
    }

    [TestMethod]
    public void Get()
    {
      var route = _testContext.Repository.Get(TestRouteId);
      Assert.IsNotNull(route);
      Assert.IsNotNull(route.BestBefore);
      Assert.AreEqual(TestRouteUrl, route.FullUrl);
    }

    [TestMethod]
    public void GetAll()
    {
      var routes = _testContext.Repository.GetAll();
      Assert.IsNotNull(routes);
      Assert.IsTrue(routes.Count() > 0);
    }

    [TestMethod]
    public void GetOne()
    {
      var route = _testContext.Repository.GetOne(r => r.FullUrl == "https://www.youtube.com/watch?v=v7d6cw-26RA");
      Assert.IsNotNull(route);
      Assert.AreEqual("https://www.youtube.com/watch?v=v7d6cw-26RA", route.FullUrl);
    }

    [TestMethod]
    public void GetMany()
    {
      var routes = _testContext.Repository.GetMany(r => r.BestBefore != null || r.FullUrl == "https://www.youtube.com/watch?v=v7d6cw-26RA");
      Assert.IsNotNull(routes);
      Assert.AreEqual(3, routes.Count());
    }

    [TestMethod]
    public void GetQuery()
    {
      var query = _testContext.Repository.GetQuery();
      Assert.IsNotNull(query);

      var immortalRoutes = query.Where(r => r.BestBefore == null).Select(r => r.FullUrl);
      Assert.IsNotNull(immortalRoutes);
      Assert.IsTrue(immortalRoutes.Count() > 0);
      Assert.AreEqual("https://www.youtube.com/watch?v=v7d6cw-26RA", immortalRoutes.FirstOrDefault());
    }

    [TestMethod]
    public void Update()
    {
      var route = _testContext.Repository.Get(TestRouteId);
      route.BestBefore = DateTime.UtcNow.AddDays(1);

      _testContext.Repository.Update(route, TestRouteId);
      int changedRows = _testContext.DataProvider.SaveChanges();
      Assert.AreEqual(1, changedRows);
    }
  }
}
