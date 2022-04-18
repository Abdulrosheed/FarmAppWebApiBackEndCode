using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;

namespace FirstProject.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<BaseResponse<CategoryDto>> RegisterAsync (CreateCategoryRequestModel model);
        Task<BaseResponse<CategoryDto>> UpdateAsync (UpdateCategoryRequestModel model , int id);
        Task<BaseResponse<CategoryDto>> DeleteAsync (int id);
        Task<BaseResponse<IEnumerable<CategoryDto>>> GetAllCategoryAsync ();
        Task<BaseResponse<CategoryDto>> GetCategoryAsync (int id);
    }
}