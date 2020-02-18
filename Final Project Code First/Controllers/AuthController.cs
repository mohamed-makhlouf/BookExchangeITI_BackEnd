using Final_Project_Code_First.Models;
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
    public class AuthController : ApiController
    {
        private BookExchangeModel db = new BookExchangeModel();


        [HttpPost]
        public IHttpActionResult GetToken()
        {
            //Create Security Key
            string securityKey = "This My Security key made by Mohamed Makhlouf ";
            //Symetric Key
            var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            //var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            ////signingCredintals
            var signingCredintals = new Microsoft.IdentityModel.Tokens.SigningCredentials(key, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);
            //var signingCredintals = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256Signature);

            //AddClaims
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            claims.Add(new Claim(ClaimTypes.Role, "User"));
            claims.Add(new Claim("LogUserId", "1234"));

            ////Create token
            var token = new JwtSecurityToken(
                issuer: "smesk.in",
                audience: "readers",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredintals,
                claims:claims
                
                );
            
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            //return Ok("hello from api");
        }


        [Route("api/Auth/Login")]
        public IHttpActionResult PostLogin(string email,string password)
        {
            var user = db.Users.Where(ww => ww.Email == email && ww.Password == password).ToList();
            if (user.Count == 0)
            {
                return NotFound();
            }
            else
                return Ok(user);
        }


        [Route("api/Auth/Register")]
        public IHttpActionResult PostRegister(User u)
        {
            if(u==null)
            {
                return NotFound();
            }
            db.Entry(u).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return Ok(u);
        }
    }
}
