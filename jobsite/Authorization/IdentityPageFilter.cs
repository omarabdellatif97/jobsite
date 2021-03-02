using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Authorization
{
    public class IdentityPageFilter : IPageFilter
    {
        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            Trace.WriteLine("Hello From Page Filter");
            var page = context.HandlerInstance as PageModel;
            var claim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Discriminator");
            if (claim != null)
            {
                page.ViewData["Discriminator"] = claim.Value;
            }
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
