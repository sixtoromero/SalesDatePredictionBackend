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
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersApplication _Application;
        private readonly IConfiguration _config;
        private readonly AppSettings _appSettings;

        public OrdersController(IOrdersApplication _Application,
                                  IOptions<AppSettings> appSettings, IConfiguration config)
        {
            this._Application = _Application;
            _appSettings = appSettings.Value;
            _config = config;
        }

        [HttpGet("GetClientOrders")]
        public async Task<IActionResult> GetClientOrders(int Custid)
        {
            Response<IEnumerable<OrdersDTO>> response = new Response<IEnumerable<OrdersDTO>>();

            try
            {
                response = await _Application.GetClientOrders(Custid);
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

        [Produces("application/json")]
        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync(OrdersDTO modelDto)
        {
            Response<bool> response = new Response<bool>();
            
            try
            {
                if (modelDto == null)
                    return BadRequest();

                response = await _Application.InsertAsync(modelDto);
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
                response.Data = false;
                response.IsSuccess = false;
                response.Message = ex.Message;

                return BadRequest(response);
            }
        }

    }
}
