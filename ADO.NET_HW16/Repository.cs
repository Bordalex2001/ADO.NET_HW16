using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_HW16
{
    public class Repository : IDisposable
    {
        public SqlConnection? connection = null;

        public Repository() 
        { 
            connection = new SqlConnection();
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appconfig.json");
            var config = builder.Build();
            connection.ConnectionString = config.GetConnectionString("DefaultConnection");
        }

        public void Dispose()
        {
            connection?.Dispose();
        }
    }
}