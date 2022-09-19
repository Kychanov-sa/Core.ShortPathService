using Database.SqlServer.Test.TestContexts;
using GlacialBytes.Core.ShortPathService.Services;

namespace Services.Test.TestContexts
{
  public class GivenRouteServiceCreated : GivenRouteRepositoryAcquired
  {
    public IRouteService RouteService { get; set; }

    public override void Initialize(string databaseConnectionString)
    {
      base.Initialize(databaseConnectionString);
      RouteService = new RouteService(DataProvider);
    }

    public override void Cleanup()
    {
      base.Cleanup();
    }
  }
}
