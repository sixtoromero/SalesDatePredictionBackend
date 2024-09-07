using Dapper;
using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.InfraStructure.Interface;
using SalesDatePrediction.Transversal.Common;
using System.Data;

namespace SalesDatePrediction.InfraStructure.Repository
{
    public class ShippersRepository : IShippersRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public ShippersRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Shippers>> GetAllAsync()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "UspGetSnippers";

                var result = await connection.QueryAsync<Shippers>(query, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public Task<bool> DeleteAsync(int ID)
        {
            throw new NotImplementedException();
        }
        
        public Task<Shippers> GetAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(Shippers model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Shippers model)
        {
            throw new NotImplementedException();
        }
    }
}
