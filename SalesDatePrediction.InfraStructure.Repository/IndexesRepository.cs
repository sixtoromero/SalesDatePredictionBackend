using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.InfraStructure.Interface;
using SalesDatePrediction.Transversal.Common;
using Dapper;
using Newtonsoft.Json;
using System.Data;

namespace SalesDatePrediction.InfraStructure.Repository
{
    public class IndexesRepository : IIndexesRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IDatabasesRepository _databasesRepository;
        private readonly ITablesRepository _tablesRepository;
        public IndexesRepository(IConnectionFactory connectionFactory, IDatabasesRepository databasesRepository, ITablesRepository tablesRepository)
        {
            _connectionFactory = connectionFactory;
            _databasesRepository = databasesRepository;
            _tablesRepository = tablesRepository;
        }

        public async Task<bool> DeleteAsync(int ID)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_Delete_IndexesById_D";
                var parameters = new DynamicParameters();

                parameters.Add("@ColumnId", ID);

                var result = await connection.QuerySingleAsync<string>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result == "success" ? true : false;
            }
        }

        public async Task<IEnumerable<Indexes>> GetAllAsync()
        {
            using (var connection = _connectionFactory.GetConnection)
            {                
                var query = "PROD_GetIndexsAll_C";
                var result = await connection.QueryAsync<Indexes>(query, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<Indexes> GetAsync(int ID)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_GetIndexById_C";
                var parameters = new DynamicParameters();
                parameters.Add("ColumnId", ID);

                var result = await connection.QuerySingleAsync<Indexes>(query, param: parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<bool> InsertAsync(Indexes model)
        {
            //Validar Si existe la tabla
            var resp = await ExistIndexesDocAsync(model.TableId, model!.IndexName!);

            //Si es verdadero es porque existe la tabla en la base de datos
            if (resp)
            {
                return false;
            }

            using (var connection = _connectionFactory.GetConnection)
            {
                //var query = "PROD_Insertar_estructura_tabla_I";
                var query = "PROD_Insertar_Indexes_I";
                var parameters = new DynamicParameters();

                parameters.Add("TableId", model.TableId);
                parameters.Add("IndexName", model.IndexName);
                parameters.Add("IndexType", model.IndexType);
                parameters.Add("IncludedColumns", model.IncludedColumns);
                parameters.Add("UsersId", model.UsersId);                

                //Persistir la info en la bd
                var result = await connection.QuerySingleAsync<string>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result == "success" ? true : false;
            }
        }

        public async Task<bool> UpdateAsync(Indexes model)
        {
            //Validar Si existe la tabla
            var resp = await ExistIndexesDocAsync(model.TableId, model!.IndexName!);

            //Si es verdadero es porque existe la tabla en la base de datos
            if (resp)
            {
                return false;
            }

            using (var connection = _connectionFactory.GetConnection)
            {
                //var query = "PROD_Insertar_estructura_tabla_I";
                var query = "PROD_Update_Indexes_I";
                var parameters = new DynamicParameters();

                parameters.Add("TableId", model.TableId);
                parameters.Add("IndexName", model.IndexName);
                parameters.Add("IndexType", model.IndexType);
                parameters.Add("IncludedColumns", model.IncludedColumns);
                parameters.Add("UsersId", model.UsersId);

                //Persistir la info en la bd
                var result = await connection.QuerySingleAsync<string>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result == "success" ? true : false;
            }
        }

        public async Task<bool> ExistIndexesDocAsync(int TableId, string IndexesName)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_Exists_Indexes_C";
                var parameters = new DynamicParameters();
                parameters.Add("TableId", TableId);
                parameters.Add("IndexesName", IndexesName);

                var result = await connection.QuerySingleAsync<Indexes>(query, param: parameters, commandType: CommandType.StoredProcedure);

                return result == null ? false : true;
            }
        }

        public async Task<bool> MassIndexesRegistrationAsync(int DatabaseId, int UserId)
        {            
            IEnumerable<Indexes> indexesRegister;
            IEnumerable<Tables> tableDB;
            List<Indexes> indexesList = new List<Indexes>();

            string jsonIndexes = string.Empty;

            var respDatabase = await _databasesRepository.GetAsync(DatabaseId);
            if (respDatabase == null)
            {
                return false;
            }

            tableDB = await _tablesRepository.GetTablesByDataBaseId(DatabaseId);
            //jsonIndexes = JsonConvert.SerializeObject(tableDB, Newtonsoft.Json.Formatting.Indented);

            using (var connection = _connectionFactory.GetConnectionWithString(respDatabase.ConnectionString!))
            {
                try
                {
                    foreach (var item in tableDB)
                    {
                        #region Script para consultar los indixes por tabla
                        string strSQL = @"
                            SELECT 
			                    @TableId as TableId,
			                    i.name AS IndexName,
			                    CASE WHEN i.is_unique = 1 THEN 'Único' ELSE 'No Único' END AS IndexType,
			                    STUFF((
				                    SELECT ', ' + COL_NAME(ic.object_id, ic.column_id)
				                    FROM sys.index_columns ic
				                    WHERE i.object_id = ic.object_id AND i.index_id = ic.index_id
				                    FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS IncludedColumns,
			                    @UserId AS UsersId
		                    FROM sys.indexes i
		                    WHERE i.object_id = OBJECT_ID(@TableName) AND i.type_desc <> 'HEAP'
		                    ORDER BY i.name;
                        ";
                        #endregion

                        var parameters = new DynamicParameters();

                        parameters.Add("@TableId", item.TableId);
                        parameters.Add("@TableName", item.TableName);
                        parameters.Add("@UserId", UserId);

                        var indexesTable = await connection.QueryAsync<Indexes>(strSQL, param: parameters);

                        indexesList.AddRange(indexesTable);
                    }

                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            jsonIndexes = JsonConvert.SerializeObject(indexesList, Newtonsoft.Json.Formatting.Indented);

            using (var connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "PROD_Registro_Indexes_Por_Tabla_json_I";
                    var parameters = new DynamicParameters();

                    parameters.Add("json", jsonIndexes);
                    //Persistir la info en la bd
                    var result = await connection.QuerySingleAsync<string>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                    return result == "success" ? true : false;
                }
                catch (Exception)
                {
                    throw;
                }                
            }
        }
    }
}
