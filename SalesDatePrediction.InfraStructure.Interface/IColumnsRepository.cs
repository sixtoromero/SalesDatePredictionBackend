using SalesDatePrediction.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.InfraStructure.Interface
{
    public interface IColumnsRepository : IRepository<Columns>
    {
        Task<bool> MassColumnsRegistrationAsync(int DatabaseId, int UserId);        
    }
}
