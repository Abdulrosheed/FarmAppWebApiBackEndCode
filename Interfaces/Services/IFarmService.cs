using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;

namespace FirstProject.Interfaces.Services
{
    public interface IFarmService
    {
        Task<BaseResponse<FarmDto>> RegisterAsync (CreateFarmRequestModel model , string email);
        // Task<BaseResponse<FarmDto>> UpdateAsync (UpdateFarmRequestModel model , int id);
        Task<BaseResponse<FarmDto>> UpdateFarmAsync ( int farmInspectorId , int id , DateTime inspectionDate);
        Task<BaseResponse<FarmDto>> UpdateFarmAsync (  int id);
        Task<BaseResponse<FarmDto>> DeleteAsync (int id);
        Task<BaseResponse<IEnumerable<FarmDto>>> GetAllFarmAsync ();
        Task<BaseResponse<FarmDto>> GetFarmAsync (int id);
        Task<BaseResponse<IEnumerable<FarmDto>>> GetAllProcessingFarmAsync ();
        Task<BaseResponse<IEnumerable<FarmDto>>> GetAllApprovedFarmByFarmerAsync (string email);
        Task<BaseResponse<IEnumerable<FarmDto>>> GetAllDeclinedFarmAsync ();
        Task<BaseResponse<IEnumerable<FarmDto>>> GetAllDeclinedFarmByFarmerAsync (string email);
        Task<BaseResponse<IEnumerable<FarmDto>>> GetAllAssignedFarmAsync ();
               Task<BaseResponse<IList<FarmDto>>> GetAllInspectedFarmerByFarmInspectorEmailAsync (string farmInspectorEmail);
       Task<BaseResponse<IList<FarmDto>>> GetTobeInspectedFarmerByFarmInspectorEmailAsync (string farmInspectorEmail);
    }
}