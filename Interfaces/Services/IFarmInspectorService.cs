using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;

namespace FirstProject.Interfaces.Services
{
    public interface IFarmInspectorService
    {
         Task<BaseResponse<FarmInspectorDto>> RegisterAsync (CreateFarmInspectorRequestModel model);
        Task<BaseResponse<FarmInspectorDto>> UpdateAsync (UpdateFarmInspectorRequestModel model , int id);
        Task<BaseResponse<FarmInspectorDto>> DeleteAsync (int id);
        Task<BaseResponse<IEnumerable<FarmInspectorDto>>> GetAllFarmInspectorAsync ();
        Task<BaseResponse<FarmInspectorDto>> GetFarmInspectorAsync (int id);
        Task<BaseResponse<FarmInspectorDto>> GetFarmInspectorByEmailAsync (string email);
        Task<BaseResponse<IList<FarmInspectorDto>>> GetFarmInspectorByStateAsync (string state);
       Task<BaseResponse<IList<FarmInspectorDto>>> GetFarmInspectorByCountryAsync (string country);
        Task<BaseResponse<IList<FarmInspectorDto>>> GetFarmInspectorByLocalGovernmentAsync (string localGoverment);
    }
}