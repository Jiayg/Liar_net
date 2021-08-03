using System;
using System.Linq;
using System.Threading.Tasks;
using Liar.Application.Caching.Consts;
using Liar.Application.Contracts.Dtos.Sys.User;
using Liar.Caching.Abstractions;
using Liar.Domain.Shared.Consts;
using Liar.Domain.Sys;
using Volo.Abp.Domain.Repositories;

namespace Liar.Application.Caching.Caching
{
    public class SysCachingService : AbstractCacheService
    {
        public readonly IRedisService _redisService;
        private readonly IRepository<SysUser> _userRepository;

        public SysCachingService(IRedisServiceResolver redisServiceResolver, IRepository<SysUser> userRepository)
        {
            _redisService = redisServiceResolver.Default();
            this._userRepository = userRepository;
        }

        public override Task PreheatAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 用户详情缓存
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserValidateDto> GetUserValidateInfoFromCacheAsync(long id)
        {
            var cacheKey = ConcatCacheKey(SysCachingConsts.UserValidateInfoKeyPrefix, id.ToString());

            var cachValue = await _redisService.GetAsync(cacheKey, async () =>
            {
                var userValidate = _userRepository.Where(x => x.Id == id).Select(x => new UserValidateDto()
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
                return await Task.FromResult(userValidate);
            }, TimeSpan.FromSeconds(CachingConsts.OneDay));

            return cachValue;
        }
    }
}
