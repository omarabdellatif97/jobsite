using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Authorization
{
    public class CandidateHandler : AuthorizationHandler<CandidateRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CandidateRequirement requirement)
        {
            Trace.WriteLine("Hello from Candidate Handler");
            foreach (var claim in context.User.Claims)
            {
                Trace.WriteLine($"{claim.Type}: {claim.Value}");
            }
            if (context.User.HasClaim(c => c.Type == "Discriminator" && c.Value == "Candidate"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
