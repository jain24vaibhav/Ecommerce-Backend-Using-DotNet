using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.DA;
using Ecommerce.models;
using Ecommerce.models.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        EcommerceContext _db = new EcommerceContext();
        UserDA _user = new UserDA();

        private IConfiguration _config;

        public UserController(IConfiguration config)
        {
            _config = config;
        }



        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.userEmail),
                    new Claim(ClaimTypes.Role, user.userRole)
                };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims: claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string Hash(string input)
        {
            return Convert.ToBase64String
                (
                 SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(input))
                );
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllUsers()
        {
            var res = _user.GetAllUsers();
            return Ok(res);
        }

        [HttpPost("login")]
        public IActionResult UserLogin(LoginModel user)
        {
            var isUser = _user.UserLogin(user);
            if (isUser != null)
            {
                var tokenstring = GenerateJSONWebToken(isUser);
                return Ok( new { token = tokenstring, UserEmail = isUser.userEmail});
            }
                
            else
                return BadRequest(new { message = "User not found." });
        }

    }
}