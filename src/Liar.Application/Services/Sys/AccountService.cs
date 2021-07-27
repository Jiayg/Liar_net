using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liar.Application.Contracts.Dtos.Sys.User;
using Liar.Application.Contracts.IServices.Sys;
using Liar.Domain.Sys;
using Volo.Abp.Domain.Repositories;

namespace Liar.Application.Services.Sys
{
    public class AccountService : LiarAppService, IAccountService
    {
        private readonly IRepository<SysUser> _userRepository;

        public AccountService(IRepository<SysUser> userRepository)
        {
            this._userRepository = userRepository;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserInfoDto> GetUserInfoAsync(long id)
        {
            var user = await _userRepository.FirstAsync(x => x.Id == id);
            var userProfile = ObjectMapper.Map<SysUser, UserProfileDto>(user);

            if (userProfile == null)
                return null;

            var userInfoDto = new UserInfoDto { Id = id, Profile = userProfile };

            if (userProfile.RoleIds.IsNotNullOrEmpty())
            {
                var roleIds = userProfile.RoleIds.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x));
                //var roles = await _roleRepository
                //                  .Where(x => roleIds.Contains(x.Id))
                //                  .Select(r => new { r.Id, r.Tips, r.Name })
                //                  .ToListAsync();
                //foreach (var role in roles)
                //{
                //    userInfoDto.Roles.Add(role.Tips);
                //    userInfoDto.Profile.Roles.Add(role.Name);
                //}

                //var roleMenus = await _menuRepository.GetMenusByRoleIdsAsync(roleIds.ToArray(), true);
                //if (roleMenus?.Count > 0)
                //{
                //    userInfoDto.Permissions.AddRange(roleMenus.Select(x => x.Url).Distinct());
                //}
            }

            return userInfoDto;
        }
    }
}
