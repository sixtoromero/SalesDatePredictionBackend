using AutoMapper;
using SalesDatePrediction.Application.DTO;
using SalesDatePrediction.Application.Interface;
using SalesDatePrediction.Domain.Interface;
using SalesDatePrediction.Transversal.Common;

namespace SalesDatePrediction.Application.Main
{
    public class ProductsApplication : IProductsApplication
    {
        private readonly IProductsDomain _Domain;
        private readonly IMapper _mapper;
        private readonly IAppLogger<IProductsDomain> _logger;

        public ProductsApplication(IProductsDomain _Domain, IMapper mapper, IAppLogger<IProductsDomain> logger)
        {
            this._Domain = _Domain;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<IEnumerable<ProductsDTO>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<ProductsDTO>>();

            try
            {
                var resp = await _Domain.GetAllAsync();

                response.Data = _mapper.Map<IEnumerable<ProductsDTO>>(resp);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = string.Empty;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                _logger.LogError(ex.Message);
            }
            return response;
        }

        public Task<Response<bool>> DeleteAsync(int ID)
        {
            throw new NotImplementedException();
        }        

        public Task<Response<ProductsDTO>> GetAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> InsertAsync(ProductsDTO modelDto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> UpdateAsync(ProductsDTO modelDto)
        {
            throw new NotImplementedException();
        }
    }
}
