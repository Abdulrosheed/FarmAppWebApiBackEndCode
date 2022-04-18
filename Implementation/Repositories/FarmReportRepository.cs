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
    public class FarmReportRepository : IFarmReportRepository
    {
        private readonly ApplicationContext _context;

        public FarmReportRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<FarmReportDto> CreateAsync(FarmReport farmReport)
        {
            await _context.FarmReports.AddAsync(farmReport);
           await _context.SaveChangesAsync();
           return new FarmReportDto
           {
               Id = farmReport.Id,

               Quantity = farmReport.Quantity,
               Grade = farmReport.Grade.ToString()
           };
        }

        // public async void DeleteAsync(FarmReport farmReport)
        // {
        //     _context.FarmReports.Remove(farmReport);
        //     await _context.SaveChangesAsync();
        // }

        public async Task<IEnumerable<FarmReportDto>> GetAllApprovedFarmReportsAsync()
        {
             var farmReport =  await _context.FarmReports.Include(a => a.Farm).ThenInclude(a => a.Farmer).Include(a => a.FarmInspector).Where(a => a.FarmReportStatus == FarmReportStatus.Approved && a.IsDeleted == false).ToListAsync();
            if(farmReport == null)
            {
                return null;
            }
            return farmReport.Select(a => new FarmReportDto
           {
                Id = a.Id,
               FarmProduct = a.FarmProduct,
               Quantity = a.Quantity,
               Grade = a.Grade.ToString(),
               FarmId = a.FarmId,
               FarmInspectorEmail = a.FarmInspector.Email,
               FarmerEmail = a.Farm.Farmer.Email,
                 State = a.Farm.State,
               LocalGovernment = a.Farm.LocalGoverment,
               Country = a.Farm.Country
           }).AsEnumerable();
        }

        public async Task<IEnumerable<FarmReportDto>> GetAllApprovedFarmReportsByFarmInspectorAsync(string email)
        {
            var farmReport = await _context.FarmReports.Include(a => a.Farm).ThenInclude(a => a.Farmer).Include(a => a.FarmInspector).Where(a => a.FarmReportStatus == FarmReportStatus.Approved && a.FarmInspector.Email == email && a.IsDeleted == false).ToListAsync();
           if(farmReport == null)
           {
               return null;
           }
           return farmReport.Select(a => new FarmReportDto
           {
                Id = a.Id,
               FarmProduct = a.FarmProduct,
               Quantity = a.Quantity,
               Grade = a.Grade.ToString(),
               FarmId = a.FarmId,
               FarmInspectorEmail = a.FarmInspector.Email,
               FarmerEmail = a.Farm.Farmer.Email,
                 State = a.Farm.State,
               LocalGovernment = a.Farm.LocalGoverment,
               Country = a.Farm.Country
               
           }).AsEnumerable();
        }

        public async Task<IEnumerable<FarmReportDto>> GetAllDeclinedFarmReportsAsync()
        {
            var farmReport =  await _context.FarmReports.Include(a => a.Farm).ThenInclude(a => a.Farmer).Include(a => a.FarmInspector).Where(a => a.FarmReportStatus == FarmReportStatus.Declined && a.IsDeleted == false).ToListAsync();
            if(farmReport == null)
            {
                return null;
            }
            return farmReport.Select(a => new FarmReportDto
           {
                Id = a.Id,
               FarmProduct = a.FarmProduct,
               Quantity = a.Quantity,
               Grade = a.Grade.ToString(),
               FarmId = a.FarmId,
               FarmInspectorEmail = a.FarmInspector.Email,
               FarmerEmail = a.Farm.Farmer.Email,
                 State = a.Farm.State,
               LocalGovernment = a.Farm.LocalGoverment,
               Country = a.Farm.Country
           }).AsEnumerable();
        }

        public async Task<IEnumerable<FarmReportDto>> GetAllFarmReportsAsync()
        {
           var farmReport = await _context.FarmReports.Include(a => a.Farm).ThenInclude(a => a.Farmer).Include(a => a.FarmInspector).Where(a => a.FarmReportStatus == FarmReportStatus.ProcessingApproval && a.IsDeleted == false).ToListAsync();
           if(farmReport == null)
           {
               return null;
           }
           return farmReport.Select(a => new FarmReportDto
           {
                Id = a.Id,
               FarmProduct = a.FarmProduct,
               Quantity = a.Quantity,
               Grade = a.Grade.ToString(),
               FarmId = a.FarmId,
               FarmInspectorEmail = a.FarmInspector.Email,
               FarmerEmail = a.Farm.Farmer.Email,
                 State = a.Farm.State,
               LocalGovernment = a.Farm.LocalGoverment,
               Country = a.Farm.Country
               
           }).AsEnumerable();
            

        }

        public async Task<IEnumerable<FarmReportDto>> GetAllProcessingFarmReportsAsync()
        {
            var farmReport =  await _context.FarmReports.Include(a => a.Farm).ThenInclude(a => a.Farmer).Include(a => a.FarmInspector).Where(a => a.FarmReportStatus == FarmReportStatus.ProcessingApproval && a.IsDeleted == false).ToListAsync();
            if(farmReport == null)
            {
                return null;
            }
            return farmReport.Select(a => new FarmReportDto
           {
                   Id = a.Id,
               FarmProduct = a.FarmProduct,
               Quantity = a.Quantity,
               Grade = a.Grade.ToString(),
               FarmId = a.FarmId,
               FarmInspectorEmail = a.FarmInspector.Email,
               FarmerEmail = a.Farm.Farmer.Email,
                 State = a.Farm.State,
               LocalGovernment = a.Farm.LocalGoverment,
               Country = a.Farm.Country

           }).AsEnumerable();
        }

        public async Task<IEnumerable<FarmReportDto>> GetAllUpdatedFarmReportsAsync()
        {
            var farmReport =  await _context.FarmReports.Include(a => a.Farm).ThenInclude(a => a.Farmer).Include(a => a.FarmInspector).Where(a => a.FarmReportStatus == FarmReportStatus.ProcessingUpdateApproval && a.IsDeleted == false).ToListAsync();
            if(farmReport == null)
            {
                return null;
            }
            return farmReport.Select(a => new FarmReportDto
           {
                   Id = a.Id,
               FarmProduct = a.FarmProduct,
               Quantity = a.Quantity,
               Grade = a.Grade.ToString(),
               FarmId = a.FarmId,
               FarmInspectorEmail = a.FarmInspector.Email,
               FarmerEmail = a.Farm.Farmer.Email,
               HarvestedTime = a.HarvestedPeriod.Month,
                 State = a.Farm.State,
               LocalGovernment = a.Farm.LocalGoverment,
               Country = a.Farm.Country
               
           }).AsEnumerable();
        }

        public async Task<FarmReportDto> GetFarmReportByFarmIdReturningFarmReportDtoObjectAsync(int id)
        {
            var farmReport = await _context.FarmReports.Include(a => a.Farm).ThenInclude(a => a.Farmer).Include(a => a.FarmInspector).FirstOrDefaultAsync(a => a.FarmId == id && a.IsDeleted == false);
            if(farmReport == null)
            {
                return null;
            }
            return new FarmReportDto
            {
                Id = farmReport.Id,
               FarmProduct = farmReport.FarmProduct,
               Quantity = farmReport.Quantity,
               Grade = farmReport.Grade.ToString(),
               FarmId = farmReport.FarmId,
               FarmInspectorEmail = farmReport.FarmInspector.Email,
               FarmerEmail = farmReport.Farm.Farmer.Email,
                 State = farmReport.Farm.State,
               LocalGovernment = farmReport.Farm.LocalGoverment,
               Country = farmReport.Farm.Country
            };
        }

        public async Task<FarmReportDto> GetFarmReportByFarmInspectorEmailReturningFarmReportDtoObjectAsync(string email)
        {
            
            var farmReport = await _context.FarmReports.Include(a => a.Farm).ThenInclude(a => a.Farmer).Include(a => a.FarmInspector).FirstOrDefaultAsync(a => a.FarmInspector.Email == email && a.IsDeleted == false);
            
            if(farmReport == null)
            {
                return null;
            }
            return new FarmReportDto
            {
                Id = farmReport.Id,
               FarmProduct = farmReport.FarmProduct,
               Quantity = farmReport.Quantity,
               Grade = farmReport.Grade.ToString(),
               FarmId = farmReport.FarmId,
               FarmInspectorEmail = farmReport.FarmInspector.Email,
               FarmerEmail = farmReport.Farm.Farmer.Email,
                 State = farmReport.Farm.State,
               LocalGovernment = farmReport.Farm.LocalGoverment,
               Country = farmReport.Farm.Country
            };
        }

        public async Task<FarmReportDto> GetFarmReportReturningFarmReportDtoObjectAsync(int id)
        {
            var farmReport = await _context.FarmReports.Include(a => a.Farm).ThenInclude(a => a.Farmer).Include(a => a.FarmInspector).FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
            
            if(farmReport == null)
            {
                return null;
            }
            return new FarmReportDto
            {
                Id = farmReport.Id,
               FarmProduct = farmReport.FarmProduct,
               Quantity = farmReport.Quantity,
               Grade = farmReport.Grade.ToString(),
               FarmId = farmReport.FarmId,
               FarmInspectorEmail = farmReport.FarmInspector.Email,
               FarmerEmail = farmReport.Farm.Farmer.Email,
               State = farmReport.Farm.State,
               LocalGovernment = farmReport.Farm.LocalGoverment,
               Country = farmReport.Farm.Country

            };
        }

        public async Task<FarmReport> GetFarmReportReturningFarmReportObjectAsync(int id)
        {
            return await _context.FarmReports.Include(a => a.Farm).ThenInclude(a => a.Farmer).Include(a => a.FarmInspector).FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
        }

        public async Task<FarmReportDto> UpdateAsync(FarmReport farmReport)
        {
             _context.FarmReports.Update(farmReport);
            await _context.SaveChangesAsync();
            return new FarmReportDto
            {
                Id = farmReport.Id,
               FarmProduct = farmReport.FarmProduct,
               Quantity = farmReport.Quantity,
               Grade = farmReport.Grade.ToString(),
                 State = farmReport.Farm.State,
               LocalGovernment = farmReport.Farm.LocalGoverment,
               Country = farmReport.Farm.Country
               
            };
        }
    }
}