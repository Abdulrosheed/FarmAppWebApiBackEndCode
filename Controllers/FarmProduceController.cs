using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers
{
     [ApiController]
    [Route("api/[controller]")]

    public class FarmProduceController : ControllerBase
    {
        private readonly IFarmProduceService _farmProduceService;

        public FarmProduceController(IFarmProduceService farmProduceService)
        {
            _farmProduceService = farmProduceService;
        }
        
        [HttpPost("RegisterFarmProduce")]
        public async Task<IActionResult> Register([FromBody] CreateFarmProduceRequestModel model)
        {
            var response = await _farmProduceService.RegisterAsync(model);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
     
        [HttpGet ("GetFarmProduce/{id}")]
        public async Task<IActionResult> GetFarmProduce([FromRoute] int id)
        {
            var response = await _farmProduceService.GetFarmProduceAsync(id);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
       
         [HttpGet("GetAllFarmProduce")]
        public async Task<IActionResult> GetAllFarmProduce()
        {
            var response = await _farmProduceService.GetAllFarmProduceAsync();
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
   
        [HttpPut("UpdateFarmProduce/{id}")]
        public async Task<IActionResult> UpdateFarmProduce([FromBody] UpdateFarmProduceRequestModel model, [FromRoute] int id)
        {
            var response = await _farmProduceService.UpdateAsync(model,id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
       
        [HttpDelete("DeleteFarmProduce/{id}")]
        public async Task<IActionResult> DeleteFarmProduce( [FromRoute] int id)
        {
            var response = await _farmProduceService.DeleteAsync(id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
    }
}