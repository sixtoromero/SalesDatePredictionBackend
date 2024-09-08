using Microsoft.Extensions.Configuration;
using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.Domain.Interface;
using SalesDatePrediction.InfraStructure.Interface;

namespace SalesDatePrediction.Domain.Core
{
    public class EmployeesDomain : IEmployeesDomain
    {
        private readonly IEmployeesRepository _Repository;
        public IConfiguration Configuration { get; }

        public EmployeesDomain(IEmployeesRepository Repository, IConfiguration _configuration)
        {
            _Repository = Repository;
            Configuration = _configuration;
        }

        public async Task<IEnumerable<Employees>> GetAllAsync()
        {
            return await _Repository.GetAllAsync();
        }

        public Task<bool> DeleteAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<Employees> GetAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(Employees model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Employees model)
        {
            throw new NotImplementedException();
        }
    }
}
