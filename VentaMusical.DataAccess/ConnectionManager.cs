using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaMusical.DataAccess.Interface;

namespace VentaMusical.DataAccess
{
    public class ConnectionManager 
    {
        public ConnectionManager() {
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["DB_Connection"].ConnectionString );
        }


    }
}
