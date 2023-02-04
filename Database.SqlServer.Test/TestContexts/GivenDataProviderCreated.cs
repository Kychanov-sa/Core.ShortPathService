using Microsoft.VisualStudio.TestTools.UnitTesting;
using GlacialBytes.ShortPathService.Domain.Data;
using GlacialBytes.ShortPathService.Persistence.Database;

namespace Database.SqlServer.Test.TestContexts
{
  public partial class GivenDataProviderCreated : GivenDataContextCreated
  {
    public IDataProvider DataProvider { get; set; }

    public override void Initialize(string databaseConnectionString)
    {
      base.Initialize(databaseConnectionString);
      DataProvider = new DataProvider(DataContext);
    }

    public override void Cleanup()
    {
      DataProvider?.Dispose();
      base.Cleanup();
    }
  }
}
