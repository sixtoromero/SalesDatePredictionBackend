using SalesDatePrediction.Application.DTO;
using SalesDatePrediction.Application.Interface;
using SalesDatePrediction.Domain.Interface;
using SalesDatePrediction.Transversal.Common;
using AutoMapper;

namespace SalesDatePrediction.Application.Main
{
    public class UsersApplication : IUsersApplication
    {
        private readonly IUsersDomain _usersDomain;
        private readonly IMapper _mapper;        

        public UsersApplication(IUsersDomain usersDomain, IMapper iMapper)
        {
            _usersDomain = usersDomain;
            _mapper = iMapper;            
        }
        public async Task<Response<UsersDTO>> Authenticate(string username, string password)
        {
            var response = new Response<UsersDTO>();

            try
            {
                var user = await _usersDomain.Authenticate(username, password);
                response.Data = _mapper.Map<UsersDTO>(user);
                response.IsSuccess = true;
                response.Message = "Autenticación Exitosa!!!";
            }
            catch (InvalidOperationException ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            catch (Exception e)
            {
                response.Message = e.Message;                
            }

            return response;
        }

        public Task<Response<bool>> DeleteAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<Response<IEnumerable<UsersDTO>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<UsersDTO>> GetAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> InsertAsync(UsersDTO modelDto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> UpdateAsync(UsersDTO modelDto)
        {
            throw new NotImplementedException();
        }
    }
}
