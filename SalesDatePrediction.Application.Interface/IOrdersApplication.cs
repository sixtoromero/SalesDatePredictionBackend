using SalesDatePrediction.Application.DTO;
using SalesDatePrediction.Transversal.Common;

namespace SalesDatePrediction.Application.Interface
{
    public interface IOrdersApplication : IApplication<OrdersDTO>
    {
        Task<Response<IEnumerable<OrdersDTO>>> GetClientOrders(int Custid);
    }
}
