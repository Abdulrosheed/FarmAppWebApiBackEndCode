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
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IFarmerService _farmerService;
        private readonly IFarmService _farmService;
        private readonly IFarmInspectorService _farmInspectorService;
        private readonly IEmailService _emailService;
        private readonly IMailMessage _mailMessage;
        private readonly IFarmReportService _farmReportService;
     
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(IAdminService adminService , IFarmInspectorService farmInspectorService , IFarmerService farmerService
         , IEmailService emailService , IMailMessage mailMessage , IFarmReportService farmReportService , IFarmService farmService , IWebHostEnvironment webHostEnvironment)
        {
            _adminService = adminService;
            _farmInspectorService = farmInspectorService;
            _farmerService = farmerService;
            _emailService = emailService;
            _mailMessage = mailMessage;
            _farmReportService = farmReportService;
            _farmService = farmService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> Register([FromForm] CreateAdminRequestModel model)
        {
            
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
                    model.Image = productImage;
            
                   

                }
            }
            var response = await _adminService.RegisterAsync(model);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
    
        [HttpGet("GetAdminFromRoute/{id}")]
        public async Task<IActionResult> GetAdminFromRoute([FromRoute] int id)
        {
    
            var response = await _adminService.GetAdminAsync(id);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
          [Authorize]
        [HttpGet("GetAdmin")]
        public async Task<IActionResult> GetAdmin()
        {
             var admin = await _adminService.GetAdminByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);
            
            var response = await _adminService.GetAdminAsync(admin.Data.Id);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }

         [HttpGet("GetAllAdmin")]
         public async Task<IActionResult> GetAllAdmin()
        {
            var response = await _adminService.GetAllAdminAsync();
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
       [Authorize]
        [HttpPut("UpdateAdmin")]
        public async Task<IActionResult> UpdateAdmin([FromBody] UpdateAdminRequestModel model)
        {
            var admin = await _adminService.GetAdminByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);
            var response = await _adminService.UpdateAsync(model,admin.Data.Id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
        
        [HttpDelete("DeleteAdmin/{id}")]
        public async Task<IActionResult> DeleteAdmin([FromRoute] int id)
        {
            
            var response = await _adminService.DeleteAsync(id);
            if(response.IsSucess) return Ok(response);
            
            
            
            return BadRequest(response);
           
        }
    
        [HttpPost("AssignFarmInspectorToFarm")]
        public async Task<IActionResult> AssignFarmInspectorToFarm([FromBody] AssignFarmInspectorToFarmRequestModel model)
        {
        
            var farmInspectorResponse = await _farmInspectorService.GetFarmInspectorByEmailAsync(model.FarmInspectorEmail);
            
            var farmResponse = await _farmService.GetFarmAsync(model.FarmId);
            var farmerResponse = await _farmerService.GetFarmerByEmailAsync(farmResponse.Data.FarmerEmail);
           
        
            var email = await _emailService.GetEmailByEmailTypeAsync(EmailType.RegistrationApproval);
            
           
            
        
            if(farmInspectorResponse.IsSucess && farmerResponse.IsSucess && farmResponse.Data.FarmStatus != FarmStatus.Assigned.ToString()) 
            {
                var farmInspectorLink = $"https://localhost:5001/api/FarmInspector/GetFarmInspectorFromRoute/{farmInspectorResponse.Data.Id}";
            
                var farmerLink = $"https://localhost:5001/api/Farmer/GetFarmerFromRoute/{farmerResponse.Data.Id}";
            
               
                
                _mailMessage.NotifyFarmerAboutFarmInspectorEmail(farmInspectorResponse.Data.Email , farmInspectorLink);
                
                _mailMessage.NotifyFarmInspectorAboutToBeInspectedFarm(farmerResponse.Data.Email , farmerLink );
                
                await _farmerService.UpdateFarmerAsync(farmerResponse.Data.Id);
                await _farmService.UpdateFarmAsync(farmInspectorResponse.Data.Id , farmResponse.Data.Id , model.InspectingDate);
                var response = new BaseResponse<UserDto>
                {
                    IsSucess = true,
                    Message = "Sucessfully assigned farmInspector to farm"
                };
                return Ok(response);
            }
              var response2 = new BaseResponse<UserDto>
                {
                    IsSucess = false,
                    Message = "This farm has been assigned before"
                };

            return BadRequest (response2);
        }
        
        [HttpGet("GetAllFarmReport")]
        public async Task<IActionResult> GetAllFarmReport()
        {
            var response = await _farmReportService.GetAllFarmReportsAsync();
            
            if(response.IsSucess) return Ok(response);
            
            
            return BadRequest(response);
           
        }
      

         [HttpGet("GetProcessingFarmReport")]
        public async Task<IActionResult> GetProcessingFarmReport()
        {
            var response = await _farmReportService.GetAllProcessingFarmReportsAsync();
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
        
    }
}