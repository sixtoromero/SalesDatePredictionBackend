using AutoMapper;
using SalesDatePrediction.Application.DTO;
using SalesDatePrediction.Application.Interface;
using SalesDatePrediction.Domain.Interface;
using SalesDatePrediction.Transversal.Common;

namespace SalesDatePrediction.Application.Main
{
    public class CustomersApplication : ICustomersApplication
    {
        private readonly ICustomersDomain _Domain;
        private readonly IMapper _mapper;
        private readonly IAppLogger<CustomersApplication> _logger;

        public CustomersApplication(ICustomersDomain _Domain, IMapper mapper, IAppLogger<CustomersApplication> logger)
        {
            this._Domain = _Domain;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<IEnumerable<dynamic>>> SalesDatePrediction()
        {
            var response = new Response<IEnumerable<dynamic>>();
            try
            {
                var resp = await _Domain.SalesDatePrediction();

                //response.Data = _mapper.Map<IEnumerable<ClienteDTO>>(resp);
                response.Data = resp;
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

        public Task<Response<IEnumerable<CustomersDTO>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<CustomersDTO>> GetAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> InsertAsync(CustomersDTO modelDto)
        {
            throw new NotImplementedException();
        }        

        public Task<Response<bool>> UpdateAsync(CustomersDTO modelDto)
        {
            throw new NotImplementedException();
        }
    }
}
