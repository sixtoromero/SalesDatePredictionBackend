using SalesDatePrediction.Transversal.Common;

namespace SalesDatePrediction.Application.Interface
{
    public interface IApplication<T>
    {
        Task<Response<bool>> InsertAsync(T modelDto);
        Task<Response<bool>> UpdateAsync(T modelDto);
        Task<Response<bool>> DeleteAsync(int ID);
        Task<Response<T>> GetAsync(int ID);
        Task<Response<IEnumerable<T>>> GetAllAsync();
    }
}
