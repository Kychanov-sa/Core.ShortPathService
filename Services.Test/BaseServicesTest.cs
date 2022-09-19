using Base.Test;
using Database.SqlServer.Test;
using System;

namespace Services.Test
{
  public class BaseServicesTest : BaseDataTest
  {
    public Guid ExpiredRouteId
    {
      get { return Guid.Parse("{d3eb7e5e-c8b1-4501-97c3-2281a874362c}"); }
    }
  }
}
