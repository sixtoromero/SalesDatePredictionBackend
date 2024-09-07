using Dapper;
using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.InfraStructure.Interface;
using SalesDatePrediction.Transversal.Common;
using System.Data;

namespace SalesDatePrediction.InfraStructure.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public ProductsRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Products>> GetAllAsync()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "UspGetProducts";

                var result = await connection.QueryAsync<Products>(query, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public Task<bool> DeleteAsync(int ID)
        {
            throw new NotImplementedException();
        }        

        public Task<Products> GetAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(Products model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Products model)
        {
            throw new NotImplementedException();
        }
    }
}
