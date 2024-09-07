using SalesDatePrediction.Domain.Entity;

namespace SalesDatePrediction.InfraStructure.Interface
{
    public interface ICustomersRepository : IRepository<Customers>
    {
        Task<IEnumerable<dynamic>> SalesDatePrediction();
    }
}
