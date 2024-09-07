using SalesDatePrediction.Domain.Entity;

namespace SalesDatePrediction.InfraStructure.Interface
{
    public interface IConstraintsRepository : IRepository<Constraints>
    {
        Task<bool> MassConstraintsRegistrationAsync(int TableId, int UserId);
    }
}
