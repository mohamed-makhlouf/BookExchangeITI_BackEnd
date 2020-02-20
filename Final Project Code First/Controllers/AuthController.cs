using Final_Project_Code_First.Models;
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
    public class AutheController : ApiController
    {

        private BookExchangeModel db = new BookExchangeModel();

        //[HttpPost]
        //public IHttpActionResult GetToken()
        //{
        //    //Create Security Key
        //    string securityKey = "This My Security key made by Mohamed Makhlouf ";
        //    //Symetric Key
        //    var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

        //    //var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        //    ////signingCredintals
        //    var signingCredintals = new Microsoft.IdentityModel.Tokens.SigningCredentials(key, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);
        //    //var signingCredintals = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256Signature);

        //    //AddClaims
        //    List<Claim> claims = new List<Claim>();
        //    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        //    claims.Add(new Claim(ClaimTypes.Role, "User"));
        //    claims.Add(new Claim("LogUserId", "1234"));

        //    ////Create token
        //    var token = new JwtSecurityToken(
        //        issuer: "smesk.in",
        //        audience: "readers",
        //        expires: DateTime.Now.AddHours(1),
        //        signingCredentials: signingCredintals,
        //        claims:claims
                
        //        );
            
            
        //    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        //    //return Ok("hello from api");
        //}


        [Route("api/Auth/Login")]
        [HttpPost]
        public IHttpActionResult PostLogin(LoginParameter login)
        {
            var user = db.Users.Where(ww => ww.Email == login.Email && ww.Password == login.Password).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(generateTokenForUser(user));

            }
        }


        [Route("api/Auth/Register")]
        public IHttpActionResult PostRegister(Final_Project_Code_First.Models.User u)
        {
            if(u==null)
            {
                return NotFound();
            }
            db.Entry(u).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return Ok();
        }

        public string generateTokenForUser(User user)
        {
            // Create Security Key
            string securityKey = "//OUR_GRAD_PROJECT_1234!@#123456";
            //Symetric Key
            var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            //var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            ////signingCredintals
            var signingCredintals = new Microsoft.IdentityModel.Tokens.SigningCredentials(key, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);
            //var signingCredintals = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256Signature);

            //AddClaims
            List<Claim> claims = new List<Claim>();
           // claims.Add(new Claim(ClaimTypes.Role, user.));
            claims.Add(new Claim(ClaimTypes.Role, user.UserRole.Name));
            claims.Add(new Claim("LogUserId", user.UserId.ToString()));

            ////Create token
            var token = new JwtSecurityToken(
                issuer: "smesk.in",
                audience: "readers",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredintals,
                claims: claims

                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

   
}
