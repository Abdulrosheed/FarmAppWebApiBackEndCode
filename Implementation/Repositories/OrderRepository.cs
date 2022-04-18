using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstProject.Context;
using FirstProject.Dtos;
using FirstProject.Entities;
using FirstProject.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;


namespace FirstProject.Implementation.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _context;

        public OrderRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<OrderDto> CreateAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
           await _context.SaveChangesAsync();
           return new OrderDto
           {
               Id = order.Id,
               TotalPrice = order.TotalPrice,
               OrderReference = order.OrderReference

           };
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrderByCompanyEmailAsync(string email)
        {
            var orders = await _context.Orders.Include(a => a.Company).Include(a => a.OrderProducts).ThenInclude(a => a.FarmProduct).Where(a => a.Company.Email == email && a.IsDeleted == false).ToListAsync();
            if(orders == null)
            {
                return null;
            }
            return orders.Select(a => new OrderDto
           {
               
                Id = a.Id,
               TotalPrice = a.TotalPrice,
               DeliveryDate = (DateTime)a.DeliveryDate,
               OrderReference = a.OrderReference,
               CompanyEmail = a.Company.Email,
               CompanyName = a.Company.Name,
            
                OrderProducts = a.OrderProducts.Select( a => new FarmProductDto
                {
                    FarmProduct = a.FarmProduct.FarmProduce,
                    Quantity = a.Quantity,
                    Price = a.UnitPrice
                }).ToList()
               
            }).AsEnumerable();
        }
        

        public async Task<IEnumerable<OrderDto>> GetAllOrderByDateAsync(int date)
        {
            var orders = await _context.Orders.Include(a => a.Company).Include(a => a.OrderProducts).ThenInclude(a => a.FarmProduct).Where(a => a.Created.Day == date && a.IsDeleted == false).ToListAsync();
            if(orders == null)
            {
                return null;
            }
            return orders.Select(a => new OrderDto
           {
               
                Id = a.Id,
               TotalPrice = a.TotalPrice,
               DeliveryDate = (DateTime)a.DeliveryDate,
            //    DeliveryCountry = a.DeliveryCountry,
            //    DeliveryLocalGovernment = a.DeliveryLocalGovernment,
            //    DeliveryState = a.DeliveryState,
               OrderReference = a.OrderReference,
               CompanyEmail = a.Company.Email,
               CompanyName = a.Company.Name,
            
                OrderProducts = a.OrderProducts.Select( a => new FarmProductDto
                {
                    FarmProduct = a.FarmProduct.FarmProduce,
                    Quantity = a.Quantity,
                    Price = a.UnitPrice
                }).ToList()

           }).AsEnumerable();
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrderByDateByCompanyAsync(string email, DateTime date)
        {
            var orders = await _context.Orders.Include(a => a.Company).Include(a => a.OrderProducts).ThenInclude(a => a.FarmProduct).Where(a => a.Created == date && a.Company.Email == email && a.IsDeleted == false).ToListAsync();
            if(orders == null)
            {
                return null;
            }
            return orders.Select(a => new OrderDto
           {
               
                Id = a.Id,
               TotalPrice = a.TotalPrice,
               DeliveryDate = (DateTime)a.DeliveryDate,
            //    DeliveryCountry = a.DeliveryCountry,
            //    DeliveryLocalGovernment = a.DeliveryLocalGovernment,
            //    DeliveryState = a.DeliveryState,
               OrderReference = a.OrderReference,
               CompanyEmail = a.Company.Email,
               CompanyName = a.Company.Name,
            
                OrderProducts = a.OrderProducts.Select( a => new FarmProductDto
                {
                    FarmProduct = a.FarmProduct.FarmProduce,
                    Quantity = a.Quantity,
                    Price = a.UnitPrice
                }).ToList()

           }).AsEnumerable();
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders.Include(a => a.Company).Include(a => a.OrderProducts).ThenInclude(a => a.FarmProduct).Where(a =>  a.IsDeleted == false).ToListAsync();
            if(orders == null)
            {
                return null;
            }
            return orders.Select(a => new OrderDto
           {
               
                Id = a.Id,
               TotalPrice = a.TotalPrice,
            //    DeliveryDate = (DateTime)a.DeliveryDate,
            //    DeliveryCountry = a.DeliveryCountry,
            //    DeliveryLocalGovernment = a.DeliveryLocalGovernment,
            //    DeliveryState = a.DeliveryState,
               OrderReference = a.OrderReference,
               CompanyEmail = a.Company.Email,
               CompanyName = a.Company.Name,
                Status = a.Status.ToString(),
                OrderProducts = a.OrderProducts.Select( a => new FarmProductDto
                {
                    FarmProduct = a.FarmProduct.FarmProduce,
                    Quantity = a.Quantity,
                    Price = a.UnitPrice
                }).ToList()

           }).AsEnumerable();
        }

        public async Task<IEnumerable<OrderDto>> GetAllPaidOrderAsync()
        {
           var orders = await _context.Orders.Include(a => a.Company).Include(a => a.OrderProducts).ThenInclude(a => a.FarmProduct).Where(a => a.Status == Enums.OrderStatus.Paid && a.IsDeleted == false).ToListAsync();
            if(orders == null)
            {
                return null;
            }
            return orders.Select(a => new OrderDto
           {
               
                Id = a.Id,
               TotalPrice = a.TotalPrice,
               DeliveryDate = (DateTime)a.DeliveryDate,
            //    DeliveryCountry = a.DeliveryCountry,
            //    DeliveryLocalGovernment = a.DeliveryLocalGovernment,
            //    DeliveryState = a.DeliveryState,
               OrderReference = a.OrderReference,
               CompanyEmail = a.Company.Email,
               CompanyName = a.Company.Name,
            
                OrderProducts = a.OrderProducts.Select( a => new FarmProductDto
                {
                    FarmProduct = a.FarmProduct.FarmProduce,
                    Quantity = a.Quantity,
                    Price = a.UnitPrice
                }).ToList()

           }).AsEnumerable();
        }

        public async Task<IEnumerable<OrderDto>> GetAllUnPaidOrderAsync()
        {
            var orders = await _context.Orders.Include(a => a.Company).Include(a => a.OrderProducts).ThenInclude(a => a.FarmProduct).Where(a => a.Status == Enums.OrderStatus.Initialized && a.IsDeleted == false).ToListAsync();
            if(orders == null)
            {
                return null;
            }
            return orders.Select(a => new OrderDto
           {
               
                Id = a.Id,
               TotalPrice = a.TotalPrice,

               OrderReference = a.OrderReference,
               CompanyEmail = a.Company.Email,
               CompanyName = a.Company.Name,
                Status = a.Status.ToString(),
                OrderProducts = a.OrderProducts.Select( a => new FarmProductDto
                {
                    FarmProduct = a.FarmProduct.FarmProduce,
                    Quantity = a.Quantity,
                    Price = a.UnitPrice
                }).ToList()

           }).AsEnumerable();
        }

        public async Task<IEnumerable<OrderDto>> GetInitializedOrderByCompanyAsync(string email)
        {
            var orders = await _context.Orders.Include(a => a.Company).Include(a => a.OrderProducts).ThenInclude(a => a.FarmProduct).Where(a => a.Status == Enums.OrderStatus.Initialized && a.IsDeleted == false && a.Company.Email == email).ToListAsync();
            if(orders == null)
            {
                return null;
            }
            return orders.Select(a => new OrderDto
           {
               
                Id = a.Id,
               TotalPrice = a.TotalPrice,
              
            //    DeliveryCountry = a.DeliveryCountry,
            //    DeliveryLocalGovernment = a.DeliveryLocalGovernment,
            //    DeliveryState = a.DeliveryState,
               OrderReference = a.OrderReference,
               CompanyEmail = a.Company.Email,
               CompanyName = a.Company.Name,
            
                OrderProducts = a.OrderProducts.Select( a => new FarmProductDto
                {
                    FarmProduct = a.FarmProduct.FarmProduce,
                    Quantity = a.Quantity,
                    Price = a.UnitPrice
                }).ToList()

           }).AsEnumerable();
        }

        public async Task<Order> GetOrderByIdReturningObjectAsync(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
        }

        public async Task<OrderDto> GetOrderByOrderReferenceReturningOrderDtoObjectAsync(string orderReference)
        {
            var orders = await _context.Orders.Include(a => a.Company).Include(a => a.OrderProducts).ThenInclude(a => a.FarmProduct).FirstOrDefaultAsync(a => a.OrderReference == orderReference && a.IsDeleted == false);
            if(orders == null)
            {
                return null;
            }
            return new OrderDto
            {
                 Id = orders.Id,
               TotalPrice = orders.TotalPrice,
               OrderReference = orders.OrderReference,
               CompanyEmail = orders.Company.Email,
               CompanyName = orders.Company.Name,
                Status = orders.Status.ToString(),
            
                OrderProducts = orders.OrderProducts.Select( a => new FarmProductDto
                {
                    FarmProduct = a.FarmProduct.FarmProduce,
                    Quantity = a.Quantity,
                    Price = a.UnitPrice
                }).ToList()
            };
        }

        public async Task<IEnumerable<OrderDto>> GetPaidOrderByCompanyAsync(string email)
        {
           var orders = await _context.Orders.Include(a => a.Company).Include(a => a.OrderProducts).ThenInclude(a => a.FarmProduct).Where(a => a.Status == Enums.OrderStatus.Paid && a.IsDeleted == false && a.Company.Email == email).ToListAsync();
            if(orders == null)
            {
                return null;
            }
            return orders.Select(a => new OrderDto
           {
               
                Id = a.Id,
               TotalPrice = a.TotalPrice,
           
            //    DeliveryCountry = a.DeliveryCountry,
            //    DeliveryLocalGovernment = a.DeliveryLocalGovernment,
            //    DeliveryState = a.DeliveryState,
               OrderReference = a.OrderReference,
               CompanyEmail = a.Company.Email,
               CompanyName = a.Company.Name,
            
                OrderProducts = a.OrderProducts.Select( a => new FarmProductDto
                {
                    FarmProduct = a.FarmProduct.FarmProduce,
                    Quantity = a.Quantity,
                    Price = a.UnitPrice
                }).ToList()

           }).AsEnumerable();
        }

        // public async Task<IList<Order>> GetOrderByPickUpDateAsync(DateTime date)
        // {
        //     var day = date.Day;
        //     var orders = await _context.Orders.Include(a => a.OrderProducts).ThenInclude(a => a.FarmProduct).ThenInclude(a => a.FarmReport).ThenInclude(a => a.FarmInspector).Include(a => a.OrderProducts).ThenInclude(a => a.FarmProduct)
        //     .ThenInclude(a => a.Farmer).Where(a => a.DeliveryDate.HasValue && a.DeliveryDate.Value.Day == date.Day ).ToListAsync();
        //     if(orders == null)
        //     {
        //         return null;
        //     }
        //     return orders;
        // }



        public async Task<OrderDto> UpdateAsync(Order order)
        {
             _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return new OrderDto
            {
                Id = order.Id,
               TotalPrice = order.TotalPrice,
               DeliveryDate = (DateTime)order.DeliveryDate,
               OrderReference = order.OrderReference,
               CompanyEmail = order.Company.Email,
               CompanyName = order.Company.Name,
            
            };
        }
    }
}