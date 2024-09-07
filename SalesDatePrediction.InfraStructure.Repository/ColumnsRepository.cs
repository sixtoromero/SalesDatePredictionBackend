using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.InfraStructure.Interface;
using SalesDatePrediction.Transversal.Common;
using Dapper;
using Newtonsoft.Json;
using System.Data;

namespace SalesDatePrediction.InfraStructure.Repository
{
    public class ColumnsRepository : IColumnsRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ITablesRepository _tablesRepository;
        private readonly IDatabasesRepository _databasesRepository;

        public ColumnsRepository(ITablesRepository tablesRepository, IConnectionFactory connectionFactory, IDatabasesRepository databasesRepository)
        {
            _connectionFactory = connectionFactory;
            _tablesRepository = tablesRepository;
            _databasesRepository = databasesRepository;
        }

        public async Task<bool> DeleteAsync(int ID)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_Delete_ColumnById_D";
                var parameters = new DynamicParameters();

                parameters.Add("@ColumnId", ID);

                var result = await connection.QuerySingleAsync<string>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result == "success" ? true : false;
            }
        }

        public async Task<IEnumerable<Columns>> GetAllAsync()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_GetColumnsAll_C";

                var result = await connection.QueryAsync<Columns>(query, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<Columns> GetAsync(int ID)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_GetColumnsById_C";
                var parameters = new DynamicParameters();
                parameters.Add("ColumnId", ID);

                var result = await connection.QuerySingleAsync<Columns>(query, param: parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<bool> InsertAsync(Columns model)
        {
            //Validar Si existe la tabla
            var resp = await ExistColumnDocAsync(model.TableId, model!.ColumnName!);

            //Si es verdadero es porque existe la tabla en la base de datos
            if (resp)
            {
                return false;
            }

            using (var connection = _connectionFactory.GetConnection)
            {
                //var query = "PROD_Insertar_estructura_tabla_I";
                var query = "PROD_Insertar_Column_I";
                var parameters = new DynamicParameters();

                parameters.Add("TableId", model.TableId);
                parameters.Add("ColumnName", model.ColumnName);
                parameters.Add("DataType", model.DataType);
                parameters.Add("Size", model.Size);
                parameters.Add("IsNullable", model.IsNullable);
                parameters.Add("IsPrimaryKey", model.IsPrimaryKey);
                parameters.Add("IsForeignKey", model.IsForeignKey);
                parameters.Add("Description", model.Description);
                parameters.Add("UsersId", model.UsersId);

                //Persistir la info en la bd
                var result = await connection.QuerySingleAsync<string>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result == "success" ? true : false;
            }
        }

        public async Task<bool> UpdateAsync(Columns model)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_Update_Columns_U";
                var parameters = new DynamicParameters();

                parameters.Add("ColumnId", model.ColumnId);
                parameters.Add("TableId", model.TableId);
                parameters.Add("ColumnName", model.ColumnName);
                parameters.Add("DataType", model.DataType);
                parameters.Add("Size", model.Size);
                parameters.Add("IsNullable", model.IsNullable);
                parameters.Add("IsPrimaryKey", model.IsPrimaryKey);
                parameters.Add("IsForeignKey", model.IsForeignKey);
                parameters.Add("Description", model.Description);
                parameters.Add("UsersId", model.UsersId);

                //Persistir la info en la bd
                var result = await connection.QuerySingleAsync<string>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result == "success" ? true : false;
            }
        }

        public async Task<bool> ExistColumnDocAsync(int TableId, string ColumnName)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_Exists_Column_C";
                var parameters = new DynamicParameters();
                parameters.Add("TableId", TableId);
                parameters.Add("ColumnName", ColumnName);

                var result = await connection.QuerySingleAsync<Columns>(query, param: parameters, commandType: CommandType.StoredProcedure);

                return result == null ? false : true;
            }
        }

        public async Task<bool> MassColumnsRegistrationAsync(int DatabaseId, int UserId)
        {
            IEnumerable<Tables> tableDB;
            IEnumerable<Columns> columns;
            List<Columns> liColumns = new List<Columns>();
            string jsonColumns = string.Empty;

            var respDatabase = await _databasesRepository.GetAsync(DatabaseId);
            if (respDatabase == null)
            {
                return false;
            }

            tableDB = await _tablesRepository.GetTablesByDataBaseId(DatabaseId);

            using (var connection = _connectionFactory.GetConnectionWithString(respDatabase.ConnectionString!))
            {
                try
                {
                    foreach (var item in tableDB)
                    {
                        #region Consultando columnas por tablas
                        string strSQL = @"SELECT                                                     
                                                    c.COLUMN_NAME AS ColumnName,
                                                    c.DATA_TYPE AS DataType,
                                                    ISNULL(NULLIF(CAST(c.CHARACTER_MAXIMUM_LENGTH AS NVARCHAR(10)), ''), '') AS Size,
                                                    CASE WHEN c.IS_NULLABLE = 'YES' THEN 1 ELSE 0 END AS IsNullable,
                                                    CASE WHEN pk.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END AS IsPrimaryKey,
                                                    CASE WHEN fk.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END AS IsForeignKey,
                                                    ISNULL(CAST(ep.value AS NVARCHAR(MAX)), '') AS Description,
                                                    @UserId AS UserId
                                                FROM INFORMATION_SCHEMA.COLUMNS c
                                                LEFT JOIN (
                                                    SELECT ku.TABLE_CATALOG, ku.TABLE_SCHEMA, ku.TABLE_NAME, ku.COLUMN_NAME
                                                    FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS tc
                                                    INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ku 
                                                        ON tc.CONSTRAINT_TYPE = 'PRIMARY KEY' 
                                                        AND tc.CONSTRAINT_NAME = ku.CONSTRAINT_NAME
                                                ) pk ON c.TABLE_CATALOG = pk.TABLE_CATALOG 
                                                    AND c.TABLE_SCHEMA = pk.TABLE_SCHEMA 
                                                    AND c.TABLE_NAME = pk.TABLE_NAME 
                                                    AND c.COLUMN_NAME = pk.COLUMN_NAME
                                                LEFT JOIN (
                                                    SELECT ku.TABLE_CATALOG, ku.TABLE_SCHEMA, ku.TABLE_NAME, ku.COLUMN_NAME
                                                    FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS tc
                                                    INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ku 
                                                        ON tc.CONSTRAINT_TYPE = 'FOREIGN KEY' 
                                                        AND tc.CONSTRAINT_NAME = ku.CONSTRAINT_NAME
                                                ) fk ON c.TABLE_CATALOG = fk.TABLE_CATALOG 
                                                    AND c.TABLE_SCHEMA = fk.TABLE_SCHEMA 
                                                    AND c.TABLE_NAME = fk.TABLE_NAME 
                                                    AND c.COLUMN_NAME = fk.COLUMN_NAME
                                                LEFT JOIN sys.extended_properties ep ON ep.major_id = OBJECT_ID(@TableName)
                                                    AND ep.minor_id = 0
                                                    AND ep.class = 1
                                                    AND ep.name = 'MS_Description'
                                                WHERE c.TABLE_NAME = @TableName
                                                ORDER BY c.ORDINAL_POSITION;


                            ";
                        #endregion
                        
                        var parametersColumns = new DynamicParameters();
                        parametersColumns.Add("@TableName", item.TableName);
                        parametersColumns.Add("UserId", UserId);

                        columns = await connection.QueryAsync<Columns>(strSQL, param: parametersColumns);

                        foreach (var c in columns)
                        {
                            c.TableId = item.TableId;
                            c.UsersId = UserId;
                            liColumns.Add(c);
                        }
                    }

                    jsonColumns = JsonConvert.SerializeObject(liColumns, Newtonsoft.Json.Formatting.Indented);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_Registro_Campos_Por_tablas_json_I";
                var parameters = new DynamicParameters();

                parameters.Add("json", jsonColumns);
                parameters.Add("DatabaseId", DatabaseId);
                parameters.Add("UserId", UserId);

                //Persistir la info en la bd
                var result = await connection.QuerySingleAsync<string>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result == "success" ? true : false;
            }
        }
    }
}
