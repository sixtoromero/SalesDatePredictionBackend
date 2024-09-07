using SalesDatePrediction.Domain.Entity;

namespace SalesDatePrediction.Domain.Interface
{
    public interface IConstraintsDomain : IDomain<Constraints>
    {
        Task<bool> MassConstraintsRegistrationAsync(int TableId, int UserId);
    }
}
