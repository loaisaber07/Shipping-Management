using Business_Layer.DTO;
using Data_Access_Layer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<ApplicationUser> userManager ,
            IConfiguration configuration ,
           RoleManager<IdentityRole> roleManager )
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginRequestDTO log)
        {
            if (!ModelState.IsValid) { 
            return BadRequest( new {Messaga=$"Incorect Data " }) ;
            }
            if (log.Username is not null) {
     ApplicationUser? user=await userManager.FindByNameAsync(log.Username);
                if (user is null) {
                    return BadRequest(new { Message = "No userName founded!" });
                }
                bool adminCheck = await userManager.IsInRoleAsync(user, "Admin");
                if (!adminCheck) {
                    return BadRequest(new { Message = $"you are not authorize to use this feature please use Email instead!" });
                }
                bool result = await userManager.CheckPasswordAsync(user ,log.Password);
                if (!result) {
                    return BadRequest(new { Message = "Incorrect Password" }); 
                }
                string token = await GetTokenAsync(user);
                return Ok(token);
            }
            if (log.Email is not null) {
                ApplicationUser? user = await userManager.FindByEmailAsync(log.Email);
                if (user is null) {
                    return BadRequest(new { Message = "Incorrect Email Address" }); 
                } 
       bool result =await userManager.CheckPasswordAsync(user ,log.Password);
                if (!result) {
                    return BadRequest(new { Message = "Incorrect Password try again!" });
                }
                string token = await GetTokenAsync(user);
                return Ok(token);

            }
            return BadRequest();

        }


        private async Task<string> GetTokenAsync(ApplicationUser user) {
            var userClaims = new List<Claim>
            {
new Claim(JwtRegisteredClaimNames.Sub,user.UserName) , 
new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
new Claim(ClaimTypes.Name ,user.UserName)
            };
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles) {
                userClaims.Add(new Claim(ClaimTypes.Role, role));  
            }
            var JwtSetting= configuration.GetSection("JwtSetting");
            string? key = JwtSetting["SecretKey"];
            var secretKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredination = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims:userClaims,
                signingCredentials:signingCredination  ); 
            var tokenObj = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenObj;
        }

        // add admin data ( For testing purposes only)
        //[HttpGet]
        //public async Task<ActionResult> AddAdmin()
        //{
        //    var user = new ApplicationUser
        //    {
        //        UserName = "admin",
        //        Email = "admin",
        //        EmailConfirmed = true
        //    };

        //    var result = await userManager.CreateAsync(user, "Admin_123456");
        //    if (result.Succeeded)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}
        //[HttpPost]
        //public async Task<ActionResult> AddRole(string Name)
        //{
        //    if (string.IsNullOrEmpty(Name))
        //    {
        //        return BadRequest("Role name cannot be empty.");
        //    }
        //    else if (await roleManager.RoleExistsAsync(Name))
        //    {
        //        return BadRequest("Role already exists.");
        //    }
        //    else if (!(await roleManager.RoleExistsAsync(Name)))
        //    {

        //       var result = await roleManager.CreateAsync(new IdentityRole(Name));
        //        return Ok("Role created successfully.");
        //    }
        //    return BadRequest("An error occurred while creating the role.");
        //}

    }
}
