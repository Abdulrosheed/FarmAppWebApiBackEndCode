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
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationContext _context;

        public AdminRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<AdminDto> CreateAsync(Admin admin)
        {
            await _context.Admins.AddAsync(admin);
            await _context.SaveChangesAsync();
            return new AdminDto
            {
               Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                State = admin.State,
                LocalGovernment = admin.LocalGoverment,
                Country = admin.Country,
                PhoneNumber = admin.PhoneNumber,
                Gender = admin.Gender,
                UserName = admin.UserName,
                UserId = admin.User.Id
            };
        }

        // public async void DeleteAsync(Admin admin)
        // {
        //    _context.Admins.Remove(admin);
        //    await _context.SaveChangesAsync();
        // }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Admins.AnyAsync(a => a.Email == email && a.IsDeleted == false);
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
          return await _context.Admins.AnyAsync(a => a.Id == id && a.IsDeleted == false);
        }

        public async Task<AdminDto> GetAdminByEmailReturningAdminDtoObjectAsync(string email)
        {
           var admin = await _context.Admins.Include(a => a.User).FirstOrDefaultAsync(a => a.Email == email && a.IsDeleted == false);
           if(admin == null)
           {
               return null;
           }
            return new AdminDto
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                State = admin.State,
                LocalGovernment = admin.LocalGoverment,
                Country = admin.Country,
                PhoneNumber = admin.PhoneNumber,
                Gender = admin.Gender,
                UserName = admin.UserName,
                UserId = admin.User.Id
            };
        }

        public async Task<AdminDto> GetAdminReturningAdminDtoObjectAsync(int id)
        {
            var admin = await _context.Admins.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            if(admin == null)
            {
                return null;
            }
            return new AdminDto
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                State = admin.State,
                LocalGovernment = admin.LocalGoverment,
                Country = admin.Country,
                PhoneNumber = admin.PhoneNumber,
                Gender = admin.Gender,
                UserName = admin.UserName,
                UserId = admin.User.Id
            };
            
        }

        public async Task<Admin> GetAdminReturningAdminObjectAsync(int id)
        {
            var admin =  await _context.Admins.FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            if(admin == null)
            {
                return null;
            }
            return admin;
        }

        public async Task<IEnumerable<AdminDto>> GetAllAdminAsync()
        {
            var admin =  await _context.Admins.Include(a => a.User).Where(a =>  a.IsDeleted == false).ToListAsync();
            if(admin == null)
            {
                return null;
            }

           return admin.Select( a => new AdminDto
           {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                State = a.State,
                LocalGovernment = a.LocalGoverment,
                Country = a.Country,
                PhoneNumber = a.PhoneNumber,
                Gender = a.Gender,
                UserName = a.UserName,
                UserId = a.User.Id
            }).AsEnumerable();
        }

        public async Task<AdminDto> UpdateAsync(Admin admin)
        {
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
            return new AdminDto
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                State = admin.State,
                LocalGovernment = admin.LocalGoverment,
                Country = admin.Country,
                PhoneNumber = admin.PhoneNumber,
                Gender = admin.Gender,
                UserName = admin.UserName,
                UserId = admin.User.Id
            };
        }
    }
}