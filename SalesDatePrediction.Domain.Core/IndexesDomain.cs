using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.Domain.Interface;
using SalesDatePrediction.InfraStructure.Interface;
using Microsoft.Extensions.Configuration;

namespace SalesDatePrediction.Domain.Core
{
    public class IndexesDomain : IIndexesDomain
    {
        private readonly IIndexesRepository _Repository;
        public IConfiguration Configuration { get; }

        public IndexesDomain(IIndexesRepository Repository, IConfiguration _configuration)
        {
            _Repository = Repository;
            Configuration = _configuration;
        }

        public async Task<bool> InsertAsync(Indexes model)
        {
            return await _Repository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(Indexes model)
        {
            return await _Repository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int ID)
        {
            return await _Repository.DeleteAsync(ID);
        }

        public async Task<Indexes> GetAsync(int ID)
        {
            return await _Repository.GetAsync(ID);
        }

        public async Task<IEnumerable<Indexes>> GetAllAsync()
        {
            return await _Repository.GetAllAsync();
        }

        public async Task<bool> MassIndexesRegistrationAsync(int TableId, int UserId)
        {
            return await _Repository.MassIndexesRegistrationAsync(TableId, UserId);
        }

    }
}
