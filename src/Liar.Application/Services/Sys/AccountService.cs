using System;
using System.Collections.Generic;
using System.Data;
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
        public async Task<ResultDetails<UserInfoDto>> GetUserInfoAsync(long id)
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

                    var roles = _roleRepository.Where(x => roleIds.Contains(x.Id)).Select(x => new
                    {
                        x.Id,
                        x.Tips,
                        x.Name
                    }).ToList();


                    foreach (var role in roles)
                    {
                        userInfoDto.Roles.Add(role.Tips);
                        userInfoDto.Profile.Roles.Add(role.Name);
                    }

                    // 查询出所有角色菜单关系
                    var relations = _relationRepository.Where(x => roleIds.Contains(x.RoleId)).Select(x => x.MenuId).ToList();

                    // 查询菜单
                    var roleMenus = _menuRepository.Where(x => relations.Contains(x.Id)).Select(x => x.Url).Distinct().ToList();

                    if (roleMenus?.Count > 0)
                    {
                        userInfoDto.Permissions.AddRange(roleMenus);
                    }
                }

                return Success(userInfoDto);
            }
            catch (Exception)
            {
                return Fail<UserInfoDto>(HttpStatusCode.BadRequest, "未找到账户信息");
            }
        }
    }
}
