using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Final_Project_Code_First.Controllers
{
    public static class UserUtilities
    {
        public static int GetCurrentUserId(IPrincipal user)
        {
            return int.Parse((user.Identity as ClaimsIdentity).Claims.Where(claim => claim.Type.Equals("LogUserId")).FirstOrDefault().Value);
        }
       
    }
}