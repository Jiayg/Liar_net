using Microsoft.AspNetCore.Authorization;

namespace Liar.HttpApi.Authorize
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement() { }
    }
}
