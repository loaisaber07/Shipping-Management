using Business_Layer.DTO;
using Data_Access_Layer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Shippping_Managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<ApplicationUser> userManager ,
            IConfiguration configuration , SignInManager<ApplicationUser> signInManager ,
           RoleManager<IdentityRole> roleManager )
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
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
        [HttpGet]
        [Route("role")]
        public async Task<IActionResult> AddRole(string name)
        {
            // Validate role name
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new { Error = "Role name cannot be empty or whitespace." });
            }

            // Additional validation for role name (e.g., length and allowed characters)
            if (name.Length < 3 || !Regex.IsMatch(name, @"^[a-zA-Z0-9_\-]+$"))
            {
                return BadRequest(new { Error = "Role name must be at least 3 characters long and contain only alphanumeric characters, underscores, or hyphens." });
            }
            try
            {
                // Check if the role already exists
                if (await roleManager.RoleExistsAsync(name))
                {
                    return Conflict(new { Error = $"Role '{name}' already exists." });
                }

                // Create the new role
                var result = await roleManager.CreateAsync(new IdentityRole(name));

                // Check if the creation was successful
                if (result.Succeeded)
                {
                    return Ok(new { Message = $"Role '{name}' created successfully." });
                }
                else
                {
                    return StatusCode(500, new { Error = "An error occurred while creating the role.", Details = result.Errors });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Internal server error.", Details = ex.Message });
            }
        }

    }
}
