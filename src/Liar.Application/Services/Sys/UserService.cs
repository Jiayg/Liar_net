using Liar.Application.Contracts.Dtos.Sys.Role;
using Liar.Application.Contracts.Dtos.Sys.User;
using Liar.Application.Contracts.IServices;
using Liar.Application.Contracts.ServiceResult;
using Liar.Core.Helper;
using Liar.Core.Liar;
using Liar.Domain.Shared;
using Liar.Domain.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Liar.Application.Services.Sys
{
    public class UserService : AppService, IUserService
    {
        private readonly IRepository<SysUser> _userRepository;
        private readonly IRepository<SysRelation> _relationRepositoory;

        public UserService(IRepository<SysUser> userRepository, IRepository<SysRelation> relationRepositoory)
        {
            this._userRepository = userRepository;
            this._relationRepositoory = relationRepositoory;
        }

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<AppSrvResult> ChangeStatusAsync(long id, int status)
        {
            await _userRepository.UpdateAsync(new SysUser { Id = id, Status = status });

            return AppSrvResult();
        }

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<AppSrvResult> ChangeStatusAsync(IEnumerable<long> ids, int status)
        {
            var users = new List<SysUser>();
            foreach (var item in ids)
            {
                users.Add(new SysUser()
                {
                    Id = item,
                    Status = status
                });
            }

            await _userRepository.UpdateManyAsync(users);

            return AppSrvResult();
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppSrvResult<long>> CreateAsync(UserCreationDto input)
        {
            if (await _userRepository.AnyAsync(x => x.Account == input.Account))
                return ProblemFail(HttpStatusCode.BadRequest, "账号已经存在");

            var user = ObjectMapper.Map<UserCreationDto, SysUser>(input);
            user.Id = IdGenerater.GetNextId();
            user.Account = user.Account.ToLower();
            user.Salt = SecurityHelper.GenerateRandomCode(5);
            user.Password = HashHelper.GetHashedString(HashType.MD5, user.Password, user.Salt);

            await _userRepository.InsertAsync(user);

            return user.Id;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppSrvResult> DeleteAsync(long id)
        {
            await _userRepository.DeleteAsync(x => x.Id == id);

            return AppSrvResult();
        }

        /// <summary>
        /// 分页获取列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<PageModelDto<UserDto>> GetPagedAsync(UserSearchPagedDto search)
        {
            Expression<Func<SysUser, bool>> whereCdn = x => true;

            if (search.Account.IsNotEmpty())
                whereCdn.And(x => x.Account.Contains(search.Account));

            if (search.Name.IsNotEmpty())
                whereCdn.And(x => x.Name.Contains(search.Name));

            var query = _userRepository.Where(whereCdn).Take(search.Limit, search.Offset).ToList();

            var result = new PageModelDto<UserDto>()
            {
                Total = _userRepository.Count(),
                Item = ObjectMapper.Map<List<SysUser>, List<UserDto>>(query)
            };

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 获取当前用户是否拥有指定权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        public async Task<List<string>> GetPermissionsAsync(long userId, IEnumerable<string> permissions)
        {
            var userValidateInfo = _userRepository.Where(x => x.Id == userId).Select(s => new UserValidateDto()
            {
                Id = s.Id,
                Account = s.Account,
                Password = s.Password,
                Salt = s.Salt,
                Status = s.Status,
                Email = s.Email,
                Name = s.Name,
                RoleIds = s.RoleIds
            }).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(userValidateInfo.RoleIds))
                return default;

            if (userValidateInfo.Status != 1)
                return default;

            var roleIds = userValidateInfo.RoleIds.Trim().Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x));

            var allMenuCodes = _relationRepositoory
                .Where(x => x.Menu.Status == true)
                .Select(x => new RoleMenuCodesDto { RoleId = x.RoleId, Code = x.Menu.Code })
                .Distinct().ToList();

            var codes = allMenuCodes?.Where(x => roleIds.Contains(x.RoleId)).Select(x => x.Code.ToUpper());
            if (codes != null && codes.Any())
            {
                var result = codes.Intersect(permissions.Select(x => x.ToUpper())).ToList();
                return await Task.FromResult(result);
            }

            return default;
        }

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppSrvResult> SetRoleAsync(long id, UserSetRoleDto input)
        {
            var roleIdStr = input.RoleIds == null ? null : string.Join(",", input.RoleIds);

            await _userRepository.UpdateAsync(new SysUser() { Id = id, RoleIds = roleIdStr });

            return AppSrvResult();
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppSrvResult> UpdateAsync(long id, UserUpdationDto input)
        {
            var user = ObjectMapper.Map<UserUpdationDto, SysUser>(input);

            user.Id = id;

            await _userRepository.UpdateAsync(user);

            return AppSrvResult();
        }
    }
}
