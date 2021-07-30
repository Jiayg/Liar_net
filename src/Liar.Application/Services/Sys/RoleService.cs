using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Liar.Application.Contracts.Dtos.Sys.Menu;
using Liar.Application.Contracts.Dtos.Sys.Role;
using Liar.Application.Contracts.Dtos.Sys.User;
using Liar.Application.Contracts.IServices.Sys;
using Liar.Application.Contracts.ServiceResult;
using Liar.Core.Extensions;
using Liar.Core.Helper;
using Liar.Domain.Shared;
using Liar.Domain.Sys;
using Volo.Abp.Domain.Repositories;

namespace Liar.Application.Services.Sys
{
    public class RoleService : AppService, IRoleService
    {
        private readonly IRepository<SysRole> _roleRepository;
        private readonly IRepository<SysUser> _userRepository;
        private readonly IRepository<SysRelation> _relationRepostory;

        public RoleService(IRepository<SysRole> roleRepository, IRepository<SysUser> userRepository, IRepository<SysRelation> relationRepostory)
        {
            this._roleRepository = roleRepository;
            this._userRepository = userRepository;
            this._relationRepostory = relationRepostory;
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppSrvResult<long>> CreateAsync(RoleCreationDto input)
        {
            var isExists = _roleRepository.Where(x => x.Name == input.Name).Any();
            if (isExists)
                return Problem(HttpStatusCode.BadRequest, "该角色名称已经存在");

            var role = ObjectMapper.Map<RoleCreationDto, SysRole>(input);
            role.Id = IdGenerater.GetNextId();
            await _roleRepository.InsertAsync(role);

            return role.Id;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<AppSrvResult> DeleteAsync(long id)
        {
            if (id == 1600000000010)
                return Problem(HttpStatusCode.Forbidden, "禁止删除初始角色");

            if (await _userRepository.AnyAsync(x => x.RoleIds == id.ToString()))
                return Problem(HttpStatusCode.Forbidden, "有用户使用该角色，禁止删除");

            await _roleRepository.DeleteAsync(new SysRole() { Id = id });

            return AppSrvResult();
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageModelDto<RoleDto>> GetPagedAsync(RolePagedSearchDto input)
        {
            Expression<Func<SysRole, bool>> whereCondition = x => true;
            if (input.RoleName.IsNotNullOrEmpty())
            {
                whereCondition = whereCondition.And(x => x.Name.Contains(input.RoleName));
            }

            var query = _roleRepository.Where(whereCondition).Take(input.Limit, input.Offset).OrderBy(o => o.Ordinal).ToList();

            var result = new PageModelDto<RoleDto>()
            {
                Total = _roleRepository.Count(),
                Item = ObjectMapper.Map<List<SysRole>, List<RoleDto>>(query)
            };

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 获取用户拥有的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<RoleTreeDto> GetRoleTreeListByUserIdAsync(long userId)
        {
            RoleTreeDto result = null;
            IEnumerable<ZTreeNodeDto<long, dynamic>> treeNodes = null;

            var user = await _userRepository.GetAsync(x => x.Id == userId);

            if (user == null)
                return null;

            var roles = await _roleRepository.GetListAsync();
            var roleIds = user.RoleIds?.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)) ?? new List<long>();
            if (roles.Any())
            {
                treeNodes = roles.Select(x => new ZTreeNodeDto<long, dynamic>
                {
                    Id = x.Id,
                    PID = x.Pid.HasValue ? x.Pid.Value : 0,
                    Name = x.Name,
                    Open = x.Pid.HasValue && x.Pid.Value > 0 ? false : true,
                    Checked = roleIds.Contains(x.Id)
                });

                result = new RoleTreeDto
                {
                    TreeData = treeNodes.Select(x => new Node<long>
                    {
                        Id = x.Id,
                        PID = x.PID,
                        Name = x.Name,
                        Checked = x.Checked
                    }),
                    CheckedIds = treeNodes.Where(x => x.Checked).Select(x => x.Id)
                };
            }

            return result;
        }

        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppSrvResult> SetPermissonsAsync(RoleSetPermissonsDto input)
        {
            if (input.RoleId == 1600000000010)
                return Problem(HttpStatusCode.Forbidden, "禁止设置初始角色");

            await _relationRepostory.DeleteAsync(x => x.RoleId == input.RoleId);

            var relations = new List<SysRelation>();
            foreach (var permissionId in input.Permissions)
            {
                relations.Add(
                    new SysRelation
                    {
                        Id = IdGenerater.GetNextId(),
                        RoleId = input.RoleId,
                        MenuId = permissionId
                    }
                );
            }
            await _relationRepostory.InsertManyAsync(relations);

            return AppSrvResult();
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppSrvResult> UpdateAsync(long id, RoleUpdationDto input)
        {
            var isExists = _roleRepository.Where(x => x.Name == input.Name && x.Id != id).Any();
            if (isExists)
                return Problem(HttpStatusCode.BadRequest, "该角色名称已经存在");

            var role = ObjectMapper.Map<RoleCreationDto, SysRole>(input);

            role.Id = id;

            await _roleRepository.UpdateAsync(role);

            return AppSrvResult();
        }
    }
}
