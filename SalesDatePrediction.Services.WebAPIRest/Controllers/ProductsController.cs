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
    public class ProductsController : ControllerBase
    {
        private readonly IProductsApplication _Application;
        private readonly IConfiguration _config;
        private readonly AppSettings _appSettings;

        public ProductsController(IProductsApplication _Application,
                                  IOptions<AppSettings> appSettings, IConfiguration config)
        {
            this._Application = _Application;
            _appSettings = appSettings.Value;
            _config = config;
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            Response<IEnumerable<ProductsDTO>> response = new Response<IEnumerable<ProductsDTO>>();

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
