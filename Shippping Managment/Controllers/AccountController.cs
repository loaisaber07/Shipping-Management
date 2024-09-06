using Business_Layer.DTO;
using Data_Access_Layer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager ,
            IConfiguration configuration , SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginRequestDTO loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            // Determine if this is an admin login or a regular user login
            var isAdminLogin = loginRequest.EmailOrUsername.Equals("admin", StringComparison.OrdinalIgnoreCase);

            ApplicationUser user = null;

            if (isAdminLogin)
            {
               
                user = await userManager.FindByNameAsync("admin");
            }
            else
            {
                if (loginRequest.EmailOrUsername.Contains("@"))
                {
                    user = await userManager.FindByEmailAsync(loginRequest.EmailOrUsername);
                }
                else
                {
                    user = await userManager.FindByNameAsync(loginRequest.EmailOrUsername);
                }
            }

            // Case : user not found
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await signInManager.PasswordSignInAsync(user, loginRequest.Password, false, false);
            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            // Generate token and return success response
            string token = GetTokenString();
            return Ok(token);
        }


        private string GetTokenString() {

            var JwtSetting= configuration.GetSection("JwtSetting");
            string? key = JwtSetting["SecretKey"];
            var secretKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredination = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken( signingCredentials:signingCredination  ); 
            var tokenObj = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenObj;
        }

        // add admin data ( For testing purposes only)
        [HttpGet]
        public async Task<ActionResult> AddAdmin()
        {
            var user = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, "Admin_123456");
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
