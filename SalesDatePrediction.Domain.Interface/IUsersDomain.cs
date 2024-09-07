using SalesDatePrediction.Domain.Entity;

namespace SalesDatePrediction.Domain.Interface
{
    public interface IUsersDomain
    {
        Task<Users> Authenticate(string username, string password);
    }
}
