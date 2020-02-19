using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace Final_Project_Code_First.Controllers{



    [Authorize(Roles ="Administator")]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        //[HttpPost]
        //public Object GetName2()
        //{
        //    var identity = User.Identity as ClaimsIdentity;
        //    if (identity != null)
        //    {
        //        IEnumerable<Claim> claims = identity.Claims;
        //        var name = claims.Where(p => p.Type == "name").FirstOrDefault()?.Value;
        //        return new
        //        {
        //            data = name
        //        };
        //    }
        //    return null;
        //}
    }
}
