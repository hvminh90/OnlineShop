using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop.Common
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
         
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            List<string> role = new List<string>() { "ADMIN"};
            
            if (role.Contains(Roles))
                return true;
            else return false;
            
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {

            filterContext.Result = new ViewResult
            {
                ViewName = "~/Areas/Admin/Views/Shared/401.cshtml"
            };
            
        }
    }
}