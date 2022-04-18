using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;

namespace FirstProject.Interfaces.Services
{
    public interface IUserService
    {
        Task<BaseResponse<IEnumerable<UserDto>>> GetAllUserAsync ();
        Task<BaseResponse<UserDto>> GetUserAsync (int id);
        Task<BaseResponse<UserDto>> GetUserByEmailAsync (string email);
        Task<BaseResponse<UserDto>> LoginAsync (LoginRequestModel model);
    }
}