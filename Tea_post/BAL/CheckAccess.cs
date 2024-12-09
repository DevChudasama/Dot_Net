using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Tea_post.BAL
{
    public class CheckAccess : ActionFilterAttribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            var rd = filterContext.RouteData;
            string currentAction = rd.Values["action"].ToString();
            string currentController = rd.Values["controller"].ToString();
            //string currentArea = rd.DataTokens["area"].ToString(); 

            //         Console.WriteLine($"-----------------------{i++}---------------------------");
            //         Console.WriteLine("USERID BOOL   : " + (string.IsNullOrWhiteSpace(filterContext.HttpContext.Session.GetInt32("UserID").ToString())) + (filterContext.HttpContext.Session.GetInt32("UserID")==null));
            //         Console.WriteLine("USERName BOOL : " + (string.IsNullOrEmpty(filterContext.HttpContext.Session.GetString("Username"))) + (filterContext.HttpContext.Session.GetString("Username") == null));
            //Console.WriteLine("USERID : " + (filterContext.HttpContext.Session.GetInt32("UserID")));
            //Console.WriteLine("USERNAME : " + filterContext.HttpContext.Session.GetString("Username"));

            if ((filterContext.HttpContext.Session.GetInt32("UserID") == null || filterContext.HttpContext.Session.GetString("UserName") == null))
            {
                filterContext.HttpContext.Session.Clear();
                filterContext.Result = new RedirectResult("~/Account/Account/Index");
            }
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            filterContext.HttpContext.Response.Headers["Expires"] = "-1";
            filterContext.HttpContext.Response.Headers["Pragma"] = "no-cache";

            base.OnResultExecuting(filterContext);
        }

    }
}
