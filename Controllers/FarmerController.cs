using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Interfaces.Services;
using FirstProject.MailBox;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class FarmerController : ControllerBase
    {
        private readonly IFarmerService _farmerService;
        private readonly IMailMessage _mailMessage;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FarmerController(IFarmerService farmerService , IMailMessage mailMessage , IEmailService emailService , IWebHostEnvironment webHostEnvironment)
        {
            _farmerService = farmerService;
            _mailMessage = mailMessage;
            _emailService = emailService;
            _webHostEnvironment = webHostEnvironment;
        }

         [HttpPost("RegisterFarmer")]
        public async Task<IActionResult> RegisterFarmer([FromForm] CreateFarmerRequestModel model)
        {
            int count = 0;
             var files = HttpContext.Request.Form;
            if (files != null && files.Count > 0)
            {
                string imageDirectory = Path.Combine(_webHostEnvironment.ContentRootPath, "Images");
                Directory.CreateDirectory(imageDirectory);
                foreach (var file in files.Files)
                {
                    FileInfo fi = new FileInfo(file.FileName);
                    string productImage = Guid.NewGuid().ToString() + fi.Extension;
                    string path = Path.Combine(imageDirectory , productImage);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                   
                    if(count == 0)
                    {
                        model.Image = productImage;
                        count++;
                    }
                    if(count == 1)
                    {
                        model.FarmPicture1 = productImage;
                        count++;
                    }
                    else
                    {
                        model.FarmPicture2 = productImage;
                    }
            
                   

                }
            }
            var response = await _farmerService.RegisterAsync(model);
            if(response.IsSucess) return Ok(response);
            return BadRequest(response);
        }
       [Authorize]
        [HttpGet ("GetFarmer")]
        public async Task<IActionResult> GetFarmer()
        {
            var farmer = await _farmerService.GetFarmerByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);
            var response = await _farmerService.GetFarmerByIdAsync(farmer.Data.Id);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
      
           [HttpGet ("GetFarmerFromRoute/{id}")]
        public async Task<IActionResult> GetFarmerFromRoute([FromRoute] int id)
        {
            
            var response = await _farmerService.GetFarmerByIdAsync(id);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
       
         [HttpGet("GetAllFarmers")]
        public async Task<IActionResult> GetAllFarmers()
        {
            var response = await _farmerService.GetAllFarmersAsync();
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
       
         [HttpGet("GetAllProcessingFarmers")]
        public async Task<IActionResult> GetAllProcessingFarmers()
        {
            var response = await _farmerService.AllProcessingFarmerAprovalAsync();
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }

        
         [HttpGet("GetAllDeclinedFarmers")]
        public async Task<IActionResult> GetAllDeclinedFarmers()
        {
            var response = await _farmerService.AllDeclinedFarmerAprovalAsync();
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
        
       
         [HttpGet("GetAllAssignedFarmers")]
        public async Task<IActionResult> GetAllAssignedFarmers()
        {
            var response = await _farmerService.AllAssignedFarmerAprovalAsync();
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
        [Authorize]
        [HttpPut("UpdateFarmer")]
        public async Task<IActionResult> UpdateFarmer([FromBody] UpdateFarmerRequestModel model)
        {
            var farmer = await _farmerService.GetFarmerByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);
            var response = await _farmerService.UpdateAsync(model,farmer.Data.Id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }

        [HttpPut("UpdateFarmerStatusToDecline/{id}")]
        public async Task<IActionResult> UpdateFarmerStatusToDecline([FromRoute] int id)
        {
            
            var response = await _farmerService.ChangeFarmerStatusToDeclinedAsync(id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
        
        [HttpDelete("DeleteFarmer/{id}")]
        public async Task<IActionResult> DeleteFarmer([FromRoute] int id)
        {
           
            var response = await _farmerService.DeleteAsync(id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }

        
    }
}