using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;

namespace FirstProject.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<RoleDto> CreateAsync (Role role);
        Task<RoleDto> UpdateAsync (Role role);
        // void DeleteAsync (Role role);
        Task<IEnumerable<RoleDto>> GetAllRoleAsync ();
        Task<RoleDto> GetRoleReturningRoleDtoObjectAsync (int id);
        Task<Role> GetRoleByNameReturningRoleObjectAsync (string name);
        Task<Role> GetRoleReturningRoleObjectAsync (int id);
        Task<IList<Role>> GetSelectedRoles(List<int>ids);
          Task<bool> ExistsByNameAsync (string name);  
    }
}