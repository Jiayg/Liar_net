using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Liar.Application.Contracts.CacheConsts;
using Liar.Application.Contracts.Dtos.Sys.Dept;
using Liar.Application.Contracts.Dtos.Sys.Menu;
using Liar.Application.Contracts.Dtos.Sys.Role;
using Liar.Application.Contracts.Dtos.Sys.User;
using Liar.Caching.Abstractions;
using Liar.Domain.Shared.Consts;
using Liar.Domain.Sys;
using Newtonsoft.Json.Linq;
using Volo.Abp.Domain.Repositories;

namespace Liar.Application.Caching.Caching
{
    public class SysCachingService : AbstractCacheService
    {
        public readonly IRedisService _redisService;
        private readonly IRepository<SysUser> _userRepository;
        private readonly IRepository<SysDept> _deptRepository;
        private readonly IRepository<SysMenu> _menuRepository;
        private readonly IRepository<SysRelation> _relationRepository;
        private readonly IRepository<SysRole> _roleRepository;

        public SysCachingService(IRedisServiceResolver redisServiceResolver,
            IRepository<SysUser> userRepository,
            IRepository<SysDept> deptRepository,
            IRepository<SysMenu> menuRepository,
            IRepository<SysRelation> relationRepository,
            IRepository<SysRole> roleRepository)
        {
            _redisService = redisServiceResolver.Default();
            this._userRepository = userRepository;
            this._deptRepository = deptRepository;
            this._menuRepository = menuRepository;
            this._relationRepository = relationRepository;
            this._roleRepository = roleRepository;
        }

        public override Task PreheatAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 所有角色
        /// </summary>
        /// <returns></returns>
        public List<RoleDto> GetAllRolesFromCache()
        {
            return _redisService.Get(SysCachingConsts.RoleListCacheKey, () =>
            {
                return _roleRepository.OrderBy(o => o.Ordinal).Select(x => new RoleDto()
                {
                    Name = x.Name,
                    Ordinal = x.Ordinal,
                    Pid = x.Pid,
                    Tips = x.Tips
                }).ToList();
            }, TimeSpan.FromSeconds(CachingConsts.OneYear));
        }

        /// <summary>
        /// 菜单-角色关联
        /// </summary>
        /// <returns></returns>
        public List<RelationDto> GetAllRelationsFromCache()
        {
            return _redisService.Get(SysCachingConsts.MenuRelationCacheKey, () =>
            {
                return _relationRepository.Select(x => new RelationDto()
                {
                    MenuId = x.MenuId,
                    RoleId = x.RoleId
                }).ToList();
            }, TimeSpan.FromSeconds(CachingConsts.OneYear));
        }

        public List<RoleMenuCodesDto> GetAllMenuCodesFromCache()
        {
            return _redisService.Get(SysCachingConsts.MenuCodesCacheKey, () =>
            {
                return _relationRepository.Where(x => x.Menu.Status == true).Select(x => new RoleMenuCodesDto() { }).Distinct().ToList();

            }, TimeSpan.FromSeconds(CachingConsts.OneYear));
        }

        /// <summary>
        /// 菜单所有数据
        /// </summary>
        /// <returns></returns>
        public List<MenuDto> GetAllMenusFromCache()
        {
            return _redisService.Get(SysCachingConsts.MenuListCacheKey, () =>
            {
                return _menuRepository.OrderBy(o => o.Ordinal).Select(x => new MenuDto
                {
                    Id = x.Id,
                    Code = x.Code,
                    Component = x.Component,
                    Hidden = x.Hidden,
                    Icon = x.Icon,
                    IsMenu = x.IsMenu,
                    IsOpen = x.IsOpen,
                    Levels = x.Levels,
                    Name = x.Name,
                    Ordinal = x.Ordinal,
                    PCode = x.PCode,
                    PCodes = x.PCodes,
                    Status = x.Status,
                    Tips = x.Tips,
                    Url = x.Url
                }).ToList();
            }, TimeSpan.FromSeconds(CachingConsts.OneYear));
        }

        /// <summary>
        /// 部门所有数据
        /// </summary>
        /// <returns></returns>
        public List<DeptDto> GetAllDeptsFromCache()
        {
            return _redisService.Get(SysCachingConsts.DetpListCacheKey, () =>
            {
                return _deptRepository.OrderBy(o => o.Ordinal).Select(x => new DeptDto
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Ordinal = x.Ordinal,
                    Pid = x.Pid,
                    Pids = x.Pids,
                    SimpleName = x.SimpleName,
                    Tips = x.Tips,
                    Version = x.Version
                }).ToList();
            }, TimeSpan.FromSeconds(CachingConsts.OneYear));
        }

        /// <summary>
        /// 用户详情缓存
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserValidateDto> GetUserValidateInfoFromCacheAsync(long id)
        {
            var cacheKey = ConcatCacheKey(SysCachingConsts.UserValidateInfoKeyPrefix, id.ToString());

            return await _redisService.GetAsync(cacheKey, async () =>
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
        }

        /// <summary>
        /// 保存用户登录信息
        /// </summary>
        /// <returns></returns>
        public async Task SetValidateInfoToCacheAsync(UserValidateDto value)
        {
            var cacheKey = ConcatCacheKey(SysCachingConsts.UserValidateInfoKeyPrefix, value.Id);
            await _redisService.SetAsync(cacheKey, value, TimeSpan.FromSeconds(CachingConsts.OneDay));
        }
    }
}
