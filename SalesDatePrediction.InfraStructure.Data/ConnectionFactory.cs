using SalesDatePrediction.Transversal.Common;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SalesDatePrediction.InfraStructure.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        /// <summary>
        /// Nos va a permitir acceder a las propiedades de los diferentes proyectos
        /// </summary>
        private readonly IConfiguration _configuration;

        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Método que nos permite acceder a la base de datos en GlobusConnection
        /// </summary>
        public IDbConnection GetConnection
        {

            get
            {
                var sqlConnection = new SqlConnection();
                if (sqlConnection == null) return null;
                sqlConnection.ConnectionString = _configuration.GetConnectionString("Connection");
                sqlConnection.Open();
                return sqlConnection;
            }
        }
                                                                             
        /// <summary>
        /// Método que nos permite acceder a la base de datos con un connection string especificado
        /// </summary>
        public IDbConnection GetConnectionWithString(string connectionString)
        {

            var sqlConnection = new SqlConnection();
            if (sqlConnection == null) return null;
            sqlConnection.ConnectionString = connectionString;
            sqlConnection.Open();
            return sqlConnection;
        }

    }
}
