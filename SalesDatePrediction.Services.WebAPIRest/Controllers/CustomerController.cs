using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SalesDatePrediction.Application.DTO;
using SalesDatePrediction.Application.Interface;
using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.InfraStructure.Interface;
using SalesDatePrediction.Services.WebAPIRest.Helpers;
using SalesDatePrediction.Transversal.Common;
using System.Data;

namespace SalesDatePrediction.Services.WebAPIRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomersApplication _Application;
        private readonly IConfiguration _config;        
        private readonly AppSettings _appSettings;

        public CustomerController(ICustomersApplication _Application,
                                  IOptions<AppSettings> appSettings, IConfiguration config)
        {
            this._Application = _Application;
            _appSettings = appSettings.Value;
            _config = config;
        }

        [HttpGet("GetSalesDatePrediction")]
        public async Task<IActionResult> GetSalesDatePrediction()
        {
            Response<IEnumerable<dynamic>> response = new Response<IEnumerable<dynamic>>();

            try
            {
                response = await _Application.SalesDatePrediction();
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
