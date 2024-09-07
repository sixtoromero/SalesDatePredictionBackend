using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.InfraStructure.Interface;
using SalesDatePrediction.Transversal.Common;
using Dapper;
using System.Data;
using System.Net.Http.Json;
using System.Reflection;
using System.Xml;

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections;

namespace SalesDatePrediction.InfraStructure.Repository
{
    public class TablesRepository : ITablesRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IDatabasesRepository _databasesRepository;

        public TablesRepository(IConnectionFactory connectionFactory, IDatabasesRepository databasesRepository)
        {
            _connectionFactory = connectionFactory;
            _databasesRepository = databasesRepository;
        }

        public async Task<bool> InsertAsync(Tables model)
        {
            //Validar Si existe la tabla
            var resp = await ExistTableDocAsync(model.DatabaseId, model!.TableName!);

            //Si es verdadero es porque existe la tabla en la base de datos
            if (resp)
            {
                return false;
            }

            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_Insertar_estructura_tabla_I";
                var parameters = new DynamicParameters();

                parameters.Add("DatabaseId", model.DatabaseId);
                parameters.Add("TableName", model.TableName);
                parameters.Add("Description", model.Description);
                parameters.Add("Status", model.Status);

                //Persistir la info en la bd
                var result = await connection.QuerySingleAsync<string>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result == "success" ? true : false;
            }
        }

        public async Task<bool> UpdateAsync(Tables model)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_Update_estructura_tabla_U";
                var parameters = new DynamicParameters();

                parameters.Add("TableId", model.TableId);
                parameters.Add("DatabaseId", model.TableId);
                parameters.Add("TableName", model.TableName);
                parameters.Add("Description", model.Description);
                parameters.Add("Status", model.Status);

                //Persistir la info en la bd
                var result = await connection.QuerySingleAsync<string>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result == "success" ? true : false;
            }
        }

        public async Task<bool> DeleteAsync(int ID)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_Delete_estructura_tabla_D";
                var parameters = new DynamicParameters();

                parameters.Add("@TableId", ID);

                var result = await connection.QuerySingleAsync<string>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result == "success" ? true : false;
            }
        }

        public async Task<Tables> GetAsync(int ID)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "UspgetTableById";
                var parameters = new DynamicParameters();
                parameters.Add("TableId", ID);

                var result = await connection.QuerySingleAsync<Tables>(query, param: parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<Tables>> GetAllAsync()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "UspgetTabless";

                var result = await connection.QueryAsync<Tables>(query, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<Tables>> GetTablesByDataBaseId(int DatabaseId)
        {
            try
            {
                using (var connection = _connectionFactory.GetConnection)
                {
                    var query = "PROD_GetTablesByDataBaseId_C";

                    var parameters = new DynamicParameters();
                    parameters.Add("DatabaseId", DatabaseId);

                    var result = await connection.QueryAsync<Tables>(query, param: parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }            
        }

        public async Task<bool> RegisterTableDocAsync(Tables model)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_Insertar_doc_tabla_I";
                var parameters = new DynamicParameters();

                parameters.Add("DatabaseId", model.DatabaseId);
                parameters.Add("TableName", model.TableName);
                parameters.Add("Description", model.Description);
                parameters.Add("Status", model.Status);

                //Persistir la info en la bd
                var result = await connection.QuerySingleAsync<string>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result == "success" ? true : false;
            }
        }

        public async Task<bool> ExistTableDocAsync(int DatabaseId, string tableName)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_Exists_Table_C";
                var parameters = new DynamicParameters();
                parameters.Add("DatabaseId", DatabaseId);
                parameters.Add("TableName", tableName);

                var result = await connection.QuerySingleAsync<Tables>(query, param: parameters, commandType: CommandType.StoredProcedure);

                return result == null ? false : true;
            }
        }

        public async Task<bool> MassTableRegistrationAsync(int DatabaseId, int UserId)
        {
            IEnumerable<Tables> tables;            

            string jsonResult = string.Empty;            

            var resp = await _databasesRepository.GetAsync(DatabaseId);
            if (resp == null)
            {
                return false;
            }

            string connectionString = resp.ConnectionString!;

            using (var connection = _connectionFactory.GetConnectionWithString(connectionString))
            {
                try
                {
                    //var query = "PROD_Registro_Tablas_Por_Base_Datos_I";
                    #region Consultamos todas las tablas que tiene la base de datos a leer
                    string query = @"
                    SELECT 
                        @DatabaseId AS DatabaseId,
                        TABLE_SCHEMA AS Scheme, 
                        TABLE_NAME AS TableName, 
                        '' AS Description,
                        1 AS Status,
                        @UserId AS UsersId
                    FROM 
                        INFORMATION_SCHEMA.TABLES t                    
                    ORDER BY 
                        TABLE_SCHEMA, 
                        TABLE_NAME;
                    ";
                    
                    var parameters = new DynamicParameters();
                    parameters.Add("@DatabaseId", DatabaseId);
                    parameters.Add("@UserId", UserId);

                    tables = await connection.QueryAsync<Tables>(query, param: parameters);

                    jsonResult = JsonConvert.SerializeObject(tables, Newtonsoft.Json.Formatting.Indented);
                    #endregion
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            //Registrar el resultado en la tabla correspondiente.
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "PROD_Registro_Tablas_Por_Base_Datos_json_I";
                var parameters = new DynamicParameters();

                parameters.Add("json", jsonResult);
                parameters.Add("DatabaseId", DatabaseId);
                parameters.Add("UserId", UserId);

                //Persistir la info en la bd
                var result = await connection.QuerySingleAsync<string>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result == "success" ? true : false;
            }

            //jsonResult

        }
    }
}
