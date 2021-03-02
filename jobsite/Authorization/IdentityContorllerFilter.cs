using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Authorization
{
    public class IdentityContorllerFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Trace.WriteLine("Hello From Contorller Filter");
            Controller controller = context.Controller as Controller;
            var claim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Discriminator");
            if (claim != null) 
            {
                controller.ViewData["Discriminator"] = claim.Value;
            }

            // Do something before the action executes.
            //MyDebug.Write(MethodBase.GetCurrentMethod(), context.HttpContext.Request.Path);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.
            //MyDebug.Write(MethodBase.GetCurrentMethod(), context.HttpContext.Request.Path);
        }
    }
}
