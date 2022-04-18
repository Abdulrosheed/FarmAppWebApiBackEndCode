using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers
{

     [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("RegisterRole")]
        public async Task<IActionResult> Register([FromBody] CreateRoleRequestModel model)
        {
            var response = await _roleService.RegisterAsync(model);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
     
        [HttpGet ("GetRole/{id}")]
        public async Task<IActionResult> GetRole([FromRoute] int id)
        {
            var response = await _roleService.GetRoleAsync(id);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
       
         [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var response = await _roleService.GetAllRolesAsync();
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
      
        [HttpPut("UpdateRole/{id}")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleRequestModel model, [FromRoute] int id)
        {
            var response = await _roleService.UpdateAsync(model,id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
    
        [HttpDelete("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole( [FromRoute] int id)
        {
            var response = await _roleService.DeleteAsync(id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
         
    }
}