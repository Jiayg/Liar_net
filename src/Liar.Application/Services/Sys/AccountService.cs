using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Threading.Tasks;
using Liar.Application.Contracts;
using Liar.Application.Contracts.Dtos.Sys.User;
using Liar.Application.Contracts.IServices.Sys;
using Liar.Domain.Sys;
using Volo.Abp.Domain.Repositories;

namespace Liar.Application.Services.Sys
{
    public class AccountService : AppService, IAccountService
    {
        private readonly IRepository<SysUser> _userRepository;
        private readonly IRepository<SysRole> _roleRepository;
        private readonly IRepository<SysMenu> _menuRepository;
        private readonly IRepository<SysRelation> _relationRepository;

        public AccountService(IRepository<SysUser> userRepository,
            IRepository<SysRole> roleRepository,
            IRepository<SysMenu> menuRepository,
            IRepository<SysRelation> relationRepository)
        {
            this._userRepository = userRepository;
            this._roleRepository = roleRepository;
            this._menuRepository = menuRepository;
            this._relationRepository = relationRepository;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppSrvResult<UserInfoDto>> GetUserInfoAsync(long id)
        {
            try
            {
                var user = await _userRepository.FirstAsync(x => x.Id == id);
                var userProfile = ObjectMapper.Map<SysUser, UserProfileDto>(user);

                if (userProfile == null)
                    return null;

                var userInfoDto = new UserInfoDto { Id = id, Profile = userProfile };

                if (userProfile.RoleIds.IsNotNullOrEmpty())
                {
                    var roleIds = userProfile.RoleIds.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x));

                    var roles = from role in await _roleRepository.GetListAsync()
                                where roleIds.Contains(role.Id)
                                select new
                                {
                                    role.Id,
                                    role.Tips,
                                    role.Name
                                };

                    foreach (var role in roles)
                    {
                        userInfoDto.Roles.Add(role.Tips);
                        userInfoDto.Profile.Roles.Add(role.Name);
                    }

                    // 查询出所有角色菜单关系
                    var relations = from relation in await _relationRepository.GetListAsync()
                                    where roleIds.Contains(relation.RoleId)
                                    select relation.MenuId;

                    // 查询菜单
                    var roleMenus = (from menu in await _menuRepository.GetListAsync()
                                     where relations.Contains(menu.Id)
                                     select menu.Url).Distinct().ToList();

                    if (roleMenus?.Count > 0)
                    {
                        userInfoDto.Permissions.AddRange(roleMenus);
                    }
                }

                return userInfoDto;
            }
            catch (Exception)
            {
                return Fail(HttpStatusCode.BadRequest, "未找到账户信息");
            }
        }
    }
}
