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
                string token = await GetTokenAsync(user , user.Id);
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
                string token = await GetTokenAsync(user , user.Id);
                var roles = await userManager.GetRolesAsync(user);
                return Ok(new { token=token , Role=roles , ID=user.Id });

            }
            return BadRequest();

        }


        private async Task<string> GetTokenAsync(ApplicationUser user , string id)
        {
            var userClaims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim("userID",id)
    };
            Console.WriteLine(user.Id);

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtSetting = configuration.GetSection("JwtSetting");
            var keyBase64 = jwtSetting["SecretKey"];
            var key = Convert.FromBase64String(keyBase64);
            var secretKey = new SymmetricSecurityKey(key);

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);
            var expiration = DateTime.UtcNow.AddHours(1);

            var token = new JwtSecurityToken(
                claims: userClaims,
                signingCredentials: signingCredentials,
                expires: expiration
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }



    }
}
