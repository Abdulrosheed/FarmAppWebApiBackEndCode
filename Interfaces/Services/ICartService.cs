using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;

namespace FirstProject.Interfaces.Services
{
    public interface ICartService
    {
          Task<BaseResponse<CartDto>> CreateAsync (CreateCartRequestModel model);
        Task<BaseResponse<CartDto>> UpdateCartItemQuantityAsync (UpdateItemCartRequestModel model);
        Task<BaseResponse<CartDto>> UpdateAsync (UpdateCartRequestModel model);
        // void DeleteAsync (Admin admin);
        Task<BaseResponse<CartDto>> GetCartByCompanyIdAsync (int CompanyId);
        Task<BaseResponse<IEnumerable<CartDto>>> GetAllCartAsync ();
        Task<BaseResponse<CartDto>> GetCartByIdReturningCartDtoObjectAsync (int id);
    }
}