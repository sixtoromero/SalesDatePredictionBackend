using SalesDatePrediction.Application.DTO;
using SalesDatePrediction.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDatePrediction.Application.Interface
{
    public interface IColumnsApplication : IApplication<ColumnsDTO>
    {
        Task<Response<bool>> MassColumnsRegistrationAsync(int TableId, int UserId);
    }
}
