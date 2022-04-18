using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;

namespace FirstProject.Interfaces.Repositories
{
    public interface IAdminRepository
    {
        Task<AdminDto> CreateAsync (Admin admin);
        Task<AdminDto> UpdateAsync (Admin admin);
        // void DeleteAsync (Admin admin);
        Task<IEnumerable<AdminDto>> GetAllAdminAsync ();
        Task<AdminDto> GetAdminReturningAdminDtoObjectAsync (int id);
        Task<AdminDto> GetAdminByEmailReturningAdminDtoObjectAsync (string email);
        Task<Admin> GetAdminReturningAdminObjectAsync (int id);
        Task<bool> ExistsByEmailAsync (string email);
       
        Task<bool> ExistsByIdAsync (int id);
    }
}