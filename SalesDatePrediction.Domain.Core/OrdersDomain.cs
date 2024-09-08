using Microsoft.Extensions.Configuration;
using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.Domain.Interface;
using SalesDatePrediction.InfraStructure.Interface;

namespace SalesDatePrediction.Domain.Core
{
    public class OrdersDomain : IOrdersDomain
    {

        private readonly IOrdersRepository _Repository;
        public IConfiguration Configuration { get; }

        public OrdersDomain(IOrdersRepository Repository, IConfiguration _configuration)
        {
            _Repository = Repository;
            Configuration = _configuration;
        }

        public async Task<IEnumerable<Orders>> GetClientOrders(int Custid)
        {
            return await _Repository.GetClientOrders(Custid);
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

        public Task<bool> InsertAsync(Orders model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Orders model)
        {
            throw new NotImplementedException();
        }
    }
}
