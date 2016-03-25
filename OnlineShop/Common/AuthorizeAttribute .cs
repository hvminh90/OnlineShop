using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace OnlineShop.Common
{
    public class AuthorizeClientAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string[] userRoles = System.Web.Security.Roles.GetRolesForUser("admin");
            var userName = httpContext.User.Identity.Name;
            if(userName == "")
            {
                return false;
            }
            else
            {
                if (Roles != "")
                {
                    if (userRoles.Contains(Roles))
                        return true;
                    else return false;
                }
            }
            return true;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //User isn't logged in
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("/dang-nhap");
                
            }
            //User is logged in but has no access
            else
            {
                filterContext.Result = new RedirectResult("/error/403");
            }
        }
    }
}