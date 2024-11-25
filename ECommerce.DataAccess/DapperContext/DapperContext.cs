using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.DapperContext
{
    public class AppDapperContext(IConfiguration configuration)
    {
        private string connectionString => configuration.GetConnectionString("Def") ?? string.Empty;

        public IDbConnection CreateConnection() => new SqlConnection(connectionString);
    }
}
