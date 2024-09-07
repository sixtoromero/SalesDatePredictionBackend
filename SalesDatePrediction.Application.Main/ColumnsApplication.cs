using SalesDatePrediction.Application.DTO;
using SalesDatePrediction.Application.Interface;
using SalesDatePrediction.Domain.Entity;
using SalesDatePrediction.Domain.Interface;
using SalesDatePrediction.Transversal.Common;
using AutoMapper;

namespace SalesDatePrediction.Application.Main
{
    public class ColumnsApplication : IColumnsApplication
    {
        private readonly IColumnsDomain _Domain;
        private readonly IMapper _mapper;
        private readonly IAppLogger<ColumnsApplication> _logger;

        public ColumnsApplication(IColumnsDomain domain, IMapper iMapper, IAppLogger<ColumnsApplication> logger)
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

        public async Task<Response<IEnumerable<ColumnsDTO>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<ColumnsDTO>>();

            try
            {
                var resp = await _Domain.GetAllAsync();

                response.Data = _mapper.Map<IEnumerable<ColumnsDTO>>(resp);
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

        public async Task<Response<ColumnsDTO>> GetAsync(int ID)
        {
            var response = new Response<ColumnsDTO>();

            try
            {
                var resp = await _Domain.GetAsync(ID);

                response.Data = _mapper.Map<ColumnsDTO>(resp);
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

        public async Task<Response<bool>> InsertAsync(ColumnsDTO modelDto)
        {
            var response = new Response<bool>();
            try
            {
                var resp = _mapper.Map<Columns>(modelDto);
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

        public async Task<Response<bool>> UpdateAsync(ColumnsDTO modelDto)
        {
            var response = new Response<bool>();
            try
            {
                var resp = _mapper.Map<Columns>(modelDto);
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

        public async Task<Response<bool>> MassColumnsRegistrationAsync(int TableId, int UserId)
        {
            var response = new Response<bool>();
            try
            {
                response.Data = await _Domain.MassColumnsRegistrationAsync(TableId, UserId);

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
