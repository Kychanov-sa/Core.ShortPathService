using Refit;
using ShortPathService.WebApi.WebApi.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Client.Test
{
  public class BaseWebClientTest : Base.Test.BaseTest
  {
    public const string TestShortPathServiceAddress = "http://rx360.ru";

    public IShortPathApi GetWebClient()
    {
      return RestService.For<IShortPathApi>(TestShortPathServiceAddress, new RefitSettings()
      {
      });
    }
  }
}
