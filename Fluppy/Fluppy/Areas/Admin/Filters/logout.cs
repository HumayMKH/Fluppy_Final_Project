using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fluppy.Areas.Admin.Filters
{
    public class logout : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Loginner"] == null)
            {
                filterContext.Result = new RedirectResult("~/Admin/Home");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}