using AIRIS.DocBD.Application.DTO;
using AIRIS.DocBD.Presentation.UI.Pages;
using AIRIS.DocBD.Transversal.Common;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace AIRIS.DocBD.Presentation.UI.Auth
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorageService;

        public AuthService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorageService = localStorageService;
        }

        public async Task<string> Login(LoginModel model)
        {
            var loginAsJson = JsonSerializer.Serialize(model);
            
            var response = await _httpClient.PostAsync("/api/Users/AutenticationAsync", 
                    new StringContent(loginAsJson, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                return null!;
            }

            var loginResult = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            Response<UsersDTO>? resp = JsonSerializer.Deserialize<Response<UsersDTO>>(loginResult, options);

            if (resp!.IsSuccess)
            {
                await _localStorageService.SetItemAsStringAsync("authToken", resp!.Data!.Token!);
                ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(model.Username!);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", resp!.Data!.Token!);
            }
            else
            {
                loginResult = null;
            }
           
            return loginResult!;
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<string> Register(UsersDTO model)
        {
            var registerAsJson = JsonSerializer.Serialize(model);

            var response = await _httpClient.PostAsync("/api/Databases/InsertAsync", 
                new StringContent(registerAsJson, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode) 
            {
                return null;
            }

            var registerResult = await response.Content.ReadAsStringAsync();
            return registerResult;
        }
    }
}
