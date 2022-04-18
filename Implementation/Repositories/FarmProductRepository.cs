using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstProject.Context;
using FirstProject.Entities;
using FirstProject.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Implementation.Repositories
{
    public class FarmProductRepository : IFarmProductRepository
    {
        private readonly ApplicationContext _context;

        public FarmProductRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<FarmProductDto> CreateAsync(FarmProduct farmProduct)
        {
            await _context.FarmProducts.AddAsync(farmProduct);
            await _context.SaveChangesAsync();
            return new FarmProductDto
            {
                Id = farmProduct.Id,
                State = farmProduct.State,
                Country = farmProduct.Country,
                LocalGovernment = farmProduct.LocalGoverment,
                FarmProduct = farmProduct.FarmProduce,
                FarmProductStatus = farmProduct.FarmProductStatus.ToString(),
                Quantity = farmProduct.Quantity,
                Grade = farmProduct.Grade.ToString(),
                HarvestedTime = farmProduct.HarvestedPeriod

            };
        }

        public async Task<List<FarmProduct>> GetAllFarmProductObjectAsync()
        {
            var farmProducts = await _context.FarmProducts?.Where(a => a.HarvestedPeriod.Day < DateTime.UtcNow.Day).ToListAsync();
            if (farmProducts == null)
            {
                return null;
            }
            return farmProducts;
        }

        public async Task<IEnumerable<FarmProductDto>> GetAllFarmProductsAsync()
        {
            var farmProduce = await _context.FarmProducts.Include(a => a.Farm).ThenInclude(a => a.Farmer).Where(a => a.Price > 0 && a.IsDeleted == false && a.FarmProductStatus == FarmProductStatus.Available).ToListAsync();
            if (farmProduce == null)
            {
                return null;
            }
            return farmProduce.Select(a => new FarmProductDto
            {
                Id = a.Id,
                State = a.State,
                Country = a.Country,
                LocalGovernment = a.LocalGoverment,
                FarmProduct = a.FarmProduce,
                FarmProductStatus = a.FarmProductStatus.ToString(),
                Quantity = a.Quantity,
                Grade = a.Grade.ToString(),
                FarmerEmail = a.Farm.Farmer.Email,
                FarmId = a.FarmId,
                Price = (decimal)a.Price,
                HarvestedTime = a.HarvestedPeriod,
                ProductImage1 = a.ProductImage1,
                ProductImage2 = a.ProductImage2

            }).ToList();

        }

        public async Task<IEnumerable<FarmProductDto>> GetAllFarmProductsInspectedByFarmInspectorAsync(string email)
        {
            var farmProduce = await _context.FarmProducts.Include(a => a.Farm).ThenInclude(a => a.Farmer).Include(a => a.FarmReport).ThenInclude(a => a.FarmInspector).Where(a => a.FarmReport.FarmInspector.Email == email && a.IsDeleted == false && a.FarmProductStatus == FarmProductStatus.Available).ToListAsync();
            if (farmProduce == null)
            {
                return null;
            }
            return farmProduce.Select(a => new FarmProductDto
            {
                Id = a.Id,
                State = a.State,
                Country = a.Country,
                LocalGovernment = a.LocalGoverment,
                FarmProduct = a.FarmProduce,
                FarmProductStatus = a.FarmProductStatus.ToString(),
                Quantity = a.Quantity,
                Grade = a.Grade.ToString(),
                FarmerEmail = a.Farm.Farmer.Email,
                FarmId = a.FarmId,
                HarvestedTime = a.HarvestedPeriod,
                ProductImage1 = a.ProductImage1,
                ProductImage2 = a.ProductImage2
            }).ToList();
        }

        public async Task<IList<FarmProductDto>> GetFarmProductByFarmerEmailReturningFarmProductObjectAsync(string email)
        {
            var farmProduct = await _context.FarmProducts.Include(a => a.Farm).ThenInclude(a => a.Farmer).Where(a => a.Farmer.Email == email && a.Price > 0 && a.IsDeleted == false).ToListAsync();
            if (farmProduct == null)
            {
                return null;
            }
            return farmProduct.Select(a => new FarmProductDto
            {
                Id = a.Id,
                State = a.State,
                Country = a.Country,
                LocalGovernment = a.LocalGoverment,
                FarmProduct = a.FarmProduce,
                FarmProductStatus = a.FarmProductStatus.ToString(),
                Quantity = a.Quantity,
                Grade = a.Grade.ToString(),
                FarmerEmail = a.Farm.Farmer.Email,
                FarmId = a.FarmId,
                Price = (decimal)a.Price,
                HarvestedTime = a.HarvestedPeriod,
                ProductImage1 = a.ProductImage1,
                ProductImage2 = a.ProductImage2

            }).ToList();
        }

        public async Task<FarmProduct> GetFarmProductByFarmIdReturningFarmProductObjectAsync(int farmId)
        {
            return await _context.FarmProducts.Include(a => a.Farm).ThenInclude(a => a.Farmer).FirstOrDefaultAsync(a => a.FarmId == farmId && a.IsDeleted == false);
        }

        public async Task<FarmProductDto> GetFarmProductByIdAsync(int id)
        {

            var farmProduct = await _context.FarmProducts.Include(a => a.Farm).ThenInclude(a => a.Farmer).FirstOrDefaultAsync(a => a.Id == id && a.Price > 0 && a.IsDeleted == false);
            if (farmProduct == null)
            {
                return null;
            }
            return new FarmProductDto
            {
                Id = farmProduct.Id,
                State = farmProduct.State,
                Country = farmProduct.Country,
                LocalGovernment = farmProduct.LocalGoverment,
                FarmProduct = farmProduct.FarmProduce,
                FarmProductStatus = farmProduct.FarmProductStatus.ToString(),
                Quantity = farmProduct.Quantity,
                Grade = farmProduct.Grade.ToString(),
                FarmerEmail = farmProduct.Farm.Farmer.Email,
                FarmId = farmProduct.FarmId,
                FarmerId = farmProduct.Farm.Farmer.Id,
                Price = (decimal)farmProduct.Price,
                HarvestedTime = farmProduct.HarvestedPeriod,
                ProductImage1 = farmProduct.ProductImage1,
                ProductImage2 = farmProduct.ProductImage2


            };
        }

        public async Task<FarmProduct> GetFarmProductByNameAndFarmId(int farmId, string farmProduct)
        {
            return await _context.FarmProducts.SingleOrDefaultAsync(a => a.FarmId == farmId && a.FarmProduce == farmProduct && a.IsDeleted == false);
        }


        public async Task<IList<SearchRequestFarmProductDto>> GetFarmProductByRequestAsync(GetFarmProductByRequestModel model)
        {
            var neededProducts = new List<FarmProduct>();
            var neededQuantity = 0;
          
            var farmProducts = await _context.FarmProducts.Include(a => a.Farm).ThenInclude(a => a.Farmer).Where(a => a.HarvestedPeriod.ToString().Substring(0, 4) == model.YearNeeded.ToString()
            && a.Grade == model.Grade && a.FarmProductStatus == FarmProductStatus.Available && a.HarvestedPeriod.ToString().Substring(6, 1) == model.MonthNeeded.ToString() &&
             a.Quantity == model.Quantity && a.Price > 0).ToListAsync();
     
            if (farmProducts.Count == 0)
            {
                var products = await _context.FarmProducts.Include(a => a.Farm).ThenInclude(a => a.Farmer).Where(a => a.HarvestedPeriod.ToString().Substring(0, 4) == model.YearNeeded.ToString()
               && a.Grade == model.Grade && a.FarmProductStatus == FarmProductStatus.Available && a.HarvestedPeriod.ToString().Substring(6, 1) == model.MonthNeeded.ToString() && a.FarmProduce == model.FarmProduce  && a.Price > 0).ToListAsync();
               
               
                var totalProductQuantity = products.Sum(item => item.Quantity);
                List<FarmProduct> SortedList = products.OrderByDescending(o=>o.Quantity).ToList();
                if (totalProductQuantity >= model.Quantity)
                {
                   
                    foreach(var item in SortedList)
                    {
                        neededQuantity+= item.Quantity;
                        
                        if(neededQuantity < model.Quantity)
                        {
                            neededProducts.Add(item);
                        }
                        else if(neededQuantity >= model.Quantity)
                        {
                            neededProducts.Add(item);
                            break;
                        }
                    }
                    return neededProducts.Select(a => new SearchRequestFarmProductDto
                    {
                        Id = a.Id,
                        Price = (decimal)a.Price,
                        Quantity = a.Quantity,
                      FarmerId = a.FarmerId,
                      FarmCity = a.Farm.LocalGoverment,
                      FarmCountry = a.Farm.LocalGoverment,
                      FarmerEmail = a.Farmer.Email,
                      FarmerName = $"{a.Farmer.FirstName} {a.Farmer.LastName}",
                      FarmName = a.Farm.Name,
                      FarmState = a.Farm.State,
                      PullResources = true
                    }).ToList();
                }
                else
                {
                    return neededProducts.Select(a => new SearchRequestFarmProductDto
                    {
                       
                    }).ToList();
                }

            }

            return farmProducts.Select(a => new SearchRequestFarmProductDto
            {
                  Id = a.Id,
                FarmerId = a.FarmerId,
                  Quantity = a.Quantity,
                    Price = (decimal)a.Price,
                FarmCity = a.Farm.LocalGoverment,
                FarmCountry = a.Farm.LocalGoverment,
                FarmerEmail = a.Farmer.Email,
                FarmerName = $"{a.Farmer.FirstName} {a.Farmer.LastName}",
                FarmName = a.Farm.Name,
                FarmState = a.Farm.State
            }).ToList();
        }



        public async Task<FarmProduct> GetFarmProductReturningFarmProductObjectAsync(int id)
        {
            return await _context.FarmProducts.Include(a => a.Farm).ThenInclude(a => a.Farmer).FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
        }

        public async Task<IEnumerable<FarmProductDto>> GetFarmProductsByLocalGovernmentReturningFarmProductDtoObjectAsync(string localGoverment)
        {
            var farmProduct = await _context.FarmProducts.Include(a => a.Farm).ThenInclude(a => a.Farmer).Where(a => a.LocalGoverment == localGoverment && a.Price > 0 && a.IsDeleted == false).ToListAsync();
            if (farmProduct == null)
            {
                return null;
            }
            return farmProduct.Select(a => new FarmProductDto
            {
                Id = a.Id,
                State = a.State,
                Country = a.Country,
                LocalGovernment = a.LocalGoverment,
                FarmProduct = a.FarmProduce,
                FarmProductStatus = a.FarmProductStatus.ToString(),
                Quantity = a.Quantity,
                Grade = a.Grade.ToString(),
                FarmerEmail = a.Farm.Farmer.Email,
                FarmId = a.FarmId,
                Price = (decimal)a.Price,
                HarvestedTime = a.HarvestedPeriod,
                ProductImage1 = a.ProductImage1,
                ProductImage2 = a.ProductImage2

            }).ToList();
        }

        public async Task<IEnumerable<FarmProductDto>> GetFarmProductsByStateReturningFarmProductDtoObjectAsync(string state)
        {
            var farmProduct = await _context.FarmProducts.Include(a => a.Farm).ThenInclude(a => a.Farmer).Where(a => a.State == state && a.Price > 0 && a.IsDeleted == false).ToListAsync();
            if (farmProduct == null)
            {
                return null;
            }
            return farmProduct.Select(a => new FarmProductDto
            {
                Id = a.Id,
                State = a.State,
                Country = a.Country,
                LocalGovernment = a.LocalGoverment,
                FarmProduct = a.FarmProduce,
                FarmProductStatus = a.FarmProductStatus.ToString(),
                Quantity = a.Quantity,
                Grade = a.Grade.ToString(),
                FarmerEmail = a.Farm.Farmer.Email,
                FarmId = a.FarmId,
                Price = (decimal)a.Price,
                HarvestedTime = a.HarvestedPeriod,
                ProductImage1 = a.ProductImage1,
                ProductImage2 = a.ProductImage2

            }).ToList();
        }

        public async Task<IList<FarmProductDto>> GetNotSoldFarmProductByFarmerAsync(string email)
        {
            var farmProducts = await _context.FarmProducts.Include(a => a.Farm).Include(a => a.Farmer).Where(a => a.Farmer.Email == email && a.FarmProductStatus == FarmProductStatus.Available && a.IsDeleted == false).ToListAsync();
            if (farmProducts == null)
            {
                return null;
            }
            return farmProducts.Select(a => new FarmProductDto
            {
                Id = a.Id,
                State = a.State,
                Country = a.Country,
                LocalGovernment = a.LocalGoverment,
                FarmProduct = a.FarmProduce,
                FarmProductStatus = a.FarmProductStatus.ToString(),
                Quantity = a.Quantity,
                Grade = a.Grade.ToString(),
                FarmerEmail = a.Farm.Farmer.Email,
                FarmerId = a.Farm.Farmer.Id,
                FarmId = a.FarmId,
                Price = (decimal)a.Price,
                HarvestedTime = a.HarvestedPeriod,
                ProductImage1 = a.ProductImage1,
                ProductImage2 = a.ProductImage2

            }).ToList();

        }

        public async Task<IEnumerable<FarmProduct>> GetSelectedFarmProducts(IDictionary<int, int> items)
        {
            var products = await _context.FarmProducts.Where(c => items.Keys.Contains(c.Id) && c.IsDeleted == false).ToListAsync();

            if (products == null)
            {
                return null;
            }
            return products;


        }

        public async Task<IList<FarmProductDto>> GetSoldFarmProductByFarmerAsync(string email)
        {
            var farmProducts = await _context.FarmProducts.Include(a => a.Farm).Include(a => a.Farmer).Where(a => a.Farmer.Email == email && a.FarmProductStatus == FarmProductStatus.Sold && a.IsDeleted == false).ToListAsync();
            if (farmProducts == null)
            {
                return null;
            }
            return farmProducts.Select(a => new FarmProductDto
            {
                Id = a.Id,
                State = a.State,
                Country = a.Country,
                LocalGovernment = a.LocalGoverment,
                FarmProduct = a.FarmProduce,
                FarmProductStatus = a.FarmProductStatus.ToString(),
                Quantity = a.Quantity,
                Grade = a.Grade.ToString(),
                FarmerEmail = a.Farm.Farmer.Email,
                FarmerId = a.Farm.Farmer.Id,
                FarmId = a.FarmId,
                Price = (decimal)a.Price,
                HarvestedTime = a.HarvestedPeriod,
                ProductImage1 = a.ProductImage1,
                ProductImage2 = a.ProductImage2

            }).ToList();
        }

        public async Task<IList<FarmProductDto>> GetToBeUpdatedFarmProductByFarmerEmailReturningFarmProductObjectAsync(string email)
        {
            var farmProduct = await _context.FarmProducts.Include(a => a.Farm).ThenInclude(a => a.Farmer).Where(a => a.Farmer.Email == email && a.Price == null && a.IsDeleted == false).ToListAsync();
            if (farmProduct == null)
            {
                return null;
            }
            return farmProduct.Select(a => new FarmProductDto
            {
                Id = a.Id,
                State = a.State,
                Country = a.Country,
                LocalGovernment = a.LocalGoverment,
                FarmProduct = a.FarmProduce,
                FarmProductStatus = a.FarmProductStatus.ToString(),
                Quantity = a.Quantity,
                Grade = a.Grade.ToString(),
                FarmerEmail = a.Farm.Farmer.Email,
                FarmerId = a.Farm.Farmer.Id,
                FarmId = a.FarmId,
                // Price = (decimal)a.Price,
                HarvestedTime = a.HarvestedPeriod,
                ProductImage1 = a.ProductImage1,
                ProductImage2 = a.ProductImage2

            }).ToList();
        }

        public async Task<IList<FarmProductDto>> GetToBeUpdatedFarmProductByFarmerInspectorReturningFarmProductObjectAsync(string email)
        {
            var farmProduce = await _context.FarmProducts.Include(a => a.Farm).ThenInclude(a => a.Farmer).Include(a => a.OrderProducts).Include(a => a.FarmReport).ThenInclude(a => a.FarmInspector).Where(a => a.Price > 0 && a.FarmReport.FarmInspector.Email == email && a.OrderProducts.Count == 0 && a.IsDeleted == false).ToListAsync();
            if (farmProduce == null)
            {
                return null;
            }
            return farmProduce.Select(a => new FarmProductDto
            {
                Id = a.Id,
                State = a.State,
                Country = a.Country,
                LocalGovernment = a.LocalGoverment,
                FarmProduct = a.FarmProduce,
                FarmProductStatus = a.FarmProductStatus.ToString(),
                Quantity = a.Quantity,
                Grade = a.Grade.ToString(),
                FarmerEmail = a.Farm.Farmer.Email,
                FarmId = a.FarmId,
                FarmerId = a.Farm.Farmer.Id,
                Price = (decimal)a.Price,
                FarmInspectorId = a.FarmReport.FarmInspectorId,
                HarvestedTime = a.HarvestedPeriod,
                ProductImage1 = a.ProductImage1,
                ProductImage2 = a.ProductImage2
            }).ToList();
        }

        public async Task<IEnumerable<FarmProductDto>> Search(string searchText)
        {

            var product = await _context.FarmProducts.Include(a => a.Farm).ThenInclude(a => a.Farmer).Include(a => a.OrderProducts).Include(a => a.FarmReport).ThenInclude(a => a.FarmInspector).Where(product => EF.Functions.Like(product.FarmProduce, $"%{searchText}%") && product.IsDeleted == false && product.FarmProductStatus == FarmProductStatus.Available).ToListAsync();
            if (product.Count == 0)
            {
                return null;


            }

            return product.Select(a => new FarmProductDto
            {
                Id = a.Id,
                State = a.State,
                Country = a.Country,
                LocalGovernment = a.LocalGoverment,
                FarmProduct = a.FarmProduce,
                FarmProductStatus = a.FarmProductStatus.ToString(),
                Quantity = a.Quantity,
                Grade = a.Grade.ToString(),
                FarmerEmail = a.Farm.Farmer.Email,
                FarmId = a.FarmId,
                Price = (decimal)a.Price,
                HarvestedTime = a.HarvestedPeriod,
                ProductImage1 = a.ProductImage1,
                ProductImage2 = a.ProductImage2


            }).ToList();
        }

        public async Task<FarmProductDto> UpdateAsync(FarmProduct farmProduct)
        {
            _context.FarmProducts.Update(farmProduct);
            await _context.SaveChangesAsync();
            return new FarmProductDto
            {
                Id = farmProduct.Id,
                State = farmProduct.State,
                Country = farmProduct.Country,
                LocalGovernment = farmProduct.LocalGoverment,
                FarmProduct = farmProduct.FarmProduce,
                FarmProductStatus = farmProduct.FarmProductStatus.ToString(),
                Quantity = farmProduct.Quantity,
                Grade = farmProduct.Grade.ToString(),
                Price = decimal.Parse(farmProduct.Price.ToString()),
                ProductImage1 = farmProduct.ProductImage1,
                ProductImage2 = farmProduct.ProductImage2
            };
        }
    }
}