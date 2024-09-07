using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.Domain.Interface;
using SalesDatePrediction.InfraStructure.Interface;
using Microsoft.Extensions.Configuration;

namespace SalesDatePrediction.Domain.Core
{
    public class DatabasesDomain : IDatabasesDomain
    {
        private readonly IDatabasesRepository _Repository;
        public IConfiguration Configuration { get; }

        public DatabasesDomain(IDatabasesRepository Repository, IConfiguration _configuration)
        {
            _Repository = Repository;
            Configuration = _configuration;
        }

        public async Task<bool> InsertAsync(Databases model)
        {
            return await _Repository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(Databases model)
        {
            return await _Repository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int ID)
        {
            return await _Repository.DeleteAsync(ID);
        }

        public async Task<Databases> GetAsync(int ID)
        {
            return await _Repository.GetAsync(ID);
        }

        public async Task<IEnumerable<Databases>> GetAllAsync()
        {
            return await _Repository.GetAllAsync();
        }
    }
}
