using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;

namespace FirstProject.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<OrderDto> CreateAsync (Order order);
        Task<OrderDto> UpdateAsync (Order order);
        // void DeleteAsync (Company company);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync ();
        Task<IEnumerable<OrderDto>> GetAllPaidOrderAsync ();
        Task<IEnumerable<OrderDto>> GetPaidOrderByCompanyAsync (string email);
        Task<IEnumerable<OrderDto>> GetInitializedOrderByCompanyAsync (string email);
        Task<IEnumerable<OrderDto>> GetAllUnPaidOrderAsync ();
        Task<IEnumerable<OrderDto>> GetAllOrderByDateAsync (int date);
        Task<IEnumerable<OrderDto>> GetAllOrderByDateByCompanyAsync (string email , DateTime date);
        Task<IEnumerable<OrderDto>> GetAllOrderByCompanyEmailAsync (string email);
        // Task<IList<Order>> GetOrderByPickUpDateAsync (DateTime date);
        Task<OrderDto> GetOrderByOrderReferenceReturningOrderDtoObjectAsync (string orderReference);
        Task<Order> GetOrderByIdReturningObjectAsync (int id);
    
    
    }
}