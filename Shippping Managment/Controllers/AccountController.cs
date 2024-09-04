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

        public AccountController(UserManager<ApplicationUser> userManager ,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost]
        public ActionResult Login(/*Login Dto Here!*/)  
            {

            string token = GetTokenString();
            return Ok(token); 
        }

        private string GetTokenString() {

            var JwtSetting= configuration.GetSection("JwtSetting");
            string? key = JwtSetting["SecretKey"];
            var secretKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredination = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
          signingCredentials:signingCredination      
                ); 
var tokenObj = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenObj;
        }

    }
}
