using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;
using FirstProject.Interfaces.Repositories;
using FirstProject.Interfaces.Services;

namespace FirstProject.Implementation.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<BaseResponse<RoleDto>> DeleteAsync(int id)
        {
           var role = await _roleRepository.GetRoleReturningRoleObjectAsync(id);
            if(role == null)
            {
                return new BaseResponse<RoleDto>
                {
                    IsSucess = false,
                    Message = "Role  Not Found"
                };
            }
            role.IsDeleted = true;
           await _roleRepository.UpdateAsync(role);
            return new BaseResponse<RoleDto>
            {
                Message = "Role  Sucessfully Deleted",
                IsSucess = true
            };
        }

        public async Task<BaseResponse<IEnumerable<RoleDto>>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllRoleAsync();
            if(roles == null)
            {
                return new BaseResponse<IEnumerable<RoleDto>>
                {
                    Message = "No role has been created",
                    IsSucess = false
                };
            }
            return new BaseResponse<IEnumerable<RoleDto>>
            {
                Message = "Role  Sucessfully Retrieved",
                IsSucess = true,
                Data = await _roleRepository.GetAllRoleAsync()
            };
        }

        public async Task<BaseResponse<RoleDto>> GetRoleAsync(int id)
        {
           var role = await _roleRepository.GetRoleReturningRoleDtoObjectAsync(id);
            if(role == null)
            {
                return new BaseResponse<RoleDto>
                {
                    IsSucess = false,
                    Message = "Role  Not Found"
                };
            }
            return new BaseResponse<RoleDto>
            {
                Message = "Role Sucessfully Retrieved",
                IsSucess = true,
                Data = role
            };
        }

        public async Task<BaseResponse<RoleDto>> RegisterAsync(CreateRoleRequestModel model)
        {
             if((await _roleRepository.ExistsByNameAsync(model.Name)))
            {
                return new BaseResponse<RoleDto>
                {
                    Message = "Role  already exists",
                    IsSucess = false
                };
            }
            var role = new Role
            {
              Name = model.Name,
             Description = model.Description
            };
            var roleInfo = await _roleRepository.CreateAsync(role);
            return new BaseResponse<RoleDto>
            {
                Message = "Role  Sucessfully Created",
                IsSucess = true,
                Data = roleInfo
            };
        }

        public async Task<BaseResponse<RoleDto>> UpdateAsync(UpdateRoleRequestModel model, int id)
        {
            var role = await _roleRepository.GetRoleReturningRoleObjectAsync(id);
            if(role == null)
            {
                return new BaseResponse<RoleDto>
                {
                    IsSucess = false,
                    Message = "Role Not Found"
                };
            }
            role.Name = model.Name ?? role.Name;
            role.Description = model.Description ?? role.Description;
            var roleInfo = await _roleRepository.UpdateAsync(role);
            return new BaseResponse<RoleDto>
            {
                IsSucess = true,
                Message = "Role Updated Successfully",
                Data = roleInfo
            };
        }
    }
}