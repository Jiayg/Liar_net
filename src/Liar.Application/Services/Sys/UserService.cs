using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Liar.Application.Contracts;
using Liar.Application.Contracts.Dtos.Sys.User;
using Liar.Application.Contracts.IServices;
using Liar.Core.Extensions;
using Liar.Core.Helper;
using Liar.Domain.Shared.BaseModels;
using Liar.Domain.Sys;
using Volo.Abp.Domain.Repositories;

namespace Liar.Application.Services.Sys
{
    public class UserService : AppService, IUserService
    {
        private readonly IRepository<SysUser> _userRepository;

        public UserService(IRepository<SysUser> userRepository)
        {
            this._userRepository = userRepository;
        }

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<bool> ChangeStatusAsync(long id, int status)
        {
            var model = await _userRepository.UpdateAsync(new SysUser { Id = id, Status = status });
            return model != null;
        }

        /// <summary>
        /// 批量修改用户状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<bool> ChangeStatusAsync(IEnumerable<long> ids, int status)
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

            return true;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ResultDetails<long>> CreateAsync(UserCreationDto input)
        {
            if (await _userRepository.AllAsync(x => x.Account == input.Account))
                return Fail<long>(HttpStatusCode.BadRequest, "账号已经存在");

            var user = ObjectMapper.Map<UserCreationDto, SysUser>(input);
            user.Id = IdGenerater.GetNextId();
            user.Account = user.Account.ToLower();
            user.Salt = SecurityHelper.GenerateRandomCode(5);
            user.Password = HashHelper.GetHashedString(HashType.MD5, user.Password, user.Salt);
            user.CreateBy = 1600000000000;
            user.CreateTime = DateTime.Now;

            await _userRepository.InsertAsync(user);

            return Success(user.Id);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(long id)
        {
            await _userRepository.DeleteAsync(x => x.Id == id);
            return true;
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
        public Task<List<string>> GetPermissionsAsync(long userId, IEnumerable<string> permissions)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> SetRoleAsync(long id, UserSetRoleDto input)
        {
            var roleIdStr = input.RoleIds == null ? null : string.Join(",", input.RoleIds);
            var model = await _userRepository.UpdateAsync(new SysUser() { Id = id, RoleIds = roleIdStr });

            return model != null;
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<bool> UpdateAsync(long id, UserUpdationDto input)
        {
            throw new NotImplementedException();
        }
    }
}
