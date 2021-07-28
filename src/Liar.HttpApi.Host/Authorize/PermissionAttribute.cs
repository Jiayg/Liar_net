using System;
using Microsoft.AspNetCore.Authorization;

namespace Liar.HttpApi.Host.Authorize
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PermissionAttribute: AuthorizeAttribute
    {
        public string[] Codes { get; private set; }

        public PermissionAttribute(params string[] codes) : base(Permission.Policy)
        {
            Codes = codes;
        }
    }
}
