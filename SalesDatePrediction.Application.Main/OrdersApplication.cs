using AutoMapper;
using SalesDatePrediction.Application.DTO;
using SalesDatePrediction.Application.Interface;
using SalesDatePrediction.Domain.Interface;
using SalesDatePrediction.Transversal.Common;

namespace SalesDatePrediction.Application.Main
{
    public class OrdersApplication : IOrdersApplication
    {

        private readonly IOrdersDomain _Domain;
        private readonly IMapper _mapper;
        private readonly IAppLogger<IOrdersDomain> _logger;

        public OrdersApplication(IOrdersDomain _Domain, IMapper mapper, IAppLogger<IOrdersDomain> logger)
        {
            this._Domain = _Domain;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<IEnumerable<OrdersDTO>>> GetClientOrders(int Custid)
        {
            var response = new Response<IEnumerable<OrdersDTO>>();

            try
            {
                var resp = await _Domain.GetClientOrders(Custid);

                response.Data = _mapper.Map<IEnumerable<OrdersDTO>>(resp);
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

        public Task<Response<IEnumerable<OrdersDTO>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<OrdersDTO>> GetAsync(int ID)
        {
            throw new NotImplementedException();
        }
        
        public Task<Response<bool>> InsertAsync(OrdersDTO modelDto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> UpdateAsync(OrdersDTO modelDto)
        {
            throw new NotImplementedException();
        }
    }
}
