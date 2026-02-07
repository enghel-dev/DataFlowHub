using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace DataFlowHub.Infrastructure.DataBase
{
    public class DBconnectionFactory
    {
        //Inyeccion de dependencias
        private readonly string _connectionString;

        public DBconnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection CreateConection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
