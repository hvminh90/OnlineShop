using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class BaseController : Controller
    {

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }
        // GET: Base
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {


            if (User != null)
            {
                var dao = new UserDAO();
                var username = User.Identity.Name;

                if (!string.IsNullOrEmpty(username))
                {
                    var user = dao.GetByUserName(username);
                    string fullName = !string.IsNullOrEmpty(user.Name)? user.Name : user.UserName ;
                    ViewData.Add("FullName", fullName);
                }
            }
            base.OnActionExecuted(filterContext);
        }

        public BaseController()
        {

        }
    }
}