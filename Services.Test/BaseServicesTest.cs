using CSharpVitamins;
using Database.SqlServer.Test;
using System;

namespace Services.Test
{
  public class BaseServicesTest : BaseDataTest
  {
    public string ExpiredRouteId
    {
      get { return new ShortGuid(Guid.Parse("{d3eb7e5e-c8b1-4501-97c3-2281a874362c}")).ToString(); }
    }

    public string ValidRouteId
    {
      get { return new ShortGuid(Guid.Parse("{ee8d4c33-23e2-4e7c-a152-abb0de3ad59e}")).ToString(); }
    }

    public string InvalidRouteId
    {
      get { return "invalid id"; }
    }

    public string NotExistsRouteId
    {
      get { return new ShortGuid(Guid.NewGuid()).ToString(); }
    }
  }
}
