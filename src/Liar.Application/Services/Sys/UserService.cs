using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Liar.Application.Contracts;
using Liar.Application.Contracts.Dtos.Sys.User;
using Liar.Application.Contracts.IServices;
using Liar.Core.Helper;
using Liar.Domain.Shared.BaseModels;
using Liar.Domain.Sys;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.Domain.Repositories;

namespace Liar.Application.Services.Sys
{
    public class UserService : LiarAppService, IUserService
    {
        private readonly IRepository<SysUser> _userRepository;

        public UserService(IRepository<SysUser> userRepository)
        {
            this._userRepository = userRepository;
        }

        public Task<AppSrvResult> ChangeStatusAsync(long id, int status)
        {
            throw new NotImplementedException();
        }

        public Task<AppSrvResult> ChangeStatusAsync(IEnumerable<long> id, int status)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppSrvResult<long>> CreateAsync(UserCreationDto input)
        {
            if (await _userRepository.AllAsync(x => x.Account == input.Account))
                return Problem(HttpStatusCode.BadRequest, "账号已经存在");

            var user = ObjectMapper.Map<UserCreationDto, SysUser>(input);
            user.Id = 1600000000001;
            user.Account = user.Account.ToLower();
            user.Salt = SecurityHelper.GenerateRandomCode(5);
            user.Password = HashHelper.GetHashedString(HashType.MD5, user.Password, user.Salt);
            user.CreateBy = 1600000000000;
            user.CreateTime = DateTime.Now;

            await _userRepository.InsertAsync(user);

            return user.Id;
        }

        public Task<AppSrvResult> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<PageModelDto<UserDto>> GetPagedAsync(UserSearchPagedDto search)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetPermissionsAsync(long userId, IEnumerable<string> permissions)
        {
            throw new NotImplementedException();
        }

        public Task<AppSrvResult> SetRoleAsync(long id, UserSetRoleDto input)
        {
            throw new NotImplementedException();
        }

        public Task<AppSrvResult> UpdateAsync(long id, UserUpdationDto input)
        {
            throw new NotImplementedException();
        }
    }
}
