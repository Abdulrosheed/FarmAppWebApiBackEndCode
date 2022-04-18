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
    public class FarmProduceRepository : IFarmProduceRepository
    {
        private readonly ApplicationContext _context;

        public FarmProduceRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<FarmProduceDto> CreateAsync(FarmProduce farmProduce)
        {
             await _context.FarmProduces.AddAsync(farmProduce);
           await _context.SaveChangesAsync();
           return new FarmProduceDto
           {
               Id = farmProduce.Id,
                Name = farmProduce.Name,
                Description = farmProduce.Description,

           };
        }

        // public async void DeleteAsync(FarmProduce farmProduce)
        // {
        //    _context.FarmProduces.Remove(farmProduce);
        //     await _context.SaveChangesAsync();
        // }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.FarmProduces.AnyAsync(a => a.Name == name && a.IsDeleted == false);
        }

        

        public async Task<IEnumerable<FarmProduceDto>> GetAllFarmProduceAsync()
        {
            var farmProduce = await _context.FarmProduces.Where(a => a.IsDeleted == false).ToListAsync();
            if(farmProduce == null)
            {
                return null;
            }
            return farmProduce.Select(a => new FarmProduceDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Farms = a.FarmProduceFarm.Select(b => new FarmDto
                {
                    Name = b.Farm.Name,
                    State = b.Farm.State,
                    Country = b.Farm.Country,
                    LocalGovernment = b.Farm.LocalGoverment,
                    FarmerName = $"{b.Farm.Farmer.FirstName} {b.Farm.Farmer.LastName}",
                }).ToList()


           }).ToList();
        }

        public async Task<FarmProduceDto> GetFarmProduceReturningFarmProduceDtoObjectAsync(int id)
        {
            var farmProduce = await _context.FarmProduces.FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            if(farmProduce == null)
            {
                return null;
            }
            return new FarmProduceDto
            {
                Id = farmProduce.Id,
                Name = farmProduce.Name,
                Description = farmProduce.Description,
                Farms = farmProduce.FarmProduceFarm.Select(b => new FarmDto
                {
                    Name = b.Farm.Name,
                     State = b.Farm.State,
                    Country = b.Farm.Country,
                    LocalGovernment = b.Farm.LocalGoverment,
                    FarmerName = $"{b.Farm.Farmer.FirstName} {b.Farm.Farmer.LastName}",
                }).ToList()

            };
        }

        public async Task<FarmProduce> GetFarmProduceReturningFarmProduceObjectAsync(int id)
        {
            return await _context.FarmProduces.FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
        }

        public async Task<IEnumerable<FarmProduce>> GetSelectedFarmProduce(IList<int> ids)
        {
            return await _context.FarmProduces.Where(a => ids.Contains(a.Id) && a.IsDeleted == false).ToListAsync();
        }

        public async Task<FarmProduceDto> UpdateAsync(FarmProduce farmProduce)
        {
            _context.FarmProduces.Update(farmProduce);
            await _context.SaveChangesAsync();
            return new FarmProduceDto
            {
                Id = farmProduce.Id,
                Name = farmProduce.Name,
                Description = farmProduce.Description,
                Farms = farmProduce.FarmProduceFarm.Select(b => new FarmDto
                {
                    Name = b.Farm.Name,
                    State = b.Farm.State,
                    Country = b.Farm.Country,
                    LocalGovernment = b.Farm.LocalGoverment,
                    FarmerName = $"{b.Farm.Farmer.FirstName} {b.Farm.Farmer.LastName}",
                }).ToList()
            };
        }
    }
}