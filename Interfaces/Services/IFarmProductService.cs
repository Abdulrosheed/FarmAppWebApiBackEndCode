using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;

namespace FirstProject.Interfaces.Services
{
    public interface IFarmProductService
    {
        //  Task <BaseResponse<FarmProductDto>> CreateAsync (CreateFarmProductRequestModel model);
        Task <BaseResponse<FarmProductDto>> UpdateAsync (UpdateFarmProductForAdminRequestModel model , int id);
        Task <BaseResponse<FarmProductDto>> UpdateAsync (UpdateFarmProductForFarmerRequestModel model , int id);
          Task <BaseResponse<FarmProductDto>> DeleteAsync (int id);
        Task<BaseResponse<IEnumerable<FarmProductDto>>> GetAllFarmProductsAsync ();
            Task<BaseResponse<IEnumerable<SearchRequestFarmProductDto>>> SearchRequest (int id);
        Task <BaseResponse<FarmProductDto>> GetFarmProductByIdAsync (int id);
        Task <BaseResponse<IList<FarmProductDto>>> GetFarmProductByFarmerEmailAsync (string email);
        Task <BaseResponse<IList<FarmProductDto>>> GetSoldFarmProductByFarmerEmailAsync (string email);
        Task <BaseResponse<IList<FarmProductDto>>> GetNotSoldFarmProductByFarmerEmailAsync (string email);
          Task<BaseResponse<IEnumerable<FarmProductDto>>> GetAllFarmProductsInspectedByFarmInspectorAsync (string email);
        Task <BaseResponse<IList<FarmProductDto>>> GetToBeUpdatedFarmProductByFarmerEmailAsync (string email);
        Task <BaseResponse<IList<FarmProductDto>>> GetToBeUpdatedFarmProductByFarmerInspectorEmailAsync (string email);
        Task<BaseResponse<IEnumerable<FarmProductDto>>> GetFarmProductsByStateAndQuantityAndFarmProduceAsync (string state);
        Task <BaseResponse<IEnumerable<FarmProductDto>>> GetFarmProductsByLocalGovernmentAndQuantityAndFarmProduceAsync (string localGoverment);
    
    }
}