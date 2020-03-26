using AutoMapper;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.User;
using InventoryManagementSystem.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        protected readonly IAuthService authService;
        protected readonly IMapper mapper;
        public AuthenticationController(IAuthService authService, IMapper mapper)
        {
            this.authService = authService;
            this.mapper = mapper;
        }

        [HttpPost("Authenticate")]
        public ActionResult<string> Authenticate([FromBody] UserCredentialsDTO userCredDto){
            if(! ModelState.IsValid || userCredDto == null)
                return BadRequest();
            
            var token = authService.Authenticate(mapper.Map<User>(userCredDto));

            if(token == null)
                return Unauthorized();

            return Ok(token);
        }

        [HttpPost("Register")]
        public ActionResult<string> Register([FromBody] UserDTO userDto){
            if(! ModelState.IsValid || userDto == null)
                return BadRequest();
            
            var token = authService.Register(mapper.Map<User>(userDto));

            if(token == null)
                return Unauthorized();

            return Ok(token);
        }
    }
}