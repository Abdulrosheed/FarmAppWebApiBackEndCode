using System;
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
    public class FarmRepository : IFarmRepository
    {
        private readonly ApplicationContext _context;

        public FarmRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<FarmDto> CreateAsync(Farm farm)
        {
            await _context.Farms.AddAsync(farm);
           await _context.SaveChangesAsync();
           return new FarmDto
           {
               Id = farm.Id,
               Name = farm.Name,
                Country = farm.Country,
                LocalGovernment = farm.LocalGoverment,
                State = farm.State,
               LandSize = farm.LandSize,
               
           };
        }

        // public async void DeleteAsync(Farm farm)
        // {
        //     _context.Farms.Remove(farm);
        //     await _context.SaveChangesAsync();
        // }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Farms.AnyAsync(a => a.Name == name && a.IsDeleted == false);
        }

        public async Task<IEnumerable<FarmDto>> GetAllAssignedFarmAsync()
        {
           var farm = await _context.Farms.Include(a => a.Farmer).Include(a => a.FarmProduceFarm).ThenInclude(a => a.FarmProduce).Where(a => a.FarmStatus == FarmStatus.Assigned && a.IsDeleted == false).ToListAsync();
           if(farm == null)
           {
               return null;
           }
           return farm.Select(a => new FarmDto
           {
                Id = a.Id,
               Name = a.Name,
                Country = a.Country,
                LocalGovernment = a.LocalGoverment,
                State = a.State,
               LandSize = a.LandSize,
                FarmerEmail = a.Farmer.Email,
               FarmerName = $"{a.Farmer.FirstName} {a.Farmer.LastName}",
               FarmProduces = a.FarmProduceFarm.Select(b => new FarmProduceDto
               {
                   Name = b.FarmProduce.Name,
                   
               }).ToList()
            

           }).AsEnumerable();
        }

        public async Task<IEnumerable<FarmDto>> GetAllAssignedFarmByFarmerAsync(string email)
        {
             var farm = await _context.Farms.Include(a => a.Farmer).Include(a => a.FarmProduceFarm).ThenInclude(a => a.FarmProduce).Include(a => a.FarmProducts).Where(a => a.FarmStatus == FarmStatus.Approved && a.IsDeleted == false && a.Farmer.Email == email).ToListAsync();
           if(farm == null)
           {
               return null;
           }
           return farm.Select(a => new FarmDto
           {
                Id = a.Id,
               Name = a.Name,
                Country = a.Country,
                LocalGovernment = a.LocalGoverment,
                State = a.State,
               LandSize = a.LandSize,
                FarmerEmail = a.Farmer.Email,
                
               FarmerName = $"{a.Farmer.FirstName} {a.Farmer.LastName}",
               FarmProduces = a.FarmProduceFarm.Select(b => new FarmProduceDto
               {
                   Name = b.FarmProduce.Name,
                   
               }).ToList(),
               FarmProduct = a.FarmProducts.Select(a => new FarmProductDto
               {
                   FarmProductStatus = a.FarmProductStatus.ToString()
               }).ToList()
            

           }).AsEnumerable();
        }

        public async Task<IEnumerable<FarmDto>> GetAllDeclinedFarmAsync()
        {
            var farm = await _context.Farms.Include(a => a.Farmer).Include(a => a.FarmProduceFarm).ThenInclude(a => a.FarmProduce).Where(a => a.FarmStatus == FarmStatus.Declined && a.IsDeleted == false).ToListAsync();
           if(farm == null)
           {
               return null;
           }
           return farm.Select(a => new FarmDto
           {
                Id = a.Id,
               Name = a.Name,
                Country = a.Country,
                LocalGovernment = a.LocalGoverment,
                State = a.State,
               LandSize = a.LandSize,
                FarmerEmail = a.Farmer.Email,
               FarmerName = $"{a.Farmer.FirstName} {a.Farmer.LastName}",
               FarmProduces = a.FarmProduceFarm.Select(b => new FarmProduceDto
               {
                   Name = b.FarmProduce.Name,
                   
               }).ToList()
            

           }).AsEnumerable();
        }

        public async Task<IEnumerable<FarmDto>> GetAllDeclinedFarmByFarmerAsync(string email)
        {
            var farm = await _context.Farms.Include(a => a.Farmer).Include(a => a.FarmProduceFarm).ThenInclude(a => a.FarmProduce).Include(a => a.FarmProducts).Where(a => a.FarmStatus == FarmStatus.Declined && a.IsDeleted == false && a.Farmer.Email == email).ToListAsync();
           if(farm == null)
           {
               return null;
           }
           return farm.Select(a => new FarmDto
           {
                Id = a.Id,
               Name = a.Name,
                Country = a.Country,
                LocalGovernment = a.LocalGoverment,
                State = a.State,
               LandSize = a.LandSize,
                FarmerEmail = a.Farmer.Email,
               FarmerName = $"{a.Farmer.FirstName} {a.Farmer.LastName}",
               FarmProduces = a.FarmProduceFarm.Select(b => new FarmProduceDto
               {
                   Name = b.FarmProduce.Name,
                   
               }).ToList(),
               FarmProduct = a.FarmProducts.Select(a => new FarmProductDto
               {
                   FarmProductStatus = a.FarmProductStatus.ToString()
               }).ToList()
            

           }).AsEnumerable();
        }

        public async Task<IEnumerable<FarmDto>> GetAllFarmAsync()
        {
           var farm = await _context.Farms.Include(a => a.Farmer).Include(a => a.FarmProduceFarm).ThenInclude(a => a.FarmProduce).Include(a => a.FarmProducts).Where(a => a.FarmStatus == FarmStatus.Approved && a.IsDeleted == false).ToListAsync();
           if(farm == null)
           {
               return null;
           }
           return farm.Select(a => new FarmDto
           {
                Id = a.Id,
               Name = a.Name,
                Country = a.Country,
                LocalGovernment = a.LocalGoverment,
                State = a.State,
               LandSize = a.LandSize,
                FarmerEmail = a.Farmer.Email,
               FarmerName = $"{a.Farmer.FirstName} {a.Farmer.LastName}",
               FarmProduces = a.FarmProduceFarm.Select(b => new FarmProduceDto
               {
                   Name = b.FarmProduce.Name,
                   
               }).ToList(),
               FarmProduct = a.FarmProducts.Select(a => new FarmProductDto
               {
                   FarmProductStatus = a.FarmProductStatus.ToString()
               }).ToList()
            

           }).AsEnumerable();
        }

        public async Task<IList<FarmDto>> GetAllInspectedFarmerByFarmInspectorIdAsync(string farmInspectorEmail)
        {
            var farm =  await _context.Farms.Include(a => a.FarmProduceFarm).ThenInclude(a => a.FarmProduce).Include(a => a.Farmer).Where(a => a.FarmInspector.Email == farmInspectorEmail && a.FarmStatus == FarmStatus.Approved && a.IsDeleted == false).ToListAsync();
           if(farm == null)
           {
               return null;
           }
           return farm.Select( a => new FarmDto{
                Id = a.Id,
                Name = a.Name,
                LandSize = a.LandSize,
                Country = a.Country,
                LocalGovernment = a.LocalGoverment,
                State = a.State,
                FarmerName = $"{a.Farmer.FirstName} {a.Farmer.LastName}",
                FarmerEmail = a.Farmer.Email,
                FarmProduces = a.FarmProduceFarm.Select(b => new FarmProduceDto{
                        Name = b.FarmProduce.Name
                    }).ToList()
              
           }).ToList();
        }

        public async Task<IEnumerable<FarmDto>> GetAllProcessingFarmAsync()
        {
            var farm = await _context.Farms.Include(a => a.Farmer).Include(a => a.FarmProduceFarm).ThenInclude(a => a.FarmProduce).Where(a => a.FarmStatus == FarmStatus.ProcessingApproval && a.IsDeleted == false).ToListAsync();
            if(farm == null)
            {
                return null;
            }
            return farm.Select(a => new FarmDto
           {
                Id = a.Id,
               Name = a.Name, 
               Country = a.Country,
                LocalGovernment = a.LocalGoverment,
                State = a.State,
                FarmerEmail = a.Farmer.Email,
               LandSize = a.LandSize,
               FarmerName = $"{a.Farmer.FirstName} {a.Farmer.LastName}",
               FarmProduces = a.FarmProduceFarm.Select(b => new FarmProduceDto
               {
                   Name = b.FarmProduce.Name,
                   
               }).ToList()
            }).AsEnumerable();
        }

        // public async Task<IList<Farm>> GetFarmByInspectionDate(DateTime date)
        // {
        //     var farms = await _context.Farms.Include(a => a.Farmer).Include(a => a.FarmInspector).Include(a => a.FarmProduceFarm).ThenInclude(a => a.FarmProduce).Where(a => a.InspectionDate.HasValue && a.InspectionDate.Value.Day == date.Day).ToListAsync();
        //     if(farms == null)
        //     {
        //         return null;
        //     }
        //     return farms;
        // }

        public async Task<FarmDto> GetFarmReturningFarmDtoObjectAsync(int id)
        {
             var farm = await _context.Farms.Include(a => a.Farmer).Include(a => a.FarmProduceFarm).ThenInclude(a => a.FarmProduce).FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            if(farm == null)
            {
                return null;
            }
            return new FarmDto
            {
                Id = farm.Id,
               Name = farm.Name,
               FarmerEmail = farm.Farmer.Email,
               FarmerId = farm.Farmer.Id,
                Country = farm.Country,
                LocalGovernment = farm.LocalGoverment,
                State = farm.State,
               LandSize = farm.LandSize,
               FarmStatus = farm.FarmStatus.ToString(),
               FarmerName = $"{farm.Farmer.FirstName} {farm.Farmer.LastName}",
               FarmProduces = farm.FarmProduceFarm.Select(b => new FarmProduceDto
               {
                   Name = b.FarmProduce.Name,
                   
               }).ToList()
            };
        }

        public async Task<Farm> GetFarmReturningFarmObjectAsync(int id)
        {
            return await _context.Farms.Include(a => a.FarmProducts).FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
        }

        public async Task<IList<FarmDto>> GetTobeInspectedFarmerByFarmInspectorIdAsync(string farmInspectorEmail)
        {
            var farm =  await _context.Farms.Include(a => a.FarmProduceFarm).ThenInclude(a => a.FarmProduce).Include(a => a.Farmer).Where(a => a.FarmInspector.Email == farmInspectorEmail && a.FarmStatus == FarmStatus.Assigned && a.IsDeleted == false).ToListAsync();
           if(farm == null)
           {
               return null;
           }
           return farm.Select( a => new FarmDto{
                Id = a.Id,
                Name = a.Name,
                LandSize = a.LandSize,
                Country = a.Country,
                LocalGovernment = a.LocalGoverment,
                State = a.State,
                FarmerName = $"{a.Farmer.FirstName} {a.Farmer.LastName}",
                FarmerEmail = a.Farmer.Email,
                FarmProduces = a.FarmProduceFarm.Select(b => new FarmProduceDto{
                        Name = b.FarmProduce.Name
                    }).ToList()
              
           }).ToList();
        }

        public async Task<FarmDto> UpdateAsync(Farm farm)
        {
             _context.Farms.Update(farm);
            await _context.SaveChangesAsync();
            return new FarmDto
            {
                Id = farm.Id,
               Name = farm.Name,
                Country = farm.Country,
                LocalGovernment = farm.LocalGoverment,
                State = farm.State,
               LandSize = farm.LandSize,
              
            };;
        }
    }
}