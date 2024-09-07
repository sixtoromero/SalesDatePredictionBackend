using SalesDatePrediction.Application.DTO;
using SalesDatePrediction.Application.DTO.Request;
using SalesDatePrediction.Application.Interface;
using SalesDatePrediction.Services.WebAPIRest.Helpers;
using SalesDatePrediction.Transversal.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SalesDatePrediction.Services.WebAPIRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersApplication _Application;
        private readonly AppSettings _appSettings;
        private readonly IWebHostEnvironment env;

        public UsersController(IUsersApplication _Application,
                                  IOptions<AppSettings> appSettings,
                                  IWebHostEnvironment env)
        {
            this._Application = _Application;
            _appSettings = appSettings.Value;
            this.env = env;
        }

        [HttpPost("AutenticationAsync")]
        public async Task<IActionResult> AutenticationAsync(RequestUserDTO request)
        {
            Response<UsersDTO> response = new Response<UsersDTO>();
            try
            {
                response = await _Application.Authenticate(request.Username, request.Password);
                if (response.IsSuccess)
                {
                    BuildToken obtenerToken = new BuildToken();
                    response!.Data!.Token = obtenerToken.GetBuildToken(response.Data, _appSettings);
                    //response.Data.TimeToken = _appSettings.Expires;
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.IsSuccess = false;
                response.Message = ex.Message;

                return BadRequest(response);
            }
        }
    }
}
