using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.InfraStructure.Interface;
using SalesDatePrediction.Transversal.Common;
using Dapper;
using System.Data;

namespace SalesDatePrediction.InfraStructure.Repository
{
    public class ConstraintsRepository : IConstraintsRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public ConstraintsRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> DeleteAsync(int ID)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_Delete_ConstraintsById_D";
                var parameters = new DynamicParameters();

                parameters.Add("@ColumnId", ID);

                var result = await connection.QuerySingleAsync<string>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result == "success" ? true : false;
            }
        }

        public async Task<IEnumerable<Constraints>> GetAllAsync()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_GetConstraintsAll_C";
                var result = await connection.QueryAsync<Constraints>(query, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<Constraints> GetAsync(int ID)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_GetConstraintsById_C";
                var parameters = new DynamicParameters();
                parameters.Add("ColumnId", ID);

                var result = await connection.QuerySingleAsync<Constraints>(query, param: parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<bool> InsertAsync(Constraints model)
        {
            //Validar Si existe la tabla
            var resp = await ExistConstraintsDocAsync(model.TableId, model!.ConstraintName!);

            //Si es verdadero es porque existe la tabla en la base de datos
            if (resp)
            {
                return false;
            }

            using (var connection = _connectionFactory.GetConnection)
            {
                //var query = "PROD_Insertar_estructura_tabla_I";
                var query = "PROD_Insertar_Constraints_I";
                var parameters = new DynamicParameters();

                parameters.Add("TableId", model.TableId);
                parameters.Add("ConstraintName", model.ConstraintName);
                parameters.Add("ConstraintType", model.ConstraintType);
                parameters.Add("Description", model.Description);
                parameters.Add("UsersId", model.UsersId);

                //Persistir la info en la bd
                var result = await connection.QuerySingleAsync<string>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result == "success" ? true : false;
            }
        }

        public async Task<bool> UpdateAsync(Constraints model)
        {
            //Validar Si existe la tabla
            var resp = await ExistConstraintsDocAsync(model.TableId, model!.ConstraintName!);

            //Si es verdadero es porque existe la tabla en la base de datos
            if (resp)
            {
                return false;
            }

            using (var connection = _connectionFactory.GetConnection)
            {
                //var query = "PROD_Insertar_estructura_tabla_I";
                var query = "PROD_Update_Constraints_I";
                var parameters = new DynamicParameters();

                parameters.Add("ConstraintId", model.ConstraintId);
                parameters.Add("TableId", model.TableId);
                parameters.Add("ConstraintName", model.ConstraintName);
                parameters.Add("ConstraintType", model.ConstraintType);
                parameters.Add("Description", model.Description);
                parameters.Add("UsersId", model.UsersId);

                //Persistir la info en la bd
                var result = await connection.QuerySingleAsync<string>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result == "success" ? true : false;
            }
        }

        public async Task<bool> ExistConstraintsDocAsync(int TableId, string ConstraintsName)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_Exists_Constraints_C";
                var parameters = new DynamicParameters();
                parameters.Add("TableId", TableId);
                parameters.Add("ConstraintsName", ConstraintsName);

                var result = await connection.QuerySingleAsync<Constraints>(query, param: parameters, commandType: CommandType.StoredProcedure);

                return result == null ? false : true;
            }
        }        

        public async Task<bool> MassConstraintsRegistrationAsync(int TableId, int UserId)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_Registro_Constraints_Por_Tabla_I";
                var parameters = new DynamicParameters();

                parameters.Add("TableId", TableId);
                parameters.Add("UserId", UserId);
                //Persistir la info en la bd
                var result = await connection.QuerySingleAsync<string>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result == "success" ? true : false;
            }
        }

        
    }
}
