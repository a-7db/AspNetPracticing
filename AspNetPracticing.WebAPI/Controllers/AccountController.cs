using AspNetPracticing.WebAPI.DTOs;
using AspNetPracticing.WebAPI.Identity;
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

        public AccountController
            (
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                RoleManager<ApplicationRole> roleManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
                return BadRequest(result.Errors);
            }

        }

        [HttpGet(Name = "IsEmailAlreadyRegistered")]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
           var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Ok(false);
            }
            else {
                return Ok(false);
            }
        }
    }
}
