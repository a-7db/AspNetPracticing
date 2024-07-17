using AspNetPracticing.WebAPI.DTOs;
using AspNetPracticing.WebAPI.Identity;
using AspNetPracticing.WebAPI.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetPracticing.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtService _jwtService;

        public AccountController
            (
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                RoleManager<ApplicationRole> roleManager,
                IJwtService jwtService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApplicationUser>> Register(RegisterDTO registerDTO)
        {
            ApplicationUser user = new()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Username,
                Lastname = registerDTO.Username
            };

            // To Create User And Store it => Database
            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {                                         // To Unsave in Cockies
                await _signInManager.SignInAsync(user, isPersistent: false);

                return user;
            }
            else
            {
                // IF can't create user cause ERRORS
                return BadRequest(result.Errors.Select(er => er.Description));
            }

        }

        [HttpGet(Name = "IsEmailAlreadyRegistered")]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
           var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Ok(true);
            }
            else {
                return Ok(false);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApplicationUser>> Login(LoginDTO loginDTO)
        {
           var result = await _signInManager.PasswordSignInAsync
                (loginDTO.Username, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

            //      lockoutOnFailure: false --> block user login if user tried many times
            //                                  (send many request to SERVER)

            if (result.Succeeded)
            {
                ApplicationUser? user = await _userManager.FindByNameAsync(loginDTO.Username);

                if (user == null)
                {
                    return NoContent();
                }

                var authResponse = _jwtService.CreateJwtToken(user);

                return Ok(authResponse);
            }

            return Problem("Invalid Email or Password");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Login()
        {
            await _signInManager.SignOutAsync();

            return NoContent();
        }
    }
}
