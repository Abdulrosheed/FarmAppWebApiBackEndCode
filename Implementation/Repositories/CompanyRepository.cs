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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationContext _context;

        public CompanyRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<CompanyDto> CreateAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
            return new CompanyDto
            {
               Name = company.Name,
               UserName = company.UserName,
               UserId = company.UserId,
               LocalGovernment = company.LocalGoverment,
               State = company.State,
               Country = company.Country,
               Id = company.Id,
               Email = company.Email,
            };
        }

        // public async void DeleteAsync(Company company)
        // {
        //    _context.Companies.Remove(company);
        //    await _context.SaveChangesAsync();
        // }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Companies.AnyAsync(a => a.Email == email && a.IsDeleted == false);
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await _context.Companies.AnyAsync(a => a.Id == id && a.IsDeleted == false);
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompanyAsync()
        {
            var company =  await _context.Companies.Include(a => a.User).Where(a =>  a.IsDeleted == false).ToListAsync();
            if(company == null)
            {
                return null;
            }

           return company.Select(a => new CompanyDto
           {
                   Name = a.Name,
               UserName = a.UserName,
               UserId = a.UserId,
               LocalGovernment = a.LocalGoverment,
               State = a.State,
               Country = a.Country,
               Id = a.Id,
               Email = a.Email,
            }).AsEnumerable();
        }

        public async Task<CompanyDto> GetCompanyByEmailReturningCompanyDtoObjectAsync(string email)
        {
            var company = await _context.Companies.Include(a => a.User).Include(a => a.Orders).ThenInclude(a => a.OrderProducts).ThenInclude(a => a.FarmProduct).FirstOrDefaultAsync(a => a.Email == email && a.IsDeleted == false);
            if(company == null)
            {
                return null;
            }
            return new CompanyDto
            {
                Name = company.Name,
               UserName = company.UserName,
               UserId = company.UserId,
               LocalGovernment = company.LocalGoverment,
               State = company.State,
               Country = company.Country,
               Id = company.Id,
               PhoneNumber = company.PhoneNumber,
               Email = company.Email,
               Orders = company.Orders.Select(a => new OrderDto
               {
                OrderReference = a.OrderReference,
                CompanyEmail = company.Email,
                TotalPrice = a.TotalPrice,
                OrderProducts = a.OrderProducts.Select( a => new FarmProductDto
                {
                    FarmProduct = a.FarmProduct.FarmProduce,
                    Quantity = a.Quantity,
                    Price = a.UnitPrice
                }).ToList()
               }).ToList()
            };
        }

        public async Task<CompanyDto> GetCompanyReturningCompanyDtoObjectAsync(int id)
        {
            var company = await _context.Companies.Include(a => a.User).Include(a => a.Orders).ThenInclude(a => a.OrderProducts).ThenInclude(a => a.FarmProduct).FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            if(company == null)
            {
                return null;
            }
            return new CompanyDto
            {
                Name = company.Name,
               UserName = company.UserName,
               UserId = company.UserId,
               LocalGovernment = company.LocalGoverment,
               State = company.State,
               Country = company.Country,
               Id = company.Id,
               PhoneNumber = company.PhoneNumber,
               Email = company.Email,
                Orders = company.Orders.Select(a => new OrderDto
               {
                OrderReference = a.OrderReference,
                CompanyEmail = company.Email,
                TotalPrice = a.TotalPrice,
                OrderProducts = a.OrderProducts.Select( a => new FarmProductDto
                {
                    FarmProduct = a.FarmProduct.FarmProduce,
                    Quantity = a.Quantity,
                    Price = a.UnitPrice
                }).ToList()
               }).ToList()
            };
        }

        public async Task<Company> GetCompanyReturningCompanyObjectAsync(int id)
        {
            var company =  await _context.Companies.FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            if(company == null)
            {
                return null;
            }
            return company;
        }

        public async Task<CompanyDto> UpdateAsync(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
            return new CompanyDto
            {
                Name = company.Name,
               UserName = company.UserName,
               UserId = company.UserId,
               LocalGovernment = company.LocalGoverment,
               State = company.State,
               Country = company.Country,
               Id = company.Id,
               Email = company.Email,
               
            };
        }
    }
}