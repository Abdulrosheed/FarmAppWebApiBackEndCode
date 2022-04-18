using System.Security.Claims;
using System.Threading.Tasks;
using FirstProject.Dtos;
using FirstProject.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly ICompanyService _companyService;

        public RequestController(IRequestService requestService , ICompanyService companyService)
        {
            _requestService = requestService;
            _companyService = companyService;
        }

        [Authorize]
        [HttpPost ("CreateRequest")]
        public async Task<IActionResult> CreateRequest([FromBody]CreateRequestModel model)
        {
            var company = await _companyService.GetCompanyByEmailReturningCompanyDtoObjectAsync(User.FindFirst(ClaimTypes.Email).Value);
            
            var response = await _requestService.CreateAsync(model,company.Data.Id);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
        [Authorize]
        [HttpGet ("GetAllFulfilledRequest")]
        public async Task<IActionResult> GetAllFulfilledRequest()
        {
           var response = await _requestService.GetAllFulfilledRequestAsync();

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
        [Authorize]
        [HttpGet ("GetAllFulfilledRequestByCompany")]
        public async Task<IActionResult> GetAllFulfilledRequestByCompany()
        {
            var company = await _companyService.GetCompanyByEmailReturningCompanyDtoObjectAsync(User.FindFirst(ClaimTypes.Email).Value);
           var response = await _requestService.GetAllFulfilledRequestByCompanyAsync(company.Data.Id);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }

        [Authorize]
        [HttpGet ("GetAllMergedRequestByCompany")]
        public async Task<IActionResult> GetAllMergedRequestByCompany()
        {
            var company = await _companyService.GetCompanyByEmailReturningCompanyDtoObjectAsync(User.FindFirst(ClaimTypes.Email).Value);
           var response = await _requestService.GetAllMergedRequestByCompanyAsync(company.Data.Id);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }

        [Authorize]
        [HttpGet ("GetAllPendingRequestByCompany")]
        public async Task<IActionResult> GetAllPendingRequestByCompany()
        {
            var company = await _companyService.GetCompanyByEmailReturningCompanyDtoObjectAsync(User.FindFirst(ClaimTypes.Email).Value);
           var response = await _requestService.GetAllPendingRequestByCompanyAsync(company.Data.Id);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }

         [Authorize]
        [HttpGet ("GetAllPendingRequest")]
        public async Task<IActionResult> GetAllPendingRequest()
        {
            
           var response = await _requestService.GetAllPendingRequestAsync();

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }

        [Authorize]
        [HttpGet ("GetAllMergedRequest")]
        public async Task<IActionResult> GetAllMergedRequest()
        {
            
           var response = await _requestService.GetAllMergedRequestAsync();

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }

       
        [HttpGet ("RequestDetails/{id}")]
        public async Task<IActionResult> RequestDetails([FromRoute] int id)
        {
            
           var response = await _requestService.DetailsAsync(id);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }

        [Authorize]
        [HttpPut("UpdateRequest/{id}")]
    
       public async Task<IActionResult> UpdateRequest([FromRoute] int id , UpdateRequestModel model)
        {
            
           var response = await _requestService.UpdateAsync(model , id);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }

      
    }
}