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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user)
        {
           await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
         return user;
        }

        // public async void DeleteAsync(User user)
        // {
        //     _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();
        // }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
             return await _context.Users.AnyAsync(a => a.Email == email && a.IsDeleted == false);
        }

        public async Task<bool> ExistsByPassWordAsync(string passWord)
        {
             return await _context.Users.AnyAsync(a => a.PassWord == passWord && a.IsDeleted == false);
        }

        public async Task<IEnumerable<UserDto>> GetAllUserAsync()
        {
            var users = await _context.Users.Include(a => a.UserRole).ThenInclude(a => a.Role).Where(a =>  a.IsDeleted == false).ToListAsync();
            if(users == null)
            {
                return null;
            }
           return users.Select(a => new UserDto
            {
                Id = a.Id,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber,
                Name = $"{a.FirstName} {a.LastName}"
            }).AsEnumerable();
        }

        public async Task<UserDto> GetUserByEmailReturningUserDtoObjectAsync(string email)
        {
          var user = await _context.Users.Include(a => a.UserRole).ThenInclude(a => a.Role).FirstOrDefaultAsync(a => a.Email == email && a.IsDeleted == false);
            if(user == null)
            {
                return null;
            }
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = $"{user.FirstName}{user.LastName}",
                Roles = user.UserRole.Select(a => new RoleDto
                {
                    Name = a.Role.Name
                }).ToList()
            };
        }

        public async Task<UserDto> GetUserReturningUserDtoObjectAsync(int id)
        {
         var user = await _context.Users.Include(a => a.UserRole).ThenInclude(a => a.Role).FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            if(user == null)
            {
                return null;
            }
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = $"{user.FirstName} {user.LastName}",
                Roles = user.UserRole.Select(a => new RoleDto
                {
                    Name = a.Role.Name
                }).ToList()
                
            };
        }

        public async Task<User> GetUserReturningUserObjectAsync(int id)
        {
            return await _context.Users.Include(a => a.UserRole).ThenInclude(a => a.Role).FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
        }

        public async Task<UserDto> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = $"{user.FirstName} {user.LastName}"
            };
        }
    }
}