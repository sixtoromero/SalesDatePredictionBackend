using Dapper;
using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.InfraStructure.Interface;
using SalesDatePrediction.Transversal.Common;
using System.Data;

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

        public Task<bool> InsertAsync(Orders model)
        {
            throw new NotImplementedException();
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
