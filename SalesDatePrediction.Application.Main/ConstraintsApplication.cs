using SalesDatePrediction.Application.DTO;
using SalesDatePrediction.Application.Interface;
using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.Domain.Interface;
using SalesDatePrediction.Transversal.Common;
using AutoMapper;

namespace SalesDatePrediction.Application.Main
{
    public class ConstraintsApplication : IConstraintsApplication
    {
        private readonly IConstraintsDomain _Domain;
        private readonly IMapper _mapper;
        private readonly IAppLogger<ConstraintsApplication> _logger;

        public ConstraintsApplication(IConstraintsDomain domain, IMapper iMapper, IAppLogger<ConstraintsApplication> logger)
        {
            _Domain = domain;
            _mapper = iMapper;
            _logger = logger;
        }

        public async Task<Response<bool>> DeleteAsync(int ID)
        {
            var response = new Response<bool>();

            try
            {
                response.Data = await _Domain.DeleteAsync(ID);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Eliminación Exitosa!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                _logger.LogError(ex.Message);
            }

            return response;
        }

        public async Task<Response<IEnumerable<ConstraintsDTO>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<ConstraintsDTO>>();

            try
            {
                var resp = await _Domain.GetAllAsync();

                response.Data = _mapper.Map<IEnumerable<ConstraintsDTO>>(resp);
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

        public async Task<Response<ConstraintsDTO>> GetAsync(int ID)
        {
            var response = new Response<ConstraintsDTO>();

            try
            {
                var resp = await _Domain.GetAsync(ID);

                response.Data = _mapper.Map<ConstraintsDTO>(resp);
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

        public async Task<Response<bool>> InsertAsync(ConstraintsDTO modelDto)
        {
            var response = new Response<bool>();
            try
            {
                var resp = _mapper.Map<Constraints>(modelDto);
                response.Data = await _Domain.InsertAsync(resp);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Exitoso!";
                }
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.IsSuccess = false;
                response.Message = ex.Message;

                _logger.LogError(ex.Message);
            }

            return response;
        }

        public async Task<Response<bool>> UpdateAsync(ConstraintsDTO modelDto)
        {
            var response = new Response<bool>();
            try
            {
                var resp = _mapper.Map<Constraints>(modelDto);
                response.Data = await _Domain.UpdateAsync(resp);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Actualización Exitosa!";
                }
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.IsSuccess = false;
                response.Message = ex.Message;

                _logger.LogError(ex.Message);
            }

            return response;
        }

        public async Task<Response<bool>> MassConstraintsRegistrationAsync(int TableId, int UserId)
        {
            var response = new Response<bool>();
            try
            {
                response.Data = await _Domain.MassConstraintsRegistrationAsync(TableId, UserId);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Exitoso!";
                }
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.IsSuccess = false;
                response.Message = ex.Message;

                _logger.LogError(ex.Message);
            }

            return response;
        }


    }
}
