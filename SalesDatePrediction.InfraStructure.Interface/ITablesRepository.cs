using SalesDatePrediction.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.InfraStructure.Interface
{
    public interface ITablesRepository : IRepository<Tables>
    {
        //Task<bool> RegisterTableDocAsync(Tables model);
        Task<bool> ExistTableDocAsync(int DatabaseId, string tableName);
        Task<bool> MassTableRegistrationAsync(int DatabaseId, int UserId);
        Task<IEnumerable<Tables>> GetTablesByDataBaseId(int DatabaseId);
    }
}
