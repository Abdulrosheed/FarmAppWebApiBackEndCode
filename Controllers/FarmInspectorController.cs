using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Entities;
using FirstProject.Interfaces.Services;
using FirstProject.MailBox;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers
{
       [ApiController]
    [Route("api/[controller]")]

    public class FarmInspectorController : ControllerBase
    {
        private readonly IFarmInspectorService _farmInspectorService;
        private readonly IFarmerService _farmerService;
        private readonly IFarmService _farmService;
        private readonly IFarmReportService _farmReportService;
         private readonly IEmailService _emailService;
        private readonly IMailMessage _mailMessage;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FarmInspectorController(IFarmInspectorService farmInspectorService , IFarmerService farmerService,
          IFarmReportService farmReportService , IMailMessage mailMessage , IEmailService emailService , IFarmService farmService , IWebHostEnvironment webHostEnvironment )
        {
            _farmInspectorService = farmInspectorService;
            _farmerService = farmerService;
            _farmReportService = farmReportService;
            _emailService = emailService;
            _mailMessage = mailMessage;
            _farmService = farmService;
            _webHostEnvironment = webHostEnvironment;
        }
      
        [HttpPost("RegisterFarmInspector")]
        public async Task<IActionResult> Register([FromForm] CreateFarmInspectorRequestModel model)
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
            var response = await _farmInspectorService.RegisterAsync(model);

            if(response.IsSucess) 
            {
                
                return Ok(response);
            }
      
            
            return BadRequest(response);
        }
        [Authorize]
        [HttpGet ("GetFarmInspector")]
        public async Task<IActionResult> GetFarmInspector()
        {
            var farmInspector = await _farmInspectorService.GetFarmInspectorByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);
            
            var response = await _farmInspectorService.GetFarmInspectorAsync(farmInspector.Data.Id);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
        
        [HttpGet ("GetFarmInspectorFromRoute/{id}")]
        public async Task<IActionResult> GetFarmInspectorFromRoute([FromRoute] int id)
        {
            var farmInspector = await _farmInspectorService.GetFarmInspectorAsync(id);
            
            var response = await _farmInspectorService.GetFarmInspectorAsync(farmInspector.Data.Id);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
     
         [HttpGet("GetAllFarmInspectors")]
        public async Task<IActionResult> GetAllFarmInspector()
        {
            var response = await _farmInspectorService.GetAllFarmInspectorAsync();
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
        [Authorize]
        [HttpPut("UpdateFarmInspector")]
        public async Task<IActionResult> UpdateFarmInspector([FromBody] UpdateFarmInspectorRequestModel model)
        {
            var farmInspector = await _farmInspectorService.GetFarmInspectorByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);
            var response = await _farmInspectorService.UpdateAsync(model,farmInspector.Data.Id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
            
        [HttpDelete("DeleteFarmInspector/{id}")]
        public async Task<IActionResult> DeleteFarmInspector([FromRoute] int id)
        {
           
            var response = await _farmInspectorService.DeleteAsync(id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
     
         [HttpGet("GetFarmInspectorByCountry{country}")]
        public async Task<IActionResult> GetFarmInspectorByCountry([FromRoute] string email)
        {
            var response = await _farmInspectorService.GetFarmInspectorByCountryAsync(email);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
           
         [HttpGet("GetFarmInspectorByState{state}")]
        public async Task<IActionResult> GetFarmInspectorByState([FromRoute] string state)
        {
            var response = await _farmInspectorService.GetFarmInspectorByStateAsync(state);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
           
         [HttpGet("GetFarmInspectorByLocalGovernment{localGovernment}")]
        public async Task<IActionResult> GetFarmInspectorByLocalGovernment([FromRoute] string localGoverment)
        {
            var response = await _farmInspectorService.GetFarmInspectorByLocalGovernmentAsync(localGoverment);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
          [Authorize]
        [HttpGet("GetInspectedFarm")]
        public async Task<IActionResult> GetInspectedFarm()
        {
        
            var response = await _farmService.GetAllInspectedFarmerByFarmInspectorEmailAsync(User.FindFirst(ClaimTypes.Email).Value);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
        [Authorize]
         [HttpGet("GetToBeInspectedFarm")]
        public async Task<IActionResult> GetToBeInspectedFarm()
        {
        
            var response = await _farmService.GetTobeInspectedFarmerByFarmInspectorEmailAsync(User.FindFirst(ClaimTypes.Email).Value);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
         [Authorize]
        [HttpPost("CreateFarmReport")]
        public async Task<IActionResult> CreateFarmReport([FromForm] CreateFarmReportRequestModel model)
        {
            var productPics = new SendFarmProductPics();
            int count  = 0;
            var files = HttpContext.Request.Form;
            if (files != null && files.Count > 0)
            {
                  string imageDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                Directory.CreateDirectory(imageDirectory);
                foreach (var file in files.Files)
                {
                    FileInfo fi = new FileInfo(file.FileName);
                    string productImage = Guid.NewGuid().ToString() + fi.Extension;
                    string path = Path.Combine(imageDirectory, productImage);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    if(count == 0)
                    {
                       productPics.FarmProdctImage1 = productImage;
                        count++;
                    }
                    else
                    {
                       productPics.FarmProdctImage2 = productImage;
                    }

                }
            }
            
            
            var response = await _farmReportService.CreateAsync(User.FindFirst(ClaimTypes.Email).Value , model , productPics);
            var farm = await _farmService.UpdateFarmAsync(model.FarmId);

            if(response.IsSucess) return Ok(response);
            return BadRequest(response);
        }


        
       [Authorize]
        [HttpPost("CreateUpdateFarmReport")]
        public async Task<IActionResult> CreateUpdateFarmReport([FromBody] UpdateFarmReportRequestModel model)
        {
      
            
            var response = await _farmReportService.CreateForUpdateAsync(User.FindFirst(ClaimTypes.Email).Value, model);
            if(response.IsSucess) return Ok(response);
            return BadRequest(response);
        }
    }

    
}