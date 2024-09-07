using SalesDatePrediction.Application.DTO;
using SalesDatePrediction.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Application.Interface
{
    public interface ICustomersApplication : IApplication<CustomersDTO>
    {
        Task<Response<IEnumerable<dynamic>>> SalesDatePrediction();
    }
}
