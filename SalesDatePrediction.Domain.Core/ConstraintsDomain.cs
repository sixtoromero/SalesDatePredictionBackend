using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.Domain.Interface;
using SalesDatePrediction.InfraStructure.Interface;
using Microsoft.Extensions.Configuration;

namespace SalesDatePrediction.Domain.Core
{
    public class ConstraintsDomain : IConstraintsDomain
    {
        private readonly IConstraintsRepository _Repository;
        public IConfiguration Configuration { get; }

        public ConstraintsDomain(IConstraintsRepository Repository, IConfiguration _configuration)
        {
            _Repository = Repository;
            Configuration = _configuration;
        }

        public async Task<bool> InsertAsync(Constraints model)
        {
            return await _Repository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(Constraints model)
        {
            return await _Repository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int ID)
        {
            return await _Repository.DeleteAsync(ID);
        }

        public async Task<Constraints> GetAsync(int ID)
        {
            return await _Repository.GetAsync(ID);
        }

        public async Task<IEnumerable<Constraints>> GetAllAsync()
        {
            return await _Repository.GetAllAsync();
        }

        public async Task<bool> MassConstraintsRegistrationAsync(int TableId, int UserId)
        {
            return await _Repository.MassConstraintsRegistrationAsync(TableId, UserId);
        }

    }
}
