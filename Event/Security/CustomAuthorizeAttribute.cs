using Microsoft.AspNet.Identity;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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
            bool superAdmin = false;
            bool isAdmin =false;
            bool isuser = false;

            IserviceAdmin spa = new serviceAdmin();
            IPrincipal user = httpContext.User;
            bool authorize = false;


            string userid = user.Identity.Name;
            Admin _admin = spa.Get(x=>x.mailAdmin==userid);



            if (_admin!=null){
                if (_admin.isSuperAdmin)
                {
                    superAdmin = true;
                }
                else { isAdmin = true; }

            }




            if (user.Identity.IsAuthenticated)
            {
                
                    
                
               
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
            //if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    filterContext.Result = new ViewResult()
            //    {
            //        ViewName = "~/Home/Unauthorized"
            //    };
            //}
            filterContext.Result = new RedirectToRouteResult(new
             RouteValueDictionary(new { controller = "Home", action = "Unauthorized" }));
           // filterContext.Result = new HttpUnauthorizedResult();
        }


    }
}