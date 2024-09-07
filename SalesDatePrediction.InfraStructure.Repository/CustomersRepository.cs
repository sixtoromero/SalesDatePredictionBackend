using Dapper;
using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.InfraStructure.Interface;
using SalesDatePrediction.Transversal.Common;
using System.Data;

namespace SalesDatePrediction.InfraStructure.Repository
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public CustomersRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<dynamic>> SalesDatePrediction()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "UspgetSalesDatePrediction";
                var result = await connection.QueryAsync<dynamic>(query, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public Task<IEnumerable<Customers>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<Customers> GetAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(Customers model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Customers model)
        {
            throw new NotImplementedException();
        }

        
        //UspgetSalesDatePrediction

    }
}
