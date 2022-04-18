using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FirstProject.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers
{
     [ApiController]
    [Route("api/[controller]")]
    public class FarmProductController : ControllerBase
    {
        private readonly IFarmProductService _farmProductService;

        public FarmProductController(IFarmProductService farmProductService)
        {
            _farmProductService = farmProductService;
        }
       
        [HttpGet ("GetFarmProduct/{id}")]
        public async Task<IActionResult> GetFarmProduct([FromRoute] int id)
        {
            var response = await _farmProductService.GetFarmProductByIdAsync(id);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
        [Authorize]
        [HttpGet ("GetFarmProductByFarmerEmail")]
        public async Task<IActionResult> GetFarmProductByFarmerEmail()
        {
            var farmerEmail = User.FindFirst(ClaimTypes.Email).Value;
            var response = await _farmProductService.GetFarmProductByFarmerEmailAsync(farmerEmail);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }

          [Authorize]
        [HttpGet ("GetToBeUpdatedFarmProductByFarmerEmail")]
        public async Task<IActionResult> GetToBeUpdatedFarmProductByFarmerEmail()
        {
            var farmerEmail = User.FindFirst(ClaimTypes.Email).Value;
            var response = await _farmProductService.GetToBeUpdatedFarmProductByFarmerEmailAsync(farmerEmail);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
           [Authorize]
        [HttpGet("GetAllFarmProducts")]
        public async Task<IActionResult> GetAllFarmProducts()
        {
            var response = await _farmProductService.GetAllFarmProductsAsync();
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
        [Authorize]
        [HttpGet("GetAllInspectedFarmProductByFarmInspector")]
        public async Task<IActionResult> GetAllInspectedFarmProductByFarmInspector()
        {
            var email =   User.FindFirst(ClaimTypes.Email).Value;
            var response = await _farmProductService.GetAllFarmProductsInspectedByFarmInspectorAsync(email);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
         [Authorize]
         [HttpGet("GetToBeUpdatedFarmProductByFarmInspector")]
        public async Task<IActionResult> GetToBeUpdatedFarmProductByFarmInspector()
        {
            var email =   User.FindFirst(ClaimTypes.Email).Value;
            var response = await _farmProductService.GetToBeUpdatedFarmProductByFarmerInspectorEmailAsync(email);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
         [HttpGet("GetAllFarmProductsForHomePage")]
        public async Task<IActionResult> GetAllFarmProductsForHomePage()
        {
            var response = await _farmProductService.GetAllFarmProductsAsync();
            if(response.IsSucess) return Ok(response.Data.Take(10));
            
            return BadRequest(response);
           
        }
        
        [HttpPost("GetFarmProductsByStateAndFarmProduceAndQuantity/{state}")]
        public async Task<IActionResult> GetFarmProductsByStateAndFarmProduceAndQuantity([FromRoute]string state)
        {
            var response = await _farmProductService.GetFarmProductsByStateAndQuantityAndFarmProduceAsync(state);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
        [HttpPost("GetFarmProductsByLocalGovernmentAndFarmProduceAndQuantity/{localGoverment}")]
        public async Task<IActionResult> GetFarmProductsByLocalGovernmentAndFarmProduceAndQuantity([FromRoute] string localGoverment)
        {
            var response = await _farmProductService.GetFarmProductsByLocalGovernmentAndQuantityAndFarmProduceAsync(localGoverment);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
   
        [HttpPut("UpdateFarmProduct/{id}")]
        public async Task<IActionResult> UpdateFarmProduct([FromBody] UpdateFarmProductForAdminRequestModel model, [FromRoute] int id)
        {
            
            var response = await _farmProductService.UpdateAsync(model,id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
       
        [HttpPut("UpdateFarmProductForFarmer/{id}")]
        public async Task<IActionResult> UpdateFarmProductForFarmer([FromBody] UpdateFarmProductForFarmerRequestModel model, [FromRoute] int id)
        {
            var response = await _farmProductService.UpdateAsync(model,id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
         
        [HttpDelete("DeleteFarmProduct/{id}")]
        public async Task<IActionResult> DeleteFarmProduct( [FromRoute] int id)
        {
            var response = await _farmProductService.DeleteAsync(id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
        [Authorize]
        [HttpGet("SearchRequest/{id}")]
         public async Task<IActionResult> SearchRequest([FromRoute]int id)
        {
            var response = await _farmProductService.SearchRequest(id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
        [Authorize]
        [HttpGet("GetSoldProductForFarmer")]
        public async Task<IActionResult> GetSoldProductForFarmer()
        {
            var email =   User.FindFirst(ClaimTypes.Email).Value;
            var response = await _farmProductService.GetSoldFarmProductByFarmerEmailAsync(email);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
         [Authorize]
        [HttpGet("GetNotSoldProductForFarmer")]
        public async Task<IActionResult> GetNotSoldProductForFarmer()
        {
            var email =   User.FindFirst(ClaimTypes.Email).Value;
            var response = await _farmProductService.GetNotSoldFarmProductByFarmerEmailAsync(email);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
    }
}