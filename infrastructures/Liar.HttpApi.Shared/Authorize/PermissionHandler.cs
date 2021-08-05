using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Liar.HttpApi.Shared.Authorize
{
    public abstract class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return;
            }

            var userId = long.Parse(context.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value);

            if (context.Resource is HttpContext httpContext)
            {
                var meta = httpContext.GetEndpoint().Metadata.GetMetadata<PermissionAttribute>();

                if (meta == null)
                {
                    context.Succeed(requirement);
                    return;
                }

                var codes = meta.Codes;
                var result = await CheckUserPermissions(userId, codes);
                if (result)
                {
                    context.Succeed(requirement);
                    return;
                }
            }
            context.Fail();
        }
        protected abstract Task<bool> CheckUserPermissions(long userId, IEnumerable<string> codes);
    }
}
