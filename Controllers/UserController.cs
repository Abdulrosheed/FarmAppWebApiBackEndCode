using System.Security.Claims;
using System.Threading.Tasks;
using FirstProject.Auth;
using FirstProject.Dtos;
using FirstProject.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
      
        public UserController(IUserService userService , IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _userService = userService;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }
        
        [HttpGet ("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            var user = await _userService.GetUserByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);
            var response = await _userService.GetUserAsync(user.Data.Id);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
        [HttpGet ("GetUser/{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
           
            var response = await _userService.GetUserAsync(id);

            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
        }
         [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetAllUserAsync();
            if(response.IsSucess) return Ok(response);
            
            return BadRequest(response);
           
        }
           [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            var response = await _userService.LoginAsync(model);

            if(!response.IsSucess) return BadRequest(response);
            var token = _jwtAuthenticationManager.GenerateToken(response.Data);
            var user =  new LoginBaseResponse
            {
                IsSucess = true,
                Message = "User has logged in sucessfully",
                Data = response.Data,
                Token = token
            };
            return Ok(user);
           
        }
    }
}