using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Liar.Application.Contracts.IServices;
using Liar.HttpApi.Shared.Authorize;

namespace Liar.HttpApi.Host.Authorize
{
    public class PermissionHandlerLocal : PermissionHandler
    {
        private readonly IUserService _userService;

        public PermissionHandlerLocal(IUserService userService)
        {
            this._userService = userService;
        }

        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="codes"></param>
        /// <returns></returns>
        protected override async Task<bool> CheckUserPermissions(long userId, IEnumerable<string> codes)
        {
            var permissions = await _userService.GetPermissionsAsync(userId, codes);
            bool result = permissions != null && permissions.Any();
            return result;
        }
    }
}
