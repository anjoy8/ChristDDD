using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace Christ3D.Infrastruct.Identity.Authorization
{
    public class ClaimsRequirementHandler : AuthorizationHandler<ClaimRequirement>
    {
        private readonly IConfiguration _configuration;

        public ClaimsRequirementHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimRequirement requirement)
        {

            if (_configuration["Authentication:IdentityServer4:Enabled"].ObjToBool())
            {
                var rolename = context.User.Claims.FirstOrDefault(c => c.Type == "rolename");
                if (rolename != null && rolename.Value == "SuperAdmin")
                {
                    context.Succeed(requirement);
                }
            }
            else
            {
                var claim = context.User.Claims.FirstOrDefault(c => c.Type == requirement.ClaimName);
                if (claim != null && claim.Value.Contains(requirement.ClaimValue))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}