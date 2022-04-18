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
    public class FarmInspectorRepository : IFarmInspectorRepository
    {
         private readonly ApplicationContext _context;

        public FarmInspectorRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<FarmInspectorDto> CreateAsync(FarmInspector farmInspector)
        {
             await _context.FarmInspectors.AddAsync(farmInspector);
            await _context.SaveChangesAsync();
            return new FarmInspectorDto
            {
                Id = farmInspector.Id,
                FirstName = farmInspector.FirstName,
                LastName = farmInspector.LastName,
                LocalGovernment = farmInspector.LocalGoverment,
               State = farmInspector.State,
               Country = farmInspector.Country,
                Email = farmInspector.Email,
                Gender = farmInspector.Gender.ToString(),
              
            };
        }

        // public async void DeleteAsync(FarmInspector farmInspector)
        // {
        //     _context.FarmInspectors.Remove(farmInspector);
        //   await _context.SaveChangesAsync();
        // }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
             return await _context.FarmInspectors.AnyAsync(a => a.Email == email && a.IsDeleted == false);
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
           return await _context.FarmInspectors.AnyAsync(a => a.Id == id && a.IsDeleted == false);
        }

       

        public async Task<IEnumerable<FarmInspectorDto>> GetAllFarmInspectorAsync()
        {
            var farmInspector = await _context.FarmInspectors.Include(a => a.User).Where(a =>  a.IsDeleted == false).ToListAsync();
            if(farmInspector == null)
            {
                return null;
            }
            return farmInspector.Select(a => new FarmInspectorDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
               LocalGovernment = a.LocalGoverment,
               State = a.State,
               Country = a.Country,
                Email = a.Email,
                Gender = a.Gender.ToString(),
                UserId = a.UserId,
                PhoneNumber = a.PhoneNumber
           }).ToList();
        }

       

        public async  Task<FarmInspectorDto> GetFarmerReturningFarmInspectorDtoObjectAsync(int id)
        {
           var farmInspector = await _context.FarmInspectors.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            if(farmInspector == null)
            {
                return null;
            }
            return new FarmInspectorDto
            {
                Id = farmInspector.Id,
                FirstName = farmInspector.FirstName,
                LastName = farmInspector.LastName,
                LocalGovernment = farmInspector.LocalGoverment,
                UserName = farmInspector.UserName,
               State = farmInspector.State,
               Country = farmInspector.Country,
                Email = farmInspector.Email,
                Gender = farmInspector.Gender.ToString(),
                UserId = farmInspector.UserId,
                 PhoneNumber = farmInspector.PhoneNumber
            };
        }

        public async Task<IList<FarmInspectorDto>> GetFarmInspectorByCountryReturningFarmInspectorObjectDtoAsync(string country)
        {
            var farmInspector =  await _context.FarmInspectors.Where(a => a.Country == country && a.IsDeleted == false).ToListAsync();
            if(farmInspector == null)
            {
                return null;
            }
            return farmInspector.Select(a => new FarmInspectorDto
            {
                 Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                LocalGovernment = a.LocalGoverment,
               State = a.State,
               Country = a.Country,
                Email = a.Email,
                Gender = a.Gender.ToString(),
                UserId = a.UserId,
                 PhoneNumber = a.PhoneNumber
            }).ToList();
        }

        public async Task<IList<FarmInspectorDto>> GetFarmInspectorByLocalGovernmentReturningFarmInspectorObjectDtoAsync(string localGoverment)
        {
            var farmInspector =  await _context.FarmInspectors.Where(a => a.LocalGoverment == localGoverment && a.IsDeleted == false).ToListAsync();
            if(farmInspector == null)
            {
                return null;
            }
            return farmInspector.Select(a => new FarmInspectorDto
            
            {
                 Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                 LocalGovernment = a.LocalGoverment,
               State = a.State,
               Country = a.Country,
                Email = a.Email,
                Gender = a.Gender.ToString(),
                UserId = a.UserId,
                 PhoneNumber = a.PhoneNumber
            }).ToList();
        }

        public async Task<IList<FarmInspectorDto>> GetFarmInspectorByStateReturningFarmInspectorObjectDtoAsync(string state)
        {
            var farmInspector =  await _context.FarmInspectors.Where(a => a.State == state && a.IsDeleted == false).ToListAsync();
            if(farmInspector == null)
            {
                return null;
            }
            return farmInspector.Select(a => new FarmInspectorDto
            {
                 Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                LocalGovernment = a.LocalGoverment,
               State = a.State,
               Country = a.Country,
                Email = a.Email,
                Gender = a.Gender.ToString(),
                UserId = a.UserId,
                 PhoneNumber = a.PhoneNumber
            }).ToList();
        }

        public async Task<FarmInspectorDto> GetFarmInspectorReturningFarmInspectorDtoObjectAsync(string email)
        {
            var farmInspector = await _context.FarmInspectors.Include(a => a.User).FirstOrDefaultAsync(a => a.Email == email && a.IsDeleted == false);
            if(farmInspector == null)
            {
                return null;
            }
            return new FarmInspectorDto
            {
                Id = farmInspector.Id,
                FirstName = farmInspector.FirstName,
                LastName = farmInspector.LastName,
                LocalGovernment = farmInspector.LocalGoverment,
               State = farmInspector.State,
               Country = farmInspector.Country,
                Email = farmInspector.Email,
                Gender = farmInspector.Gender.ToString(),
                UserId = farmInspector.UserId,
                 PhoneNumber = farmInspector.PhoneNumber
            };
        }

        public async Task<FarmInspector> GetFarmInspectorReturningFarmInspectorObjectAsync(int id)
        {
             return await _context.FarmInspectors.FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
        }

        public async Task<FarmInspector> GetFarmInspectorReturningFarmInspectorObjectAsync(string email)
        {
            return await _context.FarmInspectors.FirstOrDefaultAsync(a => a.Email == email && a.IsDeleted == false);
        }

       
        

        public async Task<FarmInspectorDto> UpdateAsync(FarmInspector farmInspector)
        {
            _context.FarmInspectors.Update(farmInspector);
            await _context.SaveChangesAsync();
            return new FarmInspectorDto
            {
                Id = farmInspector.Id,
                FirstName = farmInspector.FirstName,
                LastName = farmInspector.LastName,
                 LocalGovernment = farmInspector.LocalGoverment,
               State = farmInspector.State,
               Country = farmInspector.Country,
                Email = farmInspector.Email,
                Gender = farmInspector.Gender.ToString(),
                UserId = farmInspector.UserId,
                 PhoneNumber = farmInspector.PhoneNumber
            };
        }
    }
}