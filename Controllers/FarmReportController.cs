using System.Security.Claims;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject
{
     [ApiController]
    [Route("api/[controller]")]
    public class FarmReportController : ControllerBase
    {
        private readonly IFarmReportService _farmReportService;

        public FarmReportController(IFarmReportService farmReportService)
        {
            _farmReportService = farmReportService;
        }

        
        [HttpGet ("GetFarmReport/{id}")]
        public async Task<IActionResult> GetFarmReport([FromRoute] int id)
        {
            var response = await _farmReportService.GetFarmReportAsync(id);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
        
        [HttpGet ("GetFarmReportByFarmId/{farmerId}")]
        public async Task<IActionResult> GetFarmReportByFarmId([FromRoute] int farmerId)
        {
            var response = await _farmReportService.GetFarmReportByFarmIdAsync(farmerId);

            if(response.IsSucess) return Ok(response);
            
            
            return BadRequest(response);
        }
 
         [HttpGet ("GetFarmReportByFarmInspectorEmail/{farmInspectorEmail}")]
        public async Task<IActionResult> GetFarmReportByFarmInspectorEmail([FromRoute] string farmInspectorEmail)
        {
            var response = await _farmReportService.GetFarmReportByFarmInspectorEmailAsync(farmInspectorEmail);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        } 
        
         [HttpGet("GetAllFarmReport")]
        public async Task<IActionResult> GetAllFarmReport()
        {
            var response = await _farmReportService.GetAllFarmReportsAsync();
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
        
         [HttpGet("GetAllApprovedFarmReport")]
        public async Task<IActionResult> GetAllApprovedFarmReport()
        {
            var response = await _farmReportService.GetAllApprovedFarmReportsAsync();
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
       
        [HttpGet("GetAllDeclinedFarmReport")]
        public async Task<IActionResult> GetAllDeclinedFarmReport()
        {
            var response = await _farmReportService.GetAllDeclinedFarmReportsAsync();
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
        
        [HttpGet("GetAllUpdatedFarmReport")]
        public async Task<IActionResult> GetAllUpdatedFarmReport()
        {
            var response = await _farmReportService.GetAllUpdatedFarmReportsAsync();
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
        
        
         [Authorize]
           [HttpGet("GetAllApprovedFarmReportByFarmInspectorEmail")]
        public async Task<IActionResult> GetAllApprovedFarmReportByFarmInspectorEmail()
        {
            var response = await _farmReportService.GetAllApprovedFarmReportsByFarmInspectorEmailAsync(User.FindFirst(ClaimTypes.Email).Value);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
     
        [HttpPut("UpdateFarmReport/{id}")]
        public async Task<IActionResult> UpdateFarmReport( [FromRoute] int id)
        {
            var response = await _farmReportService.ApproveUpdatedFarmReport(id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
        
      
        [HttpDelete("DeleteFarmReport/{id}")]
        public async Task<IActionResult> DeleteFarmReport( [FromRoute] int id)
        {
            var response = await _farmReportService.DeleteAsync(id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
   
        [HttpGet("ApproveFarmReport/{id}")]
         public async Task<IActionResult> ApproveFarmReport( [FromRoute] int id)
        {
            var response = await _farmReportService.ApproveFarmReport(id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
   
        [HttpGet("DeclineFarmReport/{id}")]
         public async Task<IActionResult> DeclineFarmReport( [FromRoute] int id)
        {
            var response = await _farmReportService.DeclineFarmReport(id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }

    }
}