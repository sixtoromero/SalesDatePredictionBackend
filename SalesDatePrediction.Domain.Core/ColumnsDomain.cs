using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.Domain.Interface;
using SalesDatePrediction.InfraStructure.Interface;
using Microsoft.Extensions.Configuration;

namespace SalesDatePrediction.Domain.Core
{
    public class ColumnsDomain : IColumnsDomain
    {
        private readonly IColumnsRepository _Repository;
        public IConfiguration Configuration { get; }

        public ColumnsDomain(IColumnsRepository Repository, IConfiguration _configuration)
        {
            _Repository = Repository;
            Configuration = _configuration;
        }

        public async Task<bool> InsertAsync(Columns model)
        {
            return await _Repository.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(Columns model)
        {
            return await _Repository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int ID)
        {
            return await _Repository.DeleteAsync(ID);
        }

        public async Task<Columns> GetAsync(int ID)
        {
            return await _Repository.GetAsync(ID);
        }

        public async Task<IEnumerable<Columns>> GetAllAsync()
        {
            return await _Repository.GetAllAsync();
        }

        public async Task<bool> MassColumnsRegistrationAsync(int TableId, int UserId)
        {
            return await _Repository.MassColumnsRegistrationAsync(TableId, UserId);
        }
    }
}
