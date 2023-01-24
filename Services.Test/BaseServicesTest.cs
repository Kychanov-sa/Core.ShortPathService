using Base62;
using Database.SqlServer.Test;

namespace Services.Test
{
  public class BaseServicesTest : BaseDataTest
  {
    public string ExpiredRouteId
    {
      get { return 3L.ToBase62(); }
    }

    public string ValidRouteId
    {
      get { return 1L.ToBase62(); }
    }

    public string InvalidRouteId
    {
      get { return "1"; }
    }

    public string NotExistsRouteId
    {
      get { return 5L.ToBase62(); }
    }
  }
}
