using SalesDatePrediction.Application.DTO;
using SalesDatePrediction.Transversal.Common;

namespace SalesDatePrediction.Application.Interface
{
    public interface IConstraintsApplication : IApplication<ConstraintsDTO>
    {
        Task<Response<bool>> MassConstraintsRegistrationAsync(int TableId, int UserId);
    }
}
