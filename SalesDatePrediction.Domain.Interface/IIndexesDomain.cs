using SalesDatePrediction.Domain.Entity;

namespace SalesDatePrediction.Domain.Interface
{
    public interface IIndexesDomain : IDomain<Indexes>
    {
        Task<bool> MassIndexesRegistrationAsync(int TableId, int UserId);
    }
}
