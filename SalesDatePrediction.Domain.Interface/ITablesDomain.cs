using SalesDatePrediction.Domain.Entity;

namespace SalesDatePrediction.Domain.Interface
{
    public interface ITablesDomain : IDomain<Tables>
    {
        Task<bool> MassTableRegistrationAsync(int DatabaseId, int UserId);
    }
}
