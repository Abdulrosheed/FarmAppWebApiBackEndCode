using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstProject.Context;
using FirstProject.Dtos;
using FirstProject.Entities;
using FirstProject.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Implementation.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationContext _context;

        public RoleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<RoleDto> CreateAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
           await _context.SaveChangesAsync();
           return new RoleDto
           {
               Id = role.Id,
               Name = role.Name,
               Description = role.Description
           };
        }

      

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Roles.AnyAsync(a => a.Name == name && a.IsDeleted == false);
        }

        public async Task<IEnumerable<RoleDto>> GetAllRoleAsync()
        {
            var roles = await _context.Roles.Where(a =>  a.IsDeleted == false).ToListAsync();
            if(roles == null)
            {
                return null;
            }
            return roles.Select(a => new RoleDto
            {
                Id = a.Id,
               Name = a.Name,
               Description = a.Description,
               Users = a.UserRole.Select(a => new UserDto
               {
                   Id = a.UserId,
                   Email = a.User.Email
               }).ToList()
           }).AsEnumerable();
        }

        public async Task<Role> GetRoleByNameReturningRoleObjectAsync(string name)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(a => a.Name == name && a.IsDeleted == false);
            return role;
             
        }

        public async Task<RoleDto> GetRoleReturningRoleDtoObjectAsync(int id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            if(role == null)
            {
                return null;
            }
            return new RoleDto
            {
                 Id = role.Id,
               Name = role.Name,
               Description = role.Description,
               Users = role.UserRole.Select(a => new UserDto
               {
                   Id = a.UserId,
                   Email = a.User.Email
               }).ToList()
            };
        }

        public async Task<Role> GetRoleReturningRoleObjectAsync(int id)
        {
           return await _context.Roles.FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
        }

        public async Task<IList<Role>> GetSelectedRoles(List<int> ids)
        {
            return await _context.Roles.Where(a => ids.Contains(a.Id) && a.IsDeleted == false).ToListAsync();
        }

        public async Task<RoleDto> UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return new RoleDto
            {
                Id = role.Id,
               Name = role.Name,
               Description = role.Description,
               Users = role.UserRole.Select(a => new UserDto
               {
                   Id = a.UserId,
                   Email = a.User.Email
               }).ToList()
            };
        }
    }
}