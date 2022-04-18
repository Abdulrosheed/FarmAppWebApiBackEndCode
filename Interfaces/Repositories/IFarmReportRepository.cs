using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;
using FirstProject.Enums;

namespace FirstProject.Interfaces.Repositories
{
    public interface IFarmReportRepository
    {
        Task<FarmReportDto> CreateAsync (FarmReport farmReport);
        Task<FarmReportDto> UpdateAsync (FarmReport farmReport);
        // void DeleteAsync ( FarmReport farmReport);
        Task<IEnumerable<FarmReportDto>> GetAllFarmReportsAsync ();
    
        Task<IEnumerable<FarmReportDto>> GetAllUpdatedFarmReportsAsync ();
        Task<IEnumerable<FarmReportDto>> GetAllProcessingFarmReportsAsync ();
        Task<IEnumerable<FarmReportDto>> GetAllApprovedFarmReportsAsync ();
        Task<IEnumerable<FarmReportDto>> GetAllDeclinedFarmReportsAsync ();
        Task<IEnumerable<FarmReportDto>> GetAllApprovedFarmReportsByFarmInspectorAsync (string email);
        Task<FarmReportDto> GetFarmReportReturningFarmReportDtoObjectAsync (int id);
        Task<FarmReportDto> GetFarmReportByFarmIdReturningFarmReportDtoObjectAsync (int id);
        Task<FarmReportDto> GetFarmReportByFarmInspectorEmailReturningFarmReportDtoObjectAsync (string email);
        Task<FarmReport> GetFarmReportReturningFarmReportObjectAsync (int id);
        
    }
}