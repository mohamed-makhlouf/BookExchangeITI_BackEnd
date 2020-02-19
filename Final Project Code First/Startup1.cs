using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Owin.Security.OAuth;

[assembly: OwinStartup(typeof(Final_Project_Code_First.Startup1))]

namespace Final_Project_Code_First
{
    public class Startup1
    {
        string securityKey = "//OUR_GRAD_PROJECT_1234!@#123456";
        //OUE_GRAD_PROJECT_KEY";

        public void Configuration(IAppBuilder app)
        {
            
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    //set up Validate Data
                    ValidIssuer = "smesk.in", //some string, normally web url,  
                    ValidAudience = "readers",
                    IssuerSigningKey = key
                }
            });

           
            
        }
    }
}
