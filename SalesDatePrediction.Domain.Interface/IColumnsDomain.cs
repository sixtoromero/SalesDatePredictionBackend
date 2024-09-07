using SalesDatePrediction.Domain.Entity;

namespace SalesDatePrediction.Domain.Interface
{
    public interface IColumnsDomain : IDomain<Columns>
    {
        Task<bool> MassColumnsRegistrationAsync(int TableId, int UserId);
    }
}
