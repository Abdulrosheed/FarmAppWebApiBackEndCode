using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;

namespace FirstProject.Interfaces.Services
{
    public interface IOrderService
    {
         Task<BaseResponse<OrderDto>> CreateAsync (CreateOrderRequestModel model);
        //  Task<BaseResponse<OrderDto>> PlaceRequest (int id);
        Task<BaseResponse<OrderDto>> UpdateAsync (UpdateOrderRequestModel model);
        
        Task<BaseResponse<IEnumerable<OrderDto>>> GetAllOrdersAsync ();
        Task<BaseResponse<IEnumerable<OrderDto>>> GetAllPaidOrderAsync ();
        Task<BaseResponse<IEnumerable<OrderDto>>> GetAllInitializedOrderByCompanyAsync (string email);
        Task<BaseResponse<IEnumerable<OrderDto>>> GetAllPaidOrderByCompanyAsync (string email);
        Task<BaseResponse<IEnumerable<OrderDto>>> GetAllUnPaidOrderAsync ();
        Task<BaseResponse<IEnumerable<OrderDto>>> GetAllOrderByDateAsync (int date);
        Task<BaseResponse<IEnumerable<OrderDto>>> GetAllOrderByDateByCompanyAsync (string email , DateTime date);
        Task<BaseResponse<IEnumerable<OrderDto>>> GetAllOrderByCompanyEmailAsync (string email);
        Task<BaseResponse<OrderDto>> GetOrderByOrderReferenceReturningOrderDtoObjectAsync (string orderReference);
    
    }
}