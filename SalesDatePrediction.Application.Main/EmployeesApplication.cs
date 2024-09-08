using AutoMapper;
using SalesDatePrediction.Application.DTO;
using SalesDatePrediction.Application.Interface;
using SalesDatePrediction.Domain.Interface;
using SalesDatePrediction.Transversal.Common;

namespace SalesDatePrediction.Application.Main
{
    public class EmployeesApplication : IEmployeesApplication
    {
        private readonly IEmployeesDomain _Domain;
        private readonly IMapper _mapper;
        private readonly IAppLogger<EmployeesApplication> _logger;

        public EmployeesApplication(IEmployeesDomain _Domain, IMapper mapper, IAppLogger<EmployeesApplication> logger)
        {
            this._Domain = _Domain;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<IEnumerable<EmployeesDTO>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<EmployeesDTO>>();

            try
            {
                var resp = await _Domain.GetAllAsync();

                response.Data = _mapper.Map<IEnumerable<EmployeesDTO>>(resp);
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

        public Task<Response<EmployeesDTO>> GetAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> InsertAsync(EmployeesDTO modelDto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> UpdateAsync(EmployeesDTO modelDto)
        {
            throw new NotImplementedException();
        }
    }
}
