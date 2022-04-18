using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstProject.Context;
using FirstProject.Dtos;
using FirstProject.Entities;
using FirstProject.Enums;
using FirstProject.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Implementation.Repositories
{
    public class FarmerRepository : IFarmerRepository
    {
       private readonly ApplicationContext _context;

        public FarmerRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IList<FarmerDto>> AllAssignedFarmerAprovalAsync()
        {
           var farmer =  await _context.Farmers.Include(a => a.Farms).ThenInclude(a => a.FarmProduceFarm).ThenInclude(a => a.FarmProduce).Where(a => a.FarmerStatus == FarmerStatus.Assigned && a.IsDeleted == false).ToListAsync();
           if(farmer == null)
           {
               return null;
           }
           return farmer.Select(a => new FarmerDto{
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                State = a.State,
                Country = a.Country,
                LocalGovernment = a.LocalGoverment,
                Gender = a.Gender.ToString(),
                UserId = a.UserId,
                UserName = a.UserName,
                FarmerStatus = a.FarmerStatus.ToString(),
                PhoneNumber = a.PhoneNumber,
                Email = a.Email,
                Farms = a.Farms.Select(a => new FarmDto{
                    Name = a.Name,
                    State = a.State,
                    Country = a.Country,
                    LocalGovernment = a.LocalGoverment,
                    LandSize = a.LandSize,
                    FarmProduces = a.FarmProduceFarm.Select(b => new FarmProduceDto{
                        Name = b.FarmProduce.Name
                    }).ToList()
                }).ToList()
           }).ToList();
        }

        public async Task<IList<FarmerDto>> AllDeclinedFarmerAprovalAsync()
        {
            var farmer = await _context.Farmers.Include(a => a.Farms).ThenInclude(a => a.FarmProduceFarm).ThenInclude(a => a.FarmProduce).Where(a => a.FarmerStatus == FarmerStatus.Declined && a.IsDeleted == false).ToListAsync();
            if(farmer == null)
            {
                return null;
            }
            return farmer.Select(a => new FarmerDto{
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                State = a.State,
                Country = a.Country,
                LocalGovernment = a.LocalGoverment,
                Gender = a.Gender.ToString(),
                UserId = a.UserId,
                UserName = a.UserName,
                FarmerStatus = a.FarmerStatus.ToString(),
                Email = a.Email,
                Farms = a.Farms.Select(a => new FarmDto{
                    Name = a.Name,
                    State = a.State,
                    Country = a.Country,
                    LocalGovernment = a.LocalGoverment,
                    LandSize = a.LandSize,
                    FarmProduces = a.FarmProduceFarm.Select(b => new FarmProduceDto{
                        Name = b.FarmProduce.Name
                    }).ToList()
                }).ToList()
           }).ToList();
        }

        public async Task<IList<FarmerDto>> AllProcessingFarmerAprovalAsync()
        {
            var farmer =  await _context.Farmers.Include(a => a.Farms).ThenInclude(a => a.FarmProduceFarm).ThenInclude(a => a.FarmProduce).Where(a => a.FarmerStatus == FarmerStatus.ProcessingApproval && a.IsDeleted == false).ToListAsync();
            if(farmer == null)
            {
                return null;
            }
            return farmer.Select(a => new FarmerDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                State = a.State,
                Country = a.Country,
                LocalGovernment = a.LocalGoverment,
                Gender = a.Gender.ToString(),
                UserId = a.UserId,
                UserName = a.UserName,
                FarmerStatus = a.FarmerStatus.ToString(),
                Email = a.Email,
                Farms = a.Farms.Select(a => new FarmDto{
                    Name = a.Name,
                    State = a.State,
                    Country = a.Country,
                    LocalGovernment = a.LocalGoverment,
                    LandSize = a.LandSize,
                    FarmProduces = a.FarmProduceFarm.Select(b => new FarmProduceDto{
                        Name = b.FarmProduce.Name
                    }).ToList()
                }).ToList()
           }).ToList();
        }

        public async Task<FarmerDto> CreateAsync(Farmer farmer)
        {
            await _context.Farmers.AddAsync(farmer);
            await _context.SaveChangesAsync();
            return new FarmerDto
            {
                Id = farmer.Id,
                FirstName = farmer.FirstName,
                LastName = farmer.LastName,
                State = farmer.State,
                Country = farmer.Country,
                LocalGovernment = farmer.LocalGoverment,
                Gender = farmer.Gender.ToString(),
                UserId = farmer.UserId,
                UserName = farmer.UserName,
                FarmerStatus = farmer.FarmerStatus.ToString(),
                Email = farmer.Email,
              


            };
        }

        // public async void DeleteAsync(Farmer farmer)
        // {
        //   _context.Farmers.Remove(farmer);
        //   await _context.SaveChangesAsync();
        // }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Farmers.AnyAsync(a => a.Email == email && a.IsDeleted == false);
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
             return await _context.Farmers.AnyAsync(a => a.Id == id && a.IsDeleted == false);
        }

       

        public async Task<IEnumerable<FarmerDto>> GetAllFarmersAsync()
        {
           var farmer =  await _context.Farmers.Include(a => a.Farms).ThenInclude(a => a.FarmProduceFarm).ThenInclude(a => a.FarmProduce).Where(a => a.FarmerStatus == FarmerStatus.Approved && a.IsDeleted == false).ToListAsync();
           if(farmer == null)
           {
               return null;
           }
           return farmer.Select(a => new FarmerDto{
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                State = a.State,
                Country = a.Country,
                LocalGovernment = a.LocalGoverment,
                Gender = a.Gender.ToString(),
                UserId = a.UserId,
                UserName = a.UserName,
                FarmerStatus = a.FarmerStatus.ToString(),
                Email = a.Email,
                Farms = a.Farms.Select( a => new FarmDto{
                    Name = a.Name,
                    State = a.State,
                    Country = a.Country,
                    LocalGovernment = a.LocalGoverment,
                    LandSize = a.LandSize,
                    FarmProduces = a.FarmProduceFarm.Select(b => new FarmProduceDto{
                        Name = b.FarmProduce.Name
                    }).ToList()
                }).ToList()
           }).ToList();
        }

       
        public async Task<FarmerDto> GetFarmerReturningFarmerDtoObjectAsync(int id)
        {
            var farmer = await _context.Farmers.Include(a => a.Farms).ThenInclude(a => a.FarmProduceFarm).ThenInclude(a => a.FarmProduce).FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            if(farmer == null)
            {
                return null;
            }
            return new FarmerDto
            {
                  Id = farmer.Id,
                FirstName = farmer.FirstName,
                LastName = farmer.LastName,
                 State = farmer.State,
                Country = farmer.Country,
                LocalGovernment = farmer.LocalGoverment, 
                Gender = farmer.Gender.ToString(),
                UserId = farmer.UserId,
                UserName = farmer.UserName,
                FarmerStatus = farmer.FarmerStatus.ToString(),
                PhoneNumber = farmer.PhoneNumber,
                Email = farmer.Email,
                Farms = farmer.Farms.Select(a => new FarmDto{
                    Name = a.Name,
                    State = a.State,
                    Country = a.Country,
                    LocalGovernment = a.LocalGoverment,
                    LandSize = a.LandSize,
                    Id =  a.Id,
                    FarmProduces = a.FarmProduceFarm.Select(b => new FarmProduceDto{
                        Name = b.FarmProduce.Name
                    }).ToList()
                }).ToList()
            };
        }

        public async Task<FarmerDto> GetFarmerReturningFarmerDtoObjectAsync(string email)
        {
            var farmer = await _context.Farmers.Include(a => a.Farms).ThenInclude(a => a.FarmProduceFarm).ThenInclude(a => a.FarmProduce).FirstOrDefaultAsync(a => a.Email == email && a.IsDeleted == false);
            if(farmer == null)
            {
                return null;
            }
            return new FarmerDto
            {
                 Id = farmer.Id,
                FirstName = farmer.FirstName,
                LastName = farmer.LastName,
                State = farmer.State,
                Country = farmer.Country,
                LocalGovernment = farmer.LocalGoverment,
                Gender = farmer.Gender.ToString(),
                UserId = farmer.UserId,
                UserName = farmer.UserName,
                FarmerStatus = farmer.FarmerStatus.ToString(),
                Email = farmer.Email,
                
                Farms = farmer.Farms.Select(a => new FarmDto{
                    Name = a.Name,
                    State = a.State,
                    Country = a.Country,
                    LocalGovernment = a.LocalGoverment,
                    LandSize = a.LandSize,
                    Id = a.Id,
                    FarmProduces = a.FarmProduceFarm.Select(b => new FarmProduceDto{
                        Name = b.FarmProduce.Name
                    }).ToList()
                }).ToList()
            }; 
        }

        public async Task<Farmer> GetFarmerReturningFarmerObjectAsync(int id)
        {
            return await _context.Farmers.Include(a => a.Farms).ThenInclude(a => a.FarmProduceFarm).ThenInclude(a => a.FarmProduce).FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
        }

        public async Task<Farmer> GetFarmerReturningFarmerObjectAsync(string email)
        {
            return await _context.Farmers.Include(a => a.Farms).ThenInclude(a => a.FarmProduceFarm).ThenInclude(a => a.FarmProduce).FirstOrDefaultAsync(a => a.Email == email && a.IsDeleted == false);
        }

      
        public async Task<FarmerDto> UpdateAsync(Farmer farmer)
        {
            _context.Farmers.Update(farmer);
            await _context.SaveChangesAsync();
            return new FarmerDto
            {
                 Id = farmer.Id,
                FirstName = farmer.FirstName,
                LastName = farmer.LastName,
                State = farmer.State,
                Country = farmer.Country,
                LocalGovernment = farmer.LocalGoverment,
                Gender = farmer.Gender.ToString(),
                UserId = farmer.UserId,
                UserName = farmer.UserName,
                FarmerStatus = farmer.FarmerStatus.ToString(),
                Email = farmer.Email,
               
            };
        }
    }
}