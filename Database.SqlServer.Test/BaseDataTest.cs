using Base.Test;
using System;

namespace Database.SqlServer.Test
{
  public class BaseDataTest : BaseTest
  {
    public string DatabaseConnectionString
    {
      get
      {
        return Configuration["SqlDatabaseConnectionString"];
      }
    }

    public Guid TestRouteId
    {
      get { return Guid.Parse("{ee8d4c33-23e2-4e7c-a152-abb0de3ad59e}"); }
    }
  }
}
