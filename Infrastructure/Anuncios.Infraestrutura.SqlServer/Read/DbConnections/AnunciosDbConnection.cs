using Microsoft.Data.SqlClient;
using System;

namespace Anuncios.Infrastructure.SqlServer.Read.DbConnections
{
    public class AnunciosDbConnection : IDisposable
    {
        public SqlConnection Connection { get; set; }

        public AnunciosDbConnection(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != System.Data.ConnectionState.Closed)
                Connection.Close();
        }
    }
}
