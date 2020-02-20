using Final_Project_Code_First.Models.ActionParameters;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace Final_Project_Code_First.Controllers
{
    public class AuthorizationController : ApiController
    {
        [HttpPost]
        [Route("api/Authorization/get")]
        public IHttpActionResult GetToken()
        {
            //Create Security Key
            string securityKey = "This My Security key made by ";
            //Symetric Key
            var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            //var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            ////signingCredintals
            var signingCredintals = new Microsoft.IdentityModel.Tokens.SigningCredentials(key, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);
            //var signingCredintals = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256Signature);

            //add Claims
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            claims.Add(new Claim(ClaimTypes.Role, "User"));

            claims.Add(new Claim("LoggenInUserId", "123"));
            ////Create token
            var token = new JwtSecurityToken(
                issuer: "smesk.in",
                audience: "readers",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredintals,
                claims: claims
                );
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        [HttpGet]
        [Route("api/ayhaga")]
        public IHttpActionResult GetAyHaga()
        {
            return Ok("Ay Haga ya ali");
        }
        [HttpPost]
        
        [Route("api/Authorization/login")]
        
        public IHttpActionResult Login(LoginParameter login)
        {
            return Ok(login.Email + " " + login.Password);
        }
    }
}
