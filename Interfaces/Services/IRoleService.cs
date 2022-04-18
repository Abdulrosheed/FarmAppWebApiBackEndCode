using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;

namespace FirstProject.Interfaces.Services
{
    public interface IRoleService
    {
        Task<BaseResponse<RoleDto>> RegisterAsync (CreateRoleRequestModel model);
        Task<BaseResponse<RoleDto>> UpdateAsync (UpdateRoleRequestModel model , int id);
        Task<BaseResponse<RoleDto>> DeleteAsync (int id);
        Task<BaseResponse<IEnumerable<RoleDto>>> GetAllRolesAsync ();
        Task<BaseResponse<RoleDto>> GetRoleAsync (int id);
    }
}