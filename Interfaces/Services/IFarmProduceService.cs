using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;

namespace FirstProject.Interfaces.Services
{
    public interface IFarmProduceService
    {
        Task<BaseResponse<FarmProduceDto>> RegisterAsync (CreateFarmProduceRequestModel model);
        Task<BaseResponse<FarmProduceDto>> UpdateAsync (UpdateFarmProduceRequestModel model , int id);
        Task<BaseResponse<FarmProduceDto>> DeleteAsync (int id);
        Task<BaseResponse<IEnumerable<FarmProduceDto>>> GetAllFarmProduceAsync ();
        Task<BaseResponse<FarmProduceDto>> GetFarmProduceAsync (int id);
    }
}