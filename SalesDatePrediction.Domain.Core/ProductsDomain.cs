using Microsoft.Extensions.Configuration;
using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.Domain.Interface;
using SalesDatePrediction.InfraStructure.Interface;

namespace SalesDatePrediction.Domain.Core
{
    public class ProductsDomain : IProductsDomain
    {

        private readonly IProductsRepository _Repository;
        public IConfiguration Configuration { get; }

        public ProductsDomain(IProductsRepository Repository, IConfiguration _configuration)
        {
            _Repository = Repository;
            Configuration = _configuration;
        }

        public async Task<IEnumerable<Products>> GetAllAsync()
        {
            return await _Repository.GetAllAsync();
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
