using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers
{
         [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICompanyService _companyService;

        public OrderController(IOrderService orderservice , ICompanyService companyService)
        {
            _orderService = orderservice;
            _companyService = companyService;
        }
          
        
        [HttpPost ("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestModel model)
        {
            
          
            var response = await _orderService.CreateAsync(model);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
        
        [HttpGet ("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var response = await _orderService.GetAllOrdersAsync();

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }

         
         [HttpGet ("GetAllPaidOrder")]
        public async Task<IActionResult> GetAllPaidOrder()
        {
            var response = await _orderService.GetAllPaidOrderAsync();

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
       
         [HttpGet("GetAllUnpaidOrder")]
        public async Task<IActionResult> GetAllUnpaidOrder()
        {
            var response = await _orderService.GetAllUnPaidOrderAsync();
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
        // [HttpGet("GetOrderByDate/{date}")]
        // public async Task<IActionResult> GetOrderByDate( [FromRoute] DateTime date)
        // {
        //     var response = await _orderService.GetAllOrderByDateAsync(date);
        //     if(response.IsSucess) return Ok(response);
            
        //     return BadRequest(response);
           
        // }
        
        [HttpGet("GetTodaysOrder")]
        public async Task<IActionResult> GetTodaysOrder()
        {
            var response = await _orderService.GetAllOrderByDateAsync(DateTime.UtcNow.Date.Day);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
        [HttpGet("GetOrderByOrderReferenceNumber/{referenceNumber}")]
        public async Task<IActionResult> GetOrderByOrderReferenceNumber( [FromRoute] string referenceNumber)
        {
            var response = await _orderService.GetOrderByOrderReferenceReturningOrderDtoObjectAsync(referenceNumber);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
          [Authorize]
        [HttpGet("GetOrderByDateByCompany/{date}")]
         public async Task<IActionResult> GetOrderByDateByCompany( [FromRoute] DateTime date)
        {
            
            var response = await _orderService.GetAllOrderByDateByCompanyAsync(User.FindFirst(ClaimTypes.Email).Value , date);
            if(response.IsSucess) return Ok(response);
        
            return BadRequest(response);
           
        }
          [Authorize]
        [HttpGet("GetPaidOrderByCompany")]
         public async Task<IActionResult> GetPaidOrderByCompany()
        {
            
            var response = await _orderService.GetAllPaidOrderByCompanyAsync(User.FindFirst(ClaimTypes.Email).Value);
            if(response.IsSucess) return Ok(response);
        
            return BadRequest(response);
           
        }

        [Authorize]
        [HttpGet("GetInitializedOrderByCompany")]
         public async Task<IActionResult> GetInitializedOrderByCompany()
        {
            
            var response = await _orderService.GetAllInitializedOrderByCompanyAsync(User.FindFirst(ClaimTypes.Email).Value);
            if(response.IsSucess) return Ok(response);
        
            return BadRequest(response);
           
        }
           
        [HttpPut("UpdateOrder")]
         public async Task<IActionResult> UpdateOrder( [FromBody] UpdateOrderRequestModel model)
        {
            var response = await _orderService.UpdateAsync(model);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
    }
}