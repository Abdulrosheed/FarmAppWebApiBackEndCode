using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Interfaces.Repositories;
using FirstProject.Interfaces.Services;

namespace FirstProject.Implementation.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<IEnumerable<UserDto>>> GetAllUserAsync()
        {
            var users = await _userRepository.GetAllUserAsync();
            if(users == null)
            {
                return new BaseResponse<IEnumerable<UserDto>>
                {
                    Message = "No user has been created",
                    IsSucess = false
                };
            }
            return new BaseResponse<IEnumerable<UserDto>>
            {
                Message = "Users  Sucessfully Retrieved",
                IsSucess = true,
                Data = await _userRepository.GetAllUserAsync()
            };
        }

        public async Task<BaseResponse<UserDto>> GetUserAsync(int id)
        {
            var user = await _userRepository.GetUserReturningUserDtoObjectAsync(id);
            if(user == null)
            {
                return new BaseResponse<UserDto>
                {
                    IsSucess = false,
                    Message = "User  Not Found"
                };
            }
            return new BaseResponse<UserDto>
            {
                Message = "Role Sucessfully Retrieved",
                IsSucess = true,
                Data = user
            };
        }

        public async Task<BaseResponse<UserDto>> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailReturningUserDtoObjectAsync(email);
            if(user == null)
            {
                return new BaseResponse<UserDto>
                {
                    Message = "User not found",
                    IsSucess = false,
                };
            }
            return new BaseResponse<UserDto>
            {
                Message = "User Sucessfully Retrieved",
                IsSucess = true,
                Data = user
            };
        }

        public async Task<BaseResponse<UserDto>> LoginAsync(LoginRequestModel model)
        {
            var user = await _userRepository.GetUserByEmailReturningUserDtoObjectAsync(model.Email);
            var userInfo = await _userRepository.GetUserReturningUserObjectAsync(user.Id);
            if(!(await _userRepository.ExistsByEmailAsync(model.Email) && await _userRepository.ExistsByPassWordAsync(model.PassWord)) )
            {
                return new BaseResponse<UserDto>
                {
                    IsSucess = false,
                    Message = "Email or PassWord doesn't exist",

                };
            }
            if(userInfo.PassWord != model.PassWord)
            {
                return new BaseResponse<UserDto>
                {
                    IsSucess = false,
                    Message = "Email or PassWord doesn't match",

                };
            }
            
            return new BaseResponse<UserDto>
            {
                IsSucess = true,
                Message = "User sucessfully retrieved",
                Data = user,
                 
            };
            
        }
    }
}