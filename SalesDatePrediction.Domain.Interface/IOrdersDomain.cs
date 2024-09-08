using SalesDatePrediction.Domain.Entity;

namespace SalesDatePrediction.Domain.Interface
{
    public interface IOrdersDomain : IDomain<Orders>
    {
        Task<IEnumerable<Orders>> GetClientOrders(int Custid);
    }
}
