using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers
{
  
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        
        [HttpPost("CreateEmail")]
        public async Task<IActionResult> Register([FromBody] CreateEmailRequestModel model)
        {
            var response = await _emailService.CreateAsync(model);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
        
        [HttpGet("GetEmail/{id}")]
        public async Task<IActionResult> GetEmail([FromRoute] int id)
        {
            var response = await _emailService.GetEmailByIdAsync(id);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
      
         [HttpGet("GetAllEmails")]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _emailService.GetAllAsync();
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
   
        [HttpPut("UpdateEmail{id}")]
        public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailRequestModel model, [FromRoute] int id)
        {
            var response = await _emailService.UpdateAsync(model,id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
     
        [HttpDelete("DeleteEmail{id}")]
        public async Task<IActionResult> DeleteEmail( [FromRoute] int id)
        {
            var response = await _emailService.DeleteAsync(id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
    }
}