using GlacialBytes.ShortPathService.Persistence.Database.SqlServer;
using GlacialBytes.ShortPathService.Persistence.Database;

namespace Database.SqlServer.Test.TestContexts
{
  public class GivenDataContextCreated
  {
    public DataContext DataContext { get; set; }

    public virtual void Initialize(string databaseConnectionString)
    {
      DataContext = new SqlServerDataContext(databaseConnectionString, true);
    }

    public virtual void Cleanup()
    {
      DataContext.Dispose();
    }
  }
}
