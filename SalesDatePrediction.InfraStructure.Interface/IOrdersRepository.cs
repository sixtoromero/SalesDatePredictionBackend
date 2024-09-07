using SalesDatePrediction.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.InfraStructure.Interface
{
    public interface IOrdersRepository : IRepository<Orders>
    {
        Task<IEnumerable<Orders>> GetClientOrders(int Custid);
    }
}
