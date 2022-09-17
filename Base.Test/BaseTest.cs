using Microsoft.Extensions.Configuration;

namespace Base.Test
{
  public abstract class BaseTest
  {
    public IConfiguration Configuration { get; set; }

    public BaseTest()
    {
      var builder = new ConfigurationBuilder()
                .AddUserSecrets<BaseTest>();

      Configuration = builder.Build();
    }
  }
}
