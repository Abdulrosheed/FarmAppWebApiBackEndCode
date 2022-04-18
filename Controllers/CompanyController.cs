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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
         private readonly IMailMessage _mailMessage;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CompanyController(ICompanyService companyService, IMailMessage mailMessage, IEmailService emailService , IWebHostEnvironment webHostEnvironment)
        {
            _companyService = companyService;
            _mailMessage = mailMessage;
            _emailService = emailService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("RegisterCompany")]
        public async Task<IActionResult> RegisterCompany([FromForm] CreateCompanyRequestModel model)
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
            var response = await _companyService.CreateAsync(model);
           if(response.IsSucess)return Ok(response);
           return BadRequest(response);
        }
        [Authorize]
        [HttpGet ("GetCompany")]
        public async Task<IActionResult> GetCompany()
        {
            var response = await _companyService.GetCompanyByEmailReturningCompanyDtoObjectAsync(User.FindFirst(ClaimTypes.Email).Value);
            

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
       
           [HttpGet ("GetCompanyFromRoute/{id}")]
        public async Task<IActionResult> GetCompanyFromRoute([FromRoute] int id)
        {
            
            var response = await _companyService.GetCompanyReturningCompanyDtoObjectAsync(id);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
         
         [HttpGet("GetAllCompanies")]
        public async Task<IActionResult> GetAllCompanies()
        {
            var response = await _companyService.GetAllCompanyAsync();
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
       
          [Authorize]
        [HttpPut("UpdateCompany")]
        public async Task<IActionResult> UpdateCompany([FromBody] UpdateCompanyRequestModel model)
        {
            var company = await _companyService.GetCompanyByEmailReturningCompanyDtoObjectAsync(User.FindFirst(ClaimTypes.Email).Value);
            var response = await _companyService.UpdateAsync(model,company.Data.Id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
    
        [HttpDelete("DeleteCompany/{id}")]
        public async Task<IActionResult> DeleteCompany([FromRoute] int id)
        {
           
            var response = await _companyService.DeleteAsync(id);
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }

    }
}