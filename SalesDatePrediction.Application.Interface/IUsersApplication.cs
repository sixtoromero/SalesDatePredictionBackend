using SalesDatePrediction.Application.DTO;
using SalesDatePrediction.Transversal.Common;

namespace SalesDatePrediction.Application.Interface
{
    public interface IUsersApplication : IApplication<UsersDTO>
    {
        Task<Response<UsersDTO>> Authenticate(string username, string password);
    }
}
