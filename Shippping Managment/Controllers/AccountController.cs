    using Business_Layer.DTO;
using Business_Layer.Services.FieldJob;
using Data_Access_Layer.AppSetting;
using Data_Access_Layer.DTO.FieldJob;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Repositry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RTools_NTS.Util;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Shippping_Managment.Controllers
{
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IFieldJob fieldJobRepo;
        private readonly JwtSetting config;

        public AccountController(UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
           RoleManager<IdentityRole> roleManager,
           IFieldJob fieldJobRepo , 
           IOptions<JwtSetting> _config)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
            this.fieldJobRepo = fieldJobRepo;
            config = _config.Value;
        }

        [HttpPost]
       
        public async Task<ActionResult> Login(LoginRequestDTO log)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Messaga = $"Incorect Data " });
            }
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            };

            if (log.Email is not null)
            {

                ApplicationUser? user = await userManager.FindByEmailAsync(log.Email);

                if (user is null)
                {
                    return BadRequest(new { Message = "Incorrect Email Address" });
                }
                bool adminCheck = await userManager.IsInRoleAsync(user, "Admin");
                if (adminCheck)
                {
                    return BadRequest(new { Message = $"you are not authorize to use this feature please use Username instead!" });
                }

                bool result = await userManager.CheckPasswordAsync(user, log.Password);
                if (!result)
                {
                    return BadRequest(new { Message = "Incorrect Password try again!" });
                }
                var roles = await userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    if (role == "Employee")
                    {
                        FieldJobDTO f = new FieldJobDTO();
                        FieldJob? fieldJob = new FieldJob();
                        if (user.FiledJobID is not null)
                        {
                            int fieldId = (int)user.FiledJobID;
                            fieldJob = await fieldJobRepo.GetFieldJobById(fieldId);
                            if (fieldJob is not null)
                            {
                                f = FieldJobService.MappingFieldJob(fieldJob);
                            }
                            else
                            {
                                return BadRequest(new { Message = "FieldJob NotFound" });
                            }
                            string Emptoken = await GetTokenAsync(user, user.Id);
                            return Ok(new { token = Emptoken, Role = roles, ID = user.Id, FieldJob = f });
                        }
                    }

                    string token = await GetTokenAsync(user, user.Id);
                    Response.Cookies.Append("jwt", token, cookieOptions);

                 return Ok(new { Message = "Login Successful" ,Role = roles,UserName = user.UserName, ID = user.Id });
                }
            }
            if (log.Username is not null)
            {

                ApplicationUser? user = await userManager.FindByNameAsync(log.Username);
                if (user is null)
                {
                    return BadRequest(new { Message = "Incorrect UserName" });
                }
                bool result = await userManager.CheckPasswordAsync(user, log.Password);
                if (!result)
                {
                    return BadRequest(new { Message = "Incorrect Password Or UserName try again!" });

                }
                var roles = await userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    if (role == "Employee")
                    {
                        FieldJobDTO f = new FieldJobDTO();
                        FieldJob? fieldJob = new FieldJob();
                        if (user.FiledJobID is not null)
                        {
                            int fieldId = (int)user.FiledJobID;
                            fieldJob = await fieldJobRepo.GetFieldJobById(fieldId);
                            if (fieldJob is not null)
                            {
                                f = FieldJobService.MappingFieldJob(fieldJob);
                            }
                            else
                            {
                                return BadRequest(new { Message = "FieldJob NotFound" });
                            }
                            string Emptoken = await GetTokenAsync(user, user.Id);
                            Response.Cookies.Append("jwt", Emptoken, cookieOptions);

                            return Ok(new { Role = roles, ID = user.Id, FieldJob = f, UserName = user.UserName });
                        }
                    }
                }

                string token1 = await GetTokenAsync(user , user.Id);
                Response.Cookies.Append("jwt", token1, cookieOptions);
                return Ok(new { Role=roles , ID=user.Id , UserName = user.UserName });
            }
            return BadRequest();

        }
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "Logout successful" });
        }

        [Authorize]
        [HttpGet("auth/check")]
        public IActionResult CheckAuth()
        {
            return Ok(new { isAuthenticated = true });
        }

        private async Task<string> GetTokenAsync(ApplicationUser user, string id)
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

            //var jwtSetting = configuration.GetSection("JwtSetting");
            var keyBase64 = config.SecretKey; 
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
