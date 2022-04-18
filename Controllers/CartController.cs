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
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ICompanyService _companyService;

        public CartController(ICartService cartService , ICompanyService companyService)
        {
            _cartService = cartService;
            _companyService = companyService;
        }

       
        [HttpPost("CreateCart")]
        public async Task<IActionResult> CreateCart ([FromBody] CreateCartRequestModel model)
        {
            var company  = await _companyService.GetCompanyByEmailReturningCompanyDtoObjectAsync(User.FindFirst(ClaimTypes.Email).Value);
            model.CompanyId = company.Data.Id;
            var response = await _cartService.CreateAsync(model);
            if(response.IsSucess)return Ok(response);
              return BadRequest(response);
        }
        [Authorize]
        [HttpGet("GetCartByCompanyId")]
        public async Task<IActionResult> GetCartByCompanyId ()
        {
            var company  = await _companyService.GetCompanyByEmailReturningCompanyDtoObjectAsync(User.FindFirst(ClaimTypes.Email).Value);
            
            var response = await _cartService.GetCartByIdReturningCartDtoObjectAsync(company.Data.Id);
            if(response.IsSucess)return Ok(response);
            return BadRequest(response);
        }

         [Authorize]
        [HttpPut("UpdateCart")]
        public async Task<IActionResult> UpdateCart ([FromBody] UpdateCartRequestModel model)
        {
            var company  = await _companyService.GetCompanyByEmailReturningCompanyDtoObjectAsync(User.FindFirst(ClaimTypes.Email).Value);
            model.CompanyId = company.Data.Id;
            var response = await _cartService.UpdateAsync(model);
            if(response.IsSucess)return Ok(response);
            return BadRequest(response);
        }

         [Authorize]
        [HttpPut ("UpdateCartItem")]
        public async Task<IActionResult> UpdateCartItem ([FromBody] UpdateItemCartRequestModel model)
        {
            var company  = await _companyService.GetCompanyByEmailReturningCompanyDtoObjectAsync(User.FindFirst(ClaimTypes.Email).Value);
            model.CompanyId = company.Data.Id;
            var response = await _cartService.UpdateCartItemQuantityAsync(model);
            if(response.IsSucess)return Ok(response);
            return BadRequest(response);
        }

    }
}