using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace jobsite.Authorization
{
    public class AdminHandler : AuthorizationHandler<AdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
        {
            Trace.WriteLine("Hello from Admin Handler");
            foreach(var claim in context.User.Claims) 
            {
                Trace.WriteLine($"{claim.Type}: {claim.Value}");
            }
            if (context.User.HasClaim(c => c.Type == "Discriminator" && c.Value == "Admin"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}