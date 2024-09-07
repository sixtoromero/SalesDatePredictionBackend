using SalesDatePrediction.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Domain.Interface
{
    public interface ICustomersDomain : IDomain<Customers>
    {
        Task<IEnumerable<dynamic>> SalesDatePrediction();
    }
}
