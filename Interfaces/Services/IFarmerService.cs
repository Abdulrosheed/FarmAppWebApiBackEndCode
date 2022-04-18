using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;
using FirstProject.Enums;

namespace FirstProject.Interfaces.Services
{
    public interface IFarmerService
    {
         Task<BaseResponse<FarmerDto>> RegisterAsync (CreateFarmerRequestModel model );
        Task<BaseResponse<FarmerDto>> UpdateAsync (UpdateFarmerRequestModel model , int id);
        
        Task<BaseResponse<FarmerDto>>  DeleteAsync (int id);
        
        Task<BaseResponse<IEnumerable<FarmerDto>>>  GetAllFarmersAsync ();
       Task<BaseResponse<FarmerDto>> GetFarmerByIdAsync (int id);
            Task<BaseResponse<FarmerDto>> UpdateFarmerAsync (int id);
        
        Task<BaseResponse<FarmerDto>> GetFarmerByEmailAsync (string email);
 
       Task<BaseResponse<IList<FarmerDto>>> AllProcessingFarmerAprovalAsync();
       Task<BaseResponse<IList<FarmerDto>>> AllDeclinedFarmerAprovalAsync();
       Task<BaseResponse<IList<FarmerDto>>> AllAssignedFarmerAprovalAsync();
       Task<BaseResponse<FarmerDto>> ChangeFarmerStatusToDeclinedAsync(int id);
        
    }
}