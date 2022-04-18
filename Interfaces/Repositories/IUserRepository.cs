using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;

namespace FirstProject.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync (User user);
        Task<UserDto> UpdateAsync (User user);
        // void DeleteAsync (User user);
        Task<IEnumerable<UserDto>> GetAllUserAsync ();
        Task<UserDto> GetUserReturningUserDtoObjectAsync (int id);
        Task<UserDto> GetUserByEmailReturningUserDtoObjectAsync (string email);
        Task<User> GetUserReturningUserObjectAsync (int id);
        Task<bool> ExistsByEmailAsync (string email);
        Task<bool> ExistsByPassWordAsync (string passWord);
      
    }
}