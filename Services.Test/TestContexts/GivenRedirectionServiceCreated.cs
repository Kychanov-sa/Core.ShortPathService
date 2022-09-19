using Database.SqlServer.Test.TestContexts;
using GlacialBytes.Core.ShortPathService.Services;

namespace Services.Test.TestContexts
{
  public class GivenRedirectionServiceCreated : GivenRouteRepositoryAcquired
  {
    public IRedirectionService RedirectionService { get; set; }

    public override void Initialize(string databaseConnectionString)
    {
      base.Initialize(databaseConnectionString);
      RedirectionService = new RedirectionService(DataProvider);
    }

    public override void Cleanup()
    {
      base.Cleanup();
    }
  }
}
