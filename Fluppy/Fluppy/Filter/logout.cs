using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fluppy.Filter
{
    public class logout : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["LoginnedUser"] == null)
            {
                filterContext.Result = new RedirectResult("~/Home");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}