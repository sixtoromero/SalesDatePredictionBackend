using SalesDatePrediction.Application.DTO;
using SalesDatePrediction.Transversal.Common;

namespace SalesDatePrediction.Application.Interface
{
    public interface IIndexesApplication : IApplication<IndexesDTO>
    {
        Task<Response<bool>> MassIndexesRegistrationAsync(int DatabaseId, int UserId);
    }
}
