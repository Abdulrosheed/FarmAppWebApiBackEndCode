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
    public class RequestRepository : IRequestRepository
    {
        private readonly ApplicationContext _context;

        public RequestRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<RequestDto> CreateAsync(Request request)
        {
            await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
            return new RequestDto
            {
                Id = request.Id,
                Status = request.Status.ToString(),
                Grade = request.Grade.ToString(),
                Quantity = request.Quantity,
                
            };
        }

        public async Task<RequestDto> DetailsAsync(int id)
        {
            var request = await _context.Requests.Include(a => a.FarmProductRequests).ThenInclude(a => a.FarmProduct).ThenInclude(a => a.Farmer).Include(a => a.Company).Include(a => a.FarmProduce).SingleOrDefaultAsync(a => a.Id == id);
            if (request == null)
            {
                return null;
            }
            return new RequestDto
            {
                Id = request.Id,
                Quantity = request.Quantity,
                CompanyName = request.Company.Name,
                Grade = request.Grade.ToString(),
                MonthNeeded = request.MonthNeeded,
                YearNeeded = request.YearNeeded,
                Status = request.Status.ToString(),
                FarmProduce = request.FarmProduce.Name,
                FarmProducts = request.FarmProductRequests.Select(a => new FarmProductDto
                {
                    State = a.FarmProduct.State,
                    Country = a.FarmProduct.Country,
                    FarmerEmail =a.FarmProduct.Farmer.Email,
                    FarmProduct = a.FarmProduct.FarmProduce

                }).ToList()
            };
        }

        public async Task<IEnumerable<RequestDto>> GetAllFulfilledRequestAsync()
        {
            var request = await _context.Requests.Include(a => a.FarmProductRequests).ThenInclude(a => a.FarmProduct).ThenInclude(a => a.Farmer).Include(a => a.Company).Include(a => a.FarmProduce).Where(a => a.Status == RequestStatus.Fulfilled).ToListAsync();
            if (request == null)
            {
                return null;
            }
            return request.Select(a => new RequestDto
            {
                Id = a.Id,
                Quantity = a.Quantity,
                CompanyName = a.Company.Name,
                Grade = a.Grade.ToString(),
                MonthNeeded = a.MonthNeeded,
                YearNeeded = a.YearNeeded,
                  Status = a.Status.ToString(),
                FarmProduce = a.FarmProduce.Name,
                FarmProducts = a.FarmProductRequests.Select(a => new FarmProductDto
                {
                    State = a.FarmProduct.State,
                    Country = a.FarmProduct.Country,
                    FarmerEmail =a.FarmProduct.Farmer.Email,
                    FarmProduct = a.FarmProduct.FarmProduce

                }).ToList()
            }).AsEnumerable();
        }

        public async Task<IEnumerable<RequestDto>> GetAllFulfilledRequestByCompanyAsync(int id)
        {
            var request = await _context.Requests.Include(a => a.FarmProductRequests).ThenInclude(a => a.FarmProduct).ThenInclude(a => a.Farmer).Include(a => a.Company).Include(a => a.FarmProduce).Where(a => a.Status == RequestStatus.Fulfilled && a.CompanyId == id).ToListAsync();
            if (request == null)
            {
                return null;
            }
            return request.Select(a => new RequestDto
            {
                 Id = a.Id,
                Quantity = a.Quantity,
                CompanyName = a.Company.Name,
                Grade = a.Grade.ToString(),
                MonthNeeded = a.MonthNeeded,
                YearNeeded = a.YearNeeded,
                  Status = a.Status.ToString(),
                FarmProduce = a.FarmProduce.Name,
                FarmProducts = a.FarmProductRequests.Select(a => new FarmProductDto
                {
                    State = a.FarmProduct.State,
                    Country = a.FarmProduct.Country,
                    FarmerEmail =a.FarmProduct.Farmer.Email,
                    FarmProduct = a.FarmProduct.FarmProduce

                }).ToList()
            }).AsEnumerable();
        }

        public async Task<IEnumerable<RequestDto>> GetAllMergedRequestAsync()
        {
            var request = await _context.Requests.Include(a => a.FarmProductRequests).ThenInclude(a => a.FarmProduct).ThenInclude(a => a.Farmer).Include(a => a.Company).Include(a => a.FarmProduce).Where(a => a.Status == RequestStatus.Fulfilled).ToListAsync();
            if (request == null)
            {
                return null;
            }
            return request.Select(a => new RequestDto
            {
                Id = a.Id,
                Quantity = a.Quantity,
                CompanyName = a.Company.Name,
                Grade = a.Grade.ToString(),
                MonthNeeded = a.MonthNeeded,
                YearNeeded = a.YearNeeded,
                  Status = a.Status.ToString(),
                FarmProduce = a.FarmProduce.Name,
                FarmProducts = a.FarmProductRequests.Select(a => new FarmProductDto
                {
                    State = a.FarmProduct.State,
                    Country = a.FarmProduct.Country,
                    FarmerEmail =a.FarmProduct.Farmer.Email,
                    FarmProduct = a.FarmProduct.FarmProduce

                }).ToList()
            }).AsEnumerable();
        }

        public async Task<IEnumerable<RequestDto>> GetAllMergedRequestByCompanyAsync(int id)
        {
            var request = await _context.Requests.Include(a => a.FarmProductRequests).ThenInclude(a => a.FarmProduct).ThenInclude(a => a.Farmer).Include(a => a.Company).Include(a => a.FarmProduce).Where(a => a.Status == RequestStatus.Merged).ToListAsync();
            if (request == null)
            {
                return null;
            }
            return request.Select(a => new RequestDto
            {
                Id = a.Id,
                Quantity = a.Quantity,
                CompanyName = a.Company.Name,
                Grade = a.Grade.ToString(),
                MonthNeeded = a.MonthNeeded,
                YearNeeded = a.YearNeeded,
                  Status = a.Status.ToString(),
                FarmProduce = a.FarmProduce.Name,
                FarmProducts = a.FarmProductRequests.Select(a => new FarmProductDto
                {
                    State = a.FarmProduct.State,
                    Country = a.FarmProduct.Country,
                    FarmerEmail =a.FarmProduct.Farmer.Email,
                    FarmProduct = a.FarmProduct.FarmProduce

                }).ToList()
            }).AsEnumerable();
        }

        public async Task<IEnumerable<RequestDto>> GetAllPendingRequestAsync()
        {
            var request = await _context.Requests.Include(a => a.FarmProductRequests).ThenInclude(a => a.FarmProduct).ThenInclude(a => a.Farmer).Include(a => a.Company).Include(a => a.FarmProduce).Where(a => a.Status == RequestStatus.Pending).ToListAsync();
            if (request == null)
            {
                return null;
            }
            return request.Select(a => new RequestDto
            {
                Id = a.Id,
                Quantity = a.Quantity,
                CompanyName = a.Company.Name,
                Grade = a.Grade.ToString(),
                MonthNeeded = a.MonthNeeded,
                YearNeeded = a.YearNeeded,
                  Status = a.Status.ToString(),
                FarmProduce = a.FarmProduce.Name,
                FarmProducts = a.FarmProductRequests.Select(a => new FarmProductDto
                {
                    State = a.FarmProduct.State,
                    Country = a.FarmProduct.Country,
                    FarmerEmail =a.FarmProduct.Farmer.Email,
                    FarmProduct = a.FarmProduct.FarmProduce

                }).ToList()
            }).AsEnumerable();
        }

        public async Task<IEnumerable<RequestDto>> GetAllPendingRequestByCompanyAsync(int id)
        {
            var request = await _context.Requests.Include(a => a.FarmProductRequests).ThenInclude(a => a.FarmProduct).ThenInclude(a => a.Farmer).Include(a => a.Company).Include(a => a.FarmProduce).Where(a => a.Status == RequestStatus.Pending && a.CompanyId == id).ToListAsync();
            if (request == null)
            {
                return null;
            }
            return request.Select(a => new RequestDto
            {
                Id = a.Id,
                Quantity = a.Quantity,
                CompanyName = a.Company.Name,
                Grade = a.Grade.ToString(),
                MonthNeeded = a.MonthNeeded,
                YearNeeded = a.YearNeeded,
                FarmProduce = a.FarmProduce.Name,
                Status = a.Status.ToString(),
                FarmProducts = a.FarmProductRequests.Select(a => new FarmProductDto
                {
                    State = a.FarmProduct.State,
                    Country = a.FarmProduct.Country,
                    FarmerEmail =a.FarmProduct.Farmer.Email,
                    FarmProduct = a.FarmProduct.FarmProduce

                }).ToList()
            }).AsEnumerable();
        }

        public async Task<IList<Request>> GetAllPendingRequestReturningObjectAsync()
        {
            var request = await _context.Requests.Include(a => a.FarmProductRequests).ThenInclude(a => a.FarmProduct).ThenInclude(a => a.Farmer).Include(a => a.Company).Include(a => a.FarmProduce).Where(a => a.Status != RequestStatus.Fulfilled).ToListAsync();
            if (request == null)
            {
                return null;
            }
            return request;
        }

        public async Task<Request> GetByIdAsync(int id)
        {
           var request = await _context.Requests.Include(a => a.FarmProductRequests).ThenInclude(a => a.FarmProduct).ThenInclude(a => a.Farmer).Include(a => a.Company).Include(a => a.FarmProduce).SingleOrDefaultAsync(a => a.Id == id);
           if(request == null)
           {
               return null;
           }
           return request;
        }

        public async Task<RequestDto> UpdateAsync(Request request)
        {
            _context.Requests.Update(request);
            await _context.SaveChangesAsync();
            return new RequestDto
            {
                  Id = request.Id,
                Status = request.Status.ToString(),
                Grade = request.Grade.ToString(),
                Quantity = request.Quantity,
                  
            };
        }
    }
}