using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;
using FirstProject.Interfaces.Repositories;
using FirstProject.Interfaces.Services;
using FirstProject.MailBox;

namespace FirstProject.Implementation.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IFarmProductRepository _farmProductRepository;
        private readonly IMailMessage _mailMessage;
        private readonly IRequestRepository _requestRepository;
        private readonly ICompanyRepository _companyRepository;

        public OrderService(IOrderRepository orderRepository, IFarmProductRepository farmProductRepository, IMailMessage mailMessage, IRequestRepository requestRepository, ICompanyRepository companyRepository)
        {
            _orderRepository = orderRepository;
            _farmProductRepository = farmProductRepository;
            _mailMessage = mailMessage;
            _requestRepository = requestRepository;
            _companyRepository = companyRepository;
        }

        public async Task<BaseResponse<IEnumerable<OrderDto>>> GetAllOrderByCompanyEmailAsync(string email)
        {
            var orders = await _orderRepository.GetAllOrderByCompanyEmailAsync(email);
            if (orders == null)
            {
                return new BaseResponse<IEnumerable<OrderDto>>
                {
                    Message = "No order found",
                    IsSucess = true,
                };
            }
            return new BaseResponse<IEnumerable<OrderDto>>
            {
                Message = "Order successfully retrieved",
                IsSucess = true,
                Data = await _orderRepository.GetAllOrderByCompanyEmailAsync(email)
            };
        }

        public async Task<BaseResponse<IEnumerable<OrderDto>>> GetAllOrderByDateAsync(int date)
        {
            var orders = await _orderRepository.GetAllOrderByDateAsync(date);
            if (orders == null)
            {
                return new BaseResponse<IEnumerable<OrderDto>>
                {
                    Message = "No order found",
                    IsSucess = true,
                };
            }
            return new BaseResponse<IEnumerable<OrderDto>>
            {
                Message = "Order successfully retrieved",
                IsSucess = true,
                Data = await _orderRepository.GetAllOrderByDateAsync(date)
            };
        }

        public async Task<BaseResponse<IEnumerable<OrderDto>>> GetAllOrderByDateByCompanyAsync(string email, DateTime date)
        {
            var orders = await _orderRepository.GetAllOrderByDateByCompanyAsync(email, date);
            if (orders == null)
            {
                return new BaseResponse<IEnumerable<OrderDto>>
                {
                    Message = "No order found",
                    IsSucess = true,
                };
            }
            return new BaseResponse<IEnumerable<OrderDto>>
            {
                Message = "Order successfully retrieved",
                IsSucess = true,
                Data = await _orderRepository.GetAllOrderByDateByCompanyAsync(email, date)
            };
        }

        public async Task<BaseResponse<IEnumerable<OrderDto>>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            if (orders == null)
            {
                return new BaseResponse<IEnumerable<OrderDto>>
                {
                    Message = "No order found",
                    IsSucess = true,
                };
            }
            return new BaseResponse<IEnumerable<OrderDto>>
            {
                Message = "Order successfully retrieved",
                IsSucess = true,
                Data = await _orderRepository.GetAllOrdersAsync()
            };
        }

        public async Task<BaseResponse<IEnumerable<OrderDto>>> GetAllPaidOrderAsync()
        {
            var orders = await _orderRepository.GetAllPaidOrderAsync();
            if (orders == null)
            {
                return new BaseResponse<IEnumerable<OrderDto>>
                {
                    Message = "No order found",
                    IsSucess = true,
                };
            }
            return new BaseResponse<IEnumerable<OrderDto>>
            {
                Message = "Order successfully retrieved",
                IsSucess = true,
                Data = await _orderRepository.GetAllPaidOrderAsync()
            };
        }

        public async Task<BaseResponse<IEnumerable<OrderDto>>> GetAllUnPaidOrderAsync()
        {
            var orders = await _orderRepository.GetAllUnPaidOrderAsync();
            if (orders == null)
            {
                return new BaseResponse<IEnumerable<OrderDto>>
                {
                    Message = "No order found",
                    IsSucess = true,
                };
            }
            return new BaseResponse<IEnumerable<OrderDto>>
            {
                Message = "Order successfully retrieved",
                IsSucess = true,
                Data = orders
            };
        }

        public async Task<BaseResponse<OrderDto>> GetOrderByOrderReferenceReturningOrderDtoObjectAsync(string orderReference)
        {
            var orders = await _orderRepository.GetOrderByOrderReferenceReturningOrderDtoObjectAsync(orderReference);
            if (orders == null)
            {
                return new BaseResponse<OrderDto>
                {
                    Message = "No order found",
                    IsSucess = true,
                };
            }
            return new BaseResponse<OrderDto>
            {
                Message = "Order successfully retrieved",
                IsSucess = true,
                Data = orders
            };
        }

        public async Task<BaseResponse<OrderDto>> UpdateAsync(UpdateOrderRequestModel model)
        {
            var order = await _orderRepository.GetOrderByIdReturningObjectAsync(model.Id);
            if (order == null)
            {
                return new BaseResponse<OrderDto>
                {
                    IsSucess = false,
                    Message = "Order  Not Found"
                };
            }

            order.Status = Enums.OrderStatus.Paid;
            var orderInfo = await _orderRepository.UpdateAsync(order);
            return new BaseResponse<OrderDto>
            {
                IsSucess = true,
                Message = "Order  Updated Successfully",
                Data = orderInfo
            };
        }

        public async Task<BaseResponse<IEnumerable<OrderDto>>> GetAllInitializedOrderByCompanyAsync(string email)
        {
            var orders = await _orderRepository.GetInitializedOrderByCompanyAsync(email);
            if (orders == null)
            {
                return new BaseResponse<IEnumerable<OrderDto>>
                {
                    Message = "No order found",
                    IsSucess = true,
                };
            }
            return new BaseResponse<IEnumerable<OrderDto>>
            {
                Message = "Order successfully retrieved",
                IsSucess = true,
                Data = await _orderRepository.GetInitializedOrderByCompanyAsync(email)
            };
        }

        public async Task<BaseResponse<IEnumerable<OrderDto>>> GetAllPaidOrderByCompanyAsync(string email)
        {
            var orders = await _orderRepository.GetPaidOrderByCompanyAsync(email);
            if (orders == null)
            {
                return new BaseResponse<IEnumerable<OrderDto>>
                {
                    Message = "No order found",
                    IsSucess = true,
                };
            }
            return new BaseResponse<IEnumerable<OrderDto>>
            {
                Message = "Order successfully retrieved",
                IsSucess = true,
                Data = await _orderRepository.GetPaidOrderByCompanyAsync(email)
            };
        }

        public async Task<BaseResponse<OrderDto>> CreateAsync(CreateOrderRequestModel model)
        {
            var needeQuantity = 0;
            var neededProducts = new List<FarmProduct>();
            var dict = new Dictionary<FarmProduct, int>();
            var request = await _requestRepository.GetByIdAsync(model.RequestId);
            var company = await _companyRepository.GetCompanyReturningCompanyObjectAsync(request.CompanyId);
            foreach (var item in model.FarmProductId)
            {
                var farmProduct = await _farmProductRepository.GetFarmProductReturningFarmProductObjectAsync(item);

                if (farmProduct.Quantity > request.Quantity)
                {
                    farmProduct.Quantity = farmProduct.Quantity - request.Quantity;
                    await _farmProductRepository.UpdateAsync(farmProduct);
                    var order = new Order
                    {
                        Company = company,
                        CompanyId = company.Id,
                        Status = Enums.OrderStatus.Initialized,
                        OrderReference = Guid.NewGuid().ToString().Trim().Replace("-", "").Substring(0, 10),

                    };

                    var orderProduct = new OrderFarmProduct
                    {
                        Order = order,
                        OrderId = order.Id,
                        FarmProduct = farmProduct,
                        FarmProductId = farmProduct.Id,
                        UnitPrice = (decimal)farmProduct.Price * request.Quantity,
                        Quantity = request.Quantity
                    };
                    order.OrderProducts.Add(orderProduct);
                    order.TotalPrice = (decimal)farmProduct.Price * request.Quantity;
                    request.Status = RequestStatus.Fulfilled;
                    var orderInfo = await _orderRepository.CreateAsync(order);
                    return new BaseResponse<OrderDto>
                    {
                        Message = "Order sucessfully created",
                        Data = orderInfo,
                        IsSucess = true
                    };

                }
                else if (farmProduct.Quantity == request.Quantity)
                {
                    farmProduct.FarmProductStatus = FarmProductStatus.Booked;
                    await _farmProductRepository.UpdateAsync(farmProduct);
                    var order = new Order
                    {
                        Company = company,
                        CompanyId = company.Id,
                        Status = Enums.OrderStatus.Initialized,
                        OrderReference = Guid.NewGuid().ToString().Trim().Replace("-", "").Substring(0, 10),

                    };

                    var orderProduct = new OrderFarmProduct
                    {
                        Order = order,
                        OrderId = order.Id,
                        FarmProduct = farmProduct,
                        FarmProductId = farmProduct.Id,
                        UnitPrice = (decimal)farmProduct.Price * request.Quantity,
                        Quantity = request.Quantity
                    };
                    order.OrderProducts.Add(orderProduct);
                    order.TotalPrice = (decimal)farmProduct.Price * request.Quantity;
                    request.Status = RequestStatus.Fulfilled;
                    var orderInfo = await _orderRepository.CreateAsync(order);
                    return new BaseResponse<OrderDto>
                    {
                        Message = "Order sucessfully created",
                        Data = orderInfo,
                        IsSucess = true
                    };
                }
                else if (farmProduct.Quantity < request.Quantity)
                {
                    needeQuantity += farmProduct.Quantity;
                    if (needeQuantity <= request.Quantity)
                    {
                        neededProducts.Add(farmProduct);
                    }
                    if (needeQuantity > request.Quantity)
                    {
                        var quantityLeftOver = needeQuantity - request.Quantity;
                        farmProduct.Quantity = quantityLeftOver;
                        dict.Add(farmProduct, quantityLeftOver);
                        await _farmProductRepository.UpdateAsync(farmProduct);

                    }
                }

            }
            if (neededProducts.Count != 0 && dict.Count != 0)
            {
                var order = new Order
                {
                    Company = company,
                    CompanyId = company.Id,
                    Status = Enums.OrderStatus.Initialized,
                    OrderReference = Guid.NewGuid().ToString().Trim().Replace("-", "").Substring(0, 10),

                };
                foreach (var item in neededProducts)
                {
                    item.FarmProductStatus = FarmProductStatus.Booked;
                    await _farmProductRepository.UpdateAsync(item);


                    var orderProduct = new OrderFarmProduct
                    {
                        Order = order,
                        OrderId = order.Id,
                        FarmProduct = item,
                        FarmProductId = item.Id,
                        UnitPrice = (decimal)item.Price * item.Quantity,
                        Quantity = item.Quantity
                    };
                    order.OrderProducts.Add(orderProduct);
                    order.TotalPrice += (decimal)item.Price * item.Quantity;
                    

                }
                foreach (var item in dict)
                {
                     item.Key.FarmProductStatus = FarmProductStatus.Booked;
                    await _farmProductRepository.UpdateAsync(item.Key);


                    var orderProduct = new OrderFarmProduct
                    {
                        Order = order,
                        OrderId = order.Id,
                        FarmProduct = item.Key,
                        FarmProductId = item.Key.Id,
                        UnitPrice = (decimal)item.Key.Price * item.Value,
                        Quantity = item.Value
                    };
                    order.OrderProducts.Add(orderProduct);
                    order.TotalPrice += (decimal)item.Key.Price * item.Value;
                }
                request.Status = RequestStatus.Fulfilled;
                var orderInfo = await _orderRepository.CreateAsync(order);
                return new BaseResponse<OrderDto>
                {
                    Message = "Order sucessfully created",
                    Data = orderInfo,
                    IsSucess = true
                };
            }
            if (dict.Count != 0)
            {

            }
            return new BaseResponse<OrderDto>
            {
                Message = "Order not sucessfully created",
                IsSucess = false
            };

        }
    }
}