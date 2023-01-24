using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GlacialBytes.Foundation.Data;
using GlacialBytes.ShortPathService.Domain.Data.DataModels;

namespace Database.SqlServer.Test.TestContexts
{
  public class GivenRouteRepositoryAcquired : GivenDataProviderCreated
  {
    public IRepository<long, Route> Repository { get; set; }

    public override void Initialize(string databaseConnectionString)
    {
      base.Initialize(databaseConnectionString);
      Repository = DataProvider.GetRepository<Route>();
    }

    public override void Cleanup()
    {
      base.Cleanup();
    }
  }
}
