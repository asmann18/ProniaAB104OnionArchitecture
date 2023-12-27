using Microsoft.AspNetCore.Mvc;
using Pronia.Application.Abstractions.Services;
using Pronia.Application.DTOs.AppUserDtos;

namespace Pronia.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register(AppUserRegisterDto appUserRegisterDto)
        {
            await _service.Register(appUserRegisterDto);
            return NoContent();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(AppUserLoginDto appUserLoginDto)
        {
            await _service.Login(appUserLoginDto);
            return NoContent();
        }
    }
}
