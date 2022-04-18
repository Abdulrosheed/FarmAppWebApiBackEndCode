using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
    
        [HttpPost("RegisterCategory")]
        public async Task<IActionResult> Register([FromBody] CreateCategoryRequestModel model)
        {
            var response = await _categoryService.RegisterAsync(model);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
    
        [HttpGet("GetCategory/{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            var response = await _categoryService.GetCategoryAsync(id);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }

         [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _categoryService.GetAllCategoryAsync();
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }

        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryRequestModel model, [FromRoute] int id)
        {
            var response = await _categoryService.UpdateAsync(model,id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
        

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory( [FromRoute] int id)
        {
            var response = await _categoryService.DeleteAsync(id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
    }
}