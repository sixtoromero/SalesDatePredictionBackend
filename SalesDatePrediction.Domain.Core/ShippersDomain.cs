using Microsoft.Extensions.Configuration;
using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.Domain.Interface;
using SalesDatePrediction.InfraStructure.Interface;

namespace SalesDatePrediction.Domain.Core
{
    public class ShippersDomain : IShippersDomain
    {
        private readonly IShippersRepository _Repository;
        public IConfiguration Configuration { get; }

        public ShippersDomain(IShippersRepository Repository, IConfiguration _configuration)
        {
            _Repository = Repository;
            Configuration = _configuration;
        }

        public async Task<IEnumerable<Shippers>> GetAllAsync()
        {
            return await _Repository.GetAllAsync();
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
