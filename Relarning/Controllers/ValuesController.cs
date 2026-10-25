using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Relarning.Models;
using Relarning.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Relarning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(JwtOptions JwtOptions) : ControllerBase
    {
        [HttpPost("sign-in")]
        public async Task<ActionResult> SignIn(SignInDTO request)
        {
            //check is user name and password corrct or no
            if (request.UserName != "Admin" && request.Password != "admin")
                return Unauthorized("User Name Or Password Wrong");
            //create Token.
            var TokenHandler = new JwtSecurityTokenHandler();
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = JwtOptions.Issuere,
                Audience = JwtOptions.Audiance,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SigningKey)), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name,request.UserName),
                    new (ClaimTypes.NameIdentifier,"1"),
                    new Claim("Email","ia1900@fayoum.edu.eg"),
               }),
            };
            return Ok(TokenHandler.WriteToken(TokenHandler.CreateToken(TokenDescriptor)));
        }
    }
}
