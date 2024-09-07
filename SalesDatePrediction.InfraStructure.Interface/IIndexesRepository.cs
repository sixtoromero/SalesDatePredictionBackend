using SalesDatePrediction.Domain.Entity;

namespace SalesDatePrediction.InfraStructure.Interface
{
    public interface IIndexesRepository : IRepository<Indexes>
    {
        Task<bool> MassIndexesRegistrationAsync(int DatabaseId, int UserId);
    }
}
