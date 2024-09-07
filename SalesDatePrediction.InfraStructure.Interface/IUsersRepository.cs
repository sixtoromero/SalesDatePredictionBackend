using SalesDatePrediction.Domain.Entity;

namespace SalesDatePrediction.Infrastructure.Interface
{
    public interface IUsersRepository
    {
        Task<Users> Authenticate(string username, string password);
    }
}
