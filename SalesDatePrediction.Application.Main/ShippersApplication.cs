using AutoMapper;
using SalesDatePrediction.Application.DTO;
using SalesDatePrediction.Application.Interface;
using SalesDatePrediction.Domain.Interface;
using SalesDatePrediction.Transversal.Common;

namespace SalesDatePrediction.Application.Main
{
    public class ShippersApplication : IShippersApplication
    {
        private readonly IShippersDomain _Domain;
        private readonly IMapper _mapper;
        private readonly IAppLogger<IShippersDomain> _logger;

        public ShippersApplication(IShippersDomain _Domain, IMapper mapper, IAppLogger<IShippersDomain> logger)
        {
            this._Domain = _Domain;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<IEnumerable<ShippersDTO>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<ShippersDTO>>();

            try
            {
                var resp = await _Domain.GetAllAsync();

                response.Data = _mapper.Map<IEnumerable<ShippersDTO>>(resp);
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

        public Task<Response<ShippersDTO>> GetAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> InsertAsync(ShippersDTO modelDto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> UpdateAsync(ShippersDTO modelDto)
        {
            throw new NotImplementedException();
        }
    }
}
