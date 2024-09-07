using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.InfraStructure.Interface;
using SalesDatePrediction.Transversal.Common;
using Dapper;
using System.Data;
using System.Reflection;

namespace SalesDatePrediction.InfraStructure.Repository
{
    public class DatabasesRepository : IDatabasesRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public DatabasesRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> DeleteAsync(int ID)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_Delete_database_D";
                var parameters = new DynamicParameters();

                parameters.Add("@DatabaseId", ID);

                var result = await connection.QuerySingleAsync<string>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result == "success" ? true : false;
            }
        }

        public async Task<IEnumerable<Databases>> GetAllAsync()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_Obtener_database_todos_C";

                var result = await connection.QueryAsync<Databases>(query, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<Databases> GetAsync(int ID)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_GetDataBaseById_C";
                var parameters = new DynamicParameters();
                parameters.Add("DatabaseId", ID);

                var result = await connection.QuerySingleAsync<Databases>(query, param: parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<bool> InsertAsync(Databases model)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_Insertar_database_I";
                var parameters = new DynamicParameters();

                parameters.Add("DatabaseName", model.DatabaseName);
                parameters.Add("Description", model.Description);
                parameters.Add("ConnectionString", model.ConnectionString);
                parameters.Add("UsersId", model.UsersId);

                //Persistir la info en la bd
                var result = await connection.QuerySingleAsync<string>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result == "success" ? true : false;
            }
        }

        public async Task<bool> UpdateAsync(Databases model)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_Update_database_U";
                var parameters = new DynamicParameters();

                parameters.Add("DatabaseId", model.DatabaseId);
                parameters.Add("DatabaseName", model.DatabaseName);
                parameters.Add("Description", model.Description);
                parameters.Add("ConnectionString", model.ConnectionString);                

                //Persistir la info en la bd
                var result = await connection.QuerySingleAsync<string>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result == "success" ? true : false;
            }
        }
    }
}
