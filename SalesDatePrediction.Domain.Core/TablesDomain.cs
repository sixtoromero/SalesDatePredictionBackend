using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.Domain.Interface;
using SalesDatePrediction.InfraStructure.Interface;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace SalesDatePrediction.Domain.Core
{
    public class TablesDomain : ITablesDomain
    {
        private readonly ITablesRepository _Repository;
        public IConfiguration Configuration { get; }

        public TablesDomain(ITablesRepository Repository, IConfiguration _configuration)
        {
            _Repository = Repository;
            Configuration = _configuration;
        }

        public async Task<bool> DeleteAsync(int ID)
        {
            return await _Repository.DeleteAsync(ID);
        }

        public async Task<IEnumerable<Tables>> GetAllAsync()
        {
            return await _Repository.GetAllAsync();
        }

        public async Task<Tables> GetAsync(int ID)
        {
            return await _Repository.GetAsync(ID);
        }

        public async Task<bool> InsertAsync(Tables model)
        {
            return await _Repository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(Tables model)
        {
            return await _Repository.UpdateAsync(model);
        }

        public async Task<bool> MassTableRegistrationAsync(int DatabaseId, int UserId)
        {
            return await _Repository.MassTableRegistrationAsync(DatabaseId, UserId);
        }
    }
}
