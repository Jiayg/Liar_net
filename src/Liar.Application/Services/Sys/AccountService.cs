using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Threading.Tasks;
using Liar.Application.Contracts.Dtos.Sys.User;
using Liar.Application.Contracts.IServices.Sys;
using Liar.Application.Contracts.ServiceResult;
using Liar.Core.Helper;
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
        /// 登录获取用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppSrvResult<UserValidateDto>> LoginAsync(UserLoginDto input)
        {
            var user = _userRepository.Where(x => x.Account == input.Account).Select(x => new UserValidateDto
            {
                Id = x.Id,
                Account = x.Account,
                Password = x.Password,
                Salt = x.Salt,
                Status = x.Status,
                Email = x.Email,
                Name = x.Name,
                RoleIds = x.RoleIds
            }).FirstOrDefault();

            // 自动映射模型，服务层引用_mapper出错
            //var user = _userRepository.Where(x => x.Account == input.Account).ProjectTo<UserValidateDto>(_mapper.ConfigurationProvider).FirstOrDefault();

            if (user == null)
            {
                return ProblemFail(HttpStatusCode.BadRequest, "用户名或密码错误");
            }

            var httpContext = HttpContextUtility.GetCurrentHttpContext();

            if (user.Status != 1)
            {
                return ProblemFail(HttpStatusCode.TooManyRequests, "账号已锁定");
            }

            var failLoginCount = 2;
            if (failLoginCount == 5)
            {
                return ProblemFail(HttpStatusCode.TooManyRequests, "连续登录失败次数超过5次，账号已锁定");
            }

            if (HashHelper.GetHashedString(HashType.MD5, input.Password, user.Salt) != user.Password)
            {
                return ProblemFail(HttpStatusCode.BadRequest, "用户名或密码错误");
            }

            if (user.RoleIds.IsNullOrEmpty())
            {
                return ProblemFail(HttpStatusCode.Forbidden, "未分配任务角色，请联系管理员");
            }

            //TODO 登录数据存入缓存

            return await Task.FromResult(user);
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

            return userInfoDto;
        }

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserValidateDto> GetUserValidateInfoAsync(long id)
        {
            // TODO 此处应先取缓存信息 判断缓存登录信息是否完整 缓存不存在则把数据存进去

            var user = await _userRepository.FirstAsync(x => x.Id == id);

            return ObjectMapper.Map<SysUser, UserValidateDto>(user);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppSrvResult> UpdatePasswordAsync(long id, UserChangePwdDto input)
        {
            // TODO 此处应先取缓存信息 判断缓存登录信息是否完整

            //if (user == null)
            //    return Problem(HttpStatusCode.NotFound, "用户不存在,参数信息不完整");


            var user = await _userRepository.FirstAsync(x => x.Id == id);

            var md5OldPwdString = HashHelper.GetHashedString(HashType.MD5, input.OldPassword, user.Salt);
            if (!md5OldPwdString.EqualsIgnoreCase(user.Password))
                return Problem(HttpStatusCode.BadRequest, "旧密码输入错误");

            var newPwdString = HashHelper.GetHashedString(HashType.MD5, input.Password, user.Salt);

            await _userRepository.UpdateAsync(new SysUser { Id = user.Id, Password = newPwdString });

            return AppSrvResult();
        }
    }
}
