using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace Redbridge.EntityFramework
{
    public class SqlAzureDbConfiguration : DbConfiguration
    {
		public SqlAzureDbConfiguration()
		{
			SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
		}
    }
}
