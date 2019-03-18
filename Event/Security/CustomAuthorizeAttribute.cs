using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace Event.Security
{
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {

        
        private string[] allowedroles;
        public CustomAuthorizeAttribute(params string[] Roles)
        {
            this.allowedroles = (string[])Roles.Clone();
        }


        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            IPrincipal user = httpContext.User;
            bool authorize = false;
            if (user.Identity.IsAuthenticated)
            {
                bool superAdmin = httpContext.Session["Role"].Equals("SuperAdmin");
                bool isAdmin = httpContext.Session["Role"].Equals("Admin");
                bool isuser = httpContext.Session["Role"].Equals("User");
                if (superAdmin && Roles.Contains("SuperAdmin"))
                {
                    authorize = true;
                }
                if (isAdmin && allowedroles.Contains("Admin"))
                {
                    authorize = true;
                }
                if (isuser && this.allowedroles.Contains("User"))
                {
                    authorize = true;
                }
            }

            return authorize;

        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "unauthorized"
                };
            }
            filterContext.Result = new HttpUnauthorizedResult();
        }


    }
}