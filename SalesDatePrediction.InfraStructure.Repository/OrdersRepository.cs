using Dapper;
using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.InfraStructure.Interface;
using SalesDatePrediction.Transversal.Common;
using System.Data;
using System.Text.Json;

namespace SalesDatePrediction.InfraStructure.Repository
{
    public class OrdersRepository : IOrdersRepository
    {

        private readonly IConnectionFactory _connectionFactory;
        public OrdersRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Orders>> GetClientOrders(int Custid)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "UspGetClientOrders";
                var parameters = new DynamicParameters();
                parameters.Add("custid", Custid);

                var result = await connection.QueryAsync<Orders>(query, param: parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<bool> InsertAsync(Orders model)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                try
                {
                    var query = "uspAddNewOrder";
                    var parameters = new DynamicParameters();

                    string jsonOrderDetail = JsonSerializer.Serialize(model.OrdersDetail);

                    parameters.Add("@empid", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    parameters.Add("@shipperid", model.shipperid);
                    parameters.Add("@shipname", model.shipname);
                    parameters.Add("@shipaddress", model.shipaddress);
                    parameters.Add("@shipcity", model.shipcity);
                    parameters.Add("@orderdate", model.orderdate);
                    parameters.Add("@requireddate", model.requireddate);
                    parameters.Add("@shippeddate", model.shippeddate);
                    parameters.Add("@freight", model.freight);
                    parameters.Add("@shipcountry", model.shipcountry);
                    parameters.Add("@OrderDetailsJSON", jsonOrderDetail);

                    await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);
                    
                    int orderid = parameters.Get<int>("@orderid");

                    return orderid > 0;

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        public Task<bool> UpdateAsync(Orders model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Orders>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Orders> GetAsync(int ID)
        {
            throw new NotImplementedException();
        }

        
    }
}
