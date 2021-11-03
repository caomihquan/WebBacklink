
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebBacklink.Common;

namespace WebBacklink.Controllers
{
    public class BaseController : Controller
    {

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session[CommonConstants.CurrentCulture] != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(CommonConstants.CurrentCulture.ToString());
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(CommonConstants.CurrentCulture.ToString());
            }
        }

        public ActionResult ChangeCulture(string currentCulture, string returnUrl)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(currentCulture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(currentCulture);

            Session[CommonConstants.CurrentCulture] = currentCulture;
            return Redirect(returnUrl);
        }

        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "User", action = "Login" }));
            }
            base.OnActionExecuting(filterContext);
        }

        
    }
}