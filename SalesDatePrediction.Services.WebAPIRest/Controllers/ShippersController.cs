using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SalesDatePrediction.Application.DTO;
using SalesDatePrediction.Application.Interface;
using SalesDatePrediction.Services.WebAPIRest.Helpers;
using SalesDatePrediction.Transversal.Common;

namespace SalesDatePrediction.Services.WebAPIRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippersController : ControllerBase
    {
        private readonly IShippersApplication _Application;
        private readonly IConfiguration _config;
        private readonly AppSettings _appSettings;

        public ShippersController(IShippersApplication _Application,
                                  IOptions<AppSettings> appSettings, IConfiguration config)
        {
            this._Application = _Application;
            _appSettings = appSettings.Value;
            _config = config;
        }

        [HttpGet("GetSnippers")]
        public async Task<IActionResult> GetSnippers()
        {
            Response<IEnumerable<ShippersDTO>> response = new Response<IEnumerable<ShippersDTO>>();

            try
            {
                response = await _Application.GetAllAsync();
                if (response.IsSuccess)
                {
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
