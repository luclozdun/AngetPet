using AngetPet.Application.Dtos;
using AngetPet.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AngetPet.Application.Controllers
{
    [ApiController]
    [Route("/authentications")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService service;

        public AuthenticationController(IAuthenticationService service)
        {
            this.service = service;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            var response = await service.SignUp(request);
            return Ok(response);
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            var response = await service.SignIn(request);
            return Ok(response);
        }

    }
}
