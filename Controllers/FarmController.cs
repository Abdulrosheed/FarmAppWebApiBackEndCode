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
    public class FarmController : ControllerBase
    {

        private readonly IFarmService _farmService;
        
        private readonly IWebHostEnvironment _webHostEnvironment;


        private readonly IFarmerService _farmerService;
        private readonly IEmailService _emailService;
        private readonly IMailMessage _mailMessage;

        public FarmController(IFarmService farmService, IEmailService emailService, IMailMessage mailMessage, IFarmerService farmerService ,IWebHostEnvironment webHostEnvironment)
        {
            _farmService = farmService;
            _emailService = emailService;
            _mailMessage = mailMessage;
            _farmerService = farmerService;
            _webHostEnvironment = webHostEnvironment;

        }
         [Authorize]

        [HttpPost("RegisterFarm")]
        public async Task<IActionResult> RegisterFarm([FromForm] CreateFarmRequestModel model)
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
                        model.FarmPicture1 = productImage;
                        count++;
                    }
                    else
                    {
                        model.FarmPicture2 = productImage;
                    }

                }
            }
            var userEmail = User.FindFirst(ClaimTypes.Email).Value;
            var farmer = await _farmerService.GetFarmerByEmailAsync(userEmail);
            var response = await _farmService.RegisterAsync(model, userEmail);
            if (response.IsSucess)return Ok(response);
            return BadRequest(response);
        }
        [Authorize]
        [HttpGet("GetAllApprovedFarmByFarmer")]
        public async Task<IActionResult> GetAllApprovedFarmByFarmer()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email).Value;
            var response = await _farmService.GetAllApprovedFarmByFarmerAsync(userEmail);

            if (response.IsSucess) return Ok(response);

            return BadRequest(response);
        }
          [Authorize]
        [HttpGet("GetAllDeclinedFarmByFarmer")]
        public async Task<IActionResult> GetAllDeclinedFarmByFarmer()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email).Value;
            var response = await _farmService.GetAllDeclinedFarmByFarmerAsync(userEmail);

            if (response.IsSucess) return Ok(response);

            return BadRequest(response);
        }
          
        [HttpGet("GetFarm/{id}")]
        public async Task<IActionResult> GetFarm([FromRoute] int id)
        {
            var response = await _farmService.GetFarmAsync(id);

            if (response.IsSucess) return Ok(response);

            return BadRequest(response);
        }
        
        [HttpGet("GetAllFarms")]
        public async Task<IActionResult> GetAllFarms()
        {
            var response = await _farmService.GetAllFarmAsync();
            if (response.IsSucess) return Ok(response);

            return BadRequest(response);

        }
            
        [HttpGet("GetAllDeclinedFarms")]
        public async Task<IActionResult> GetAllDeclinedFarms()
        {
            var response = await _farmService.GetAllDeclinedFarmAsync();
            if (response.IsSucess) return Ok(response);

            return BadRequest(response);

        }
        
        [HttpGet("GetAllProcessingApprovalFarms")]
        public async Task<IActionResult> GetAllProcessingApprovalFarms()
        {
            var response = await _farmService.GetAllProcessingFarmAsync();
            if (response.IsSucess) return Ok(response);

            return BadRequest(response);

        }
     
        [HttpGet("GetAllAssignedFarms")]
        public async Task<IActionResult> GetAllAssignedFarms()
        {
            var response = await _farmService.GetAllAssignedFarmAsync();
            if (response.IsSucess) return Ok(response);

            return BadRequest(response);

        }


        // [HttpPut("UpdateFarm/{id}")]
        // public async Task<IActionResult> UpdateFarm([FromBody] UpdateFarmRequestModel model, [FromRoute] int id)
        // {
        //     var response = await _farmService.UpdateAsync(model, id);
        //     if (response.IsSucess) return Ok(response);

        //     return BadRequest(response);

        // }
          
        [HttpDelete("DeleteFarm/{id}")]
        public async Task<IActionResult> DeleteFarm([FromRoute] int id)
        {
            var response = await _farmService.DeleteAsync(id);
            if (response.IsSucess) return Ok(response);

            return BadRequest(response);

        }
    }
}