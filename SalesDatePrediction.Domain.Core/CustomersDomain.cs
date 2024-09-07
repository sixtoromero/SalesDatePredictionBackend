using Microsoft.Extensions.Configuration;
using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.Domain.Interface;
using SalesDatePrediction.InfraStructure.Interface;

namespace SalesDatePrediction.Domain.Core
{
    public class CustomersDomain : ICustomersDomain
    {
        private readonly ICustomersRepository _Repository;
        public IConfiguration Configuration { get; }

        public CustomersDomain(ICustomersRepository Repository, IConfiguration _configuration)
        {
            _Repository = Repository;
            Configuration = _configuration;
        }

        public async Task<IEnumerable<dynamic>> SalesDatePrediction()
        {
            return await _Repository.SalesDatePrediction();
        }

        public Task<bool> DeleteAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customers>> GetAllAsync()
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
    }
}
