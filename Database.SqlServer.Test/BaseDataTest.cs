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

    public long TestRouteId
    {
      get { return 1; }
    }

    public string TestRouteUrl
    {
      get { return "https://www.youtube.com/c/Nellifornication"; }
    }
  }
}
