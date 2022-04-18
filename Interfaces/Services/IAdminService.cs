using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;

namespace FirstProject.Interfaces.Services
{
    public interface IAdminService
    {
        Task<BaseResponse<AdminDto>> RegisterAsync (CreateAdminRequestModel model);
        Task<BaseResponse<AdminDto>> UpdateAsync (UpdateAdminRequestModel model , int id);
   
        Task<BaseResponse<AdminDto>> DeleteAsync (int id);
        Task<BaseResponse<IEnumerable<AdminDto>>> GetAllAdminAsync ();
        Task<BaseResponse<AdminDto>> GetAdminAsync (int id);
        Task<BaseResponse<AdminDto>> GetAdminByEmailAsync (string email);

    }
}