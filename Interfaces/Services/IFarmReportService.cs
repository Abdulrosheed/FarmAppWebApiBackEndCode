using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Enums;

namespace FirstProject.Interfaces.Services
{
    public interface IFarmReportService
    {
         Task <BaseResponse<FarmReportDto>> CreateAsync (string email ,CreateFarmReportRequestModel model , SendFarmProductPics farmProductPics);
         Task <BaseResponse<FarmReportDto>> CreateForUpdateAsync (string email, UpdateFarmReportRequestModel model);
        Task <BaseResponse<FarmReportDto>> UpdateAsync (UpdateFarmReportRequestModel model , int id);
        Task <BaseResponse<FarmReportDto>> DeleteAsync (int id);
        Task<BaseResponse<IEnumerable<FarmReportDto>>> GetAllFarmReportsAsync ();
        Task<BaseResponse<IEnumerable<FarmReportDto>>> GetAllApprovedFarmReportsAsync ();
        Task<BaseResponse<IEnumerable<FarmReportDto>>> GetAllDeclinedFarmReportsAsync ();
        Task<BaseResponse<IEnumerable<FarmReportDto>>> GetAllUpdatedFarmReportsAsync ();
        Task<BaseResponse<IEnumerable<FarmReportDto>>> GetAllApprovedFarmReportsByFarmInspectorEmailAsync (string email);
        Task <BaseResponse<FarmReportDto>> GetFarmReportAsync (int id);
        Task <BaseResponse<FarmReportDto>> GetFarmReportByFarmIdAsync (int id);
        Task <BaseResponse<FarmReportDto>> GetFarmReportByFarmInspectorEmailAsync (string email);
        Task <BaseResponse<FarmProductDto>> ApproveFarmReport (int id);
        Task <BaseResponse<FarmProductDto>> ApproveUpdatedFarmReport (int id);
        Task <BaseResponse<FarmReportDto>> DeclineFarmReport (int id);
         Task<BaseResponse<IEnumerable<FarmReportDto>>> GetAllProcessingFarmReportsAsync ();

    }
}