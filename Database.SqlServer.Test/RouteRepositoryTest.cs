using GlacialBytes.Foundation.Data;
using GlacialBytes.ShortPathService.Domain.Data.DataModels;
using GlacialBytes.ShortPathService.Persistence.Database;
using GlacialBytes.ShortPathService.Persistence.Database.SqlServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Database.SqlServer.Test
{
  [TestClass]
  public class RouteRepositoryTest : BaseDataTest
  {
    private DataContext _dataContext;
    private DataProvider _dataProvider;
    private IRepository<Guid, Route> _repository;

    [TestInitialize]
    public void InitializeTest()
    {
      _dataContext = new SqlServerDataContext(DatabaseConnectionString, true);
      _dataProvider = new DataProvider(_dataContext);
      _repository = _dataProvider.GetRepository<Route>();
    }

    [TestCleanup]
    public void CleanupTest()
    {
      _dataProvider.Dispose();
      _dataContext.Dispose();
    }

    [TestMethod]
    public void Any()
    {
      bool result = _repository.Any(r => r.BestBefore != null);
      Assert.IsTrue(result);

      result = _repository.Any(r => r.FullUrl == "invalid uri");
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
        _repository.Create(route);        
        int changedRows = _dataProvider.SaveChanges();
        Assert.AreEqual(1, changedRows);
        
        routeId = route.Id;
      }
      finally
      {
        if (routeId.HasValue)
        {
          _repository.Delete(routeId.Value);
          int changedRows = _dataProvider.SaveChanges();
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

        _repository.CreateMany(new[]
        {
          route1,
          route2,
        });
        int changedRows = _dataProvider.SaveChanges();
        Assert.AreEqual(2, changedRows);

        route1Id = route1.Id;
        route2Id = route2.Id;
      }
      finally
      {
        if (route1Id.HasValue && route2Id.HasValue)
        {
          _repository.DeleteMany(r => r.Id == route1Id.Value || r.Id == route2Id.Value);
          int changedRows = _dataProvider.SaveChanges();
          Assert.AreEqual(2, changedRows);
        }
      }
    }

    [TestMethod]
    public void Get()
    {
      var route = _repository.Get(TestRouteId);
      Assert.IsNotNull(route);
      Assert.IsNotNull(route.BestBefore);
      Assert.AreEqual("https://www.youtube.com/c/Nellifornication", route.FullUrl);
    }

    [TestMethod]
    public void GetAll()
    {
      var routes = _repository.GetAll();
      Assert.IsNotNull(routes);
      Assert.AreEqual(2, routes.Count());
    }

    [TestMethod]
    public void GetOne()
    {
      var route = _repository.GetOne(r => r.BestBefore != null);
      Assert.IsNotNull(route);
      Assert.IsNotNull(route.BestBefore);
    }

    [TestMethod]
    public void GetMany()
    {
      var routes = _repository.GetMany(r => r.BestBefore != null || r.FullUrl == "https://www.youtube.com/watch?v=v7d6cw-26RA");
      Assert.IsNotNull(routes);
      Assert.AreEqual(2, routes.Count());
    }

    [TestMethod]
    public void GetQuery()
    {
      var query = _repository.GetQuery();
      Assert.IsNotNull(query);

      var immortalRoutes = query.Where(r => r.BestBefore == null).Select(r => r.FullUrl);
      Assert.IsNotNull(immortalRoutes);
      Assert.AreEqual(1, immortalRoutes.Count());
      Assert.AreEqual("https://www.youtube.com/watch?v=v7d6cw-26RA", immortalRoutes.FirstOrDefault());
    }

    [TestMethod]
    public void Update()
    {
      var route = _repository.Get(TestRouteId);
      route.BestBefore = DateTime.UtcNow.AddDays(1);

      _repository.Update(route, TestRouteId);
      int changedRows = _dataProvider.SaveChanges();
      Assert.AreEqual(1, changedRows);
    }
  }
}
