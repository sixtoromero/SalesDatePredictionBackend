using AIRIS.DocBD.Application.DTO;

namespace AIRIS.DocBD.Presentation.UI.Auth
{
    public interface IAuthService
    {
        Task<string> Register(UsersDTO model);
        Task<string> Login(LoginModel model);
        Task Logout();
    }
}
