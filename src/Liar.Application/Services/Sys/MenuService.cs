using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Liar.Application.Contracts.Dtos.Sys.Menu;
using Liar.Application.Contracts.IServices.Sys;
using Liar.Application.Contracts.ServiceResult;
using Liar.Core.Helper;
using Liar.Domain.Sys;
using Volo.Abp.Domain.Repositories;

namespace Liar.Application.Services.Sys
{
    public class MenuService : AppService, IMenuService
    {
        private readonly IRepository<SysMenu> _menuRepository;
        private readonly IRepository<SysRelation> _relationRepository;

        public MenuService(IRepository<SysMenu> menuRepository, IRepository<SysRelation> relationRepository)
        {
            this._menuRepository = menuRepository;
            this._relationRepository = relationRepository;
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppSrvResult<long>> CreateAsync(MenuCreationDto input)
        {
            var allMenus = await _menuRepository.GetListAsync();

            var isExistsCode = allMenus.Any(x => x.Code == input.Code);
            if (isExistsCode)
                return Problem(HttpStatusCode.Forbidden, "该菜单编码已经存在");

            var isExistsName = allMenus.Any(x => x.Name == input.Name);
            if (isExistsName)
                return Problem(HttpStatusCode.Forbidden, "该菜单名称已经存在");

            var parentMenu = ObjectMapper.Map<SysMenu, MenuDto>(allMenus.FirstOrDefault(x => x.Code == input.PCode));

            var addDto = ProducePCodes(input, parentMenu);
            var menu = ObjectMapper.Map<MenuCreationDto, SysMenu>(addDto);
            menu.Id = IdGenerater.GetNextId();
            await _menuRepository.InsertAsync(menu);

            return menu.Id;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppSrvResult> DeleteAsync(long id)
        {
            await _menuRepository.DeleteAsync(x => x.Id == id);

            return AppSrvResult();
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenuNodeDto>> GetlistAsync()
        {
            var result = new List<MenuNodeDto>();

            var menus = _menuRepository.OrderBy(o => o.Levels).ThenBy(x => x.Ordinal);

            var menuNodes = ObjectMapper.Map<IOrderedQueryable<SysMenu>, List<MenuNodeDto>>(menus);
            foreach (var node in menuNodes)
            {
                var parentNode = menuNodes.FirstOrDefault(x => x.Code == node.PCode);
                if (parentNode != null)
                {
                    node.ParentId = parentNode.Id;
                }
            }

            var dictNodes = menuNodes.ToDictionary(x => x.Id);
            foreach (var pair in dictNodes)
            {
                var currentNode = pair.Value;
                if (currentNode.ParentId.HasValue && dictNodes.ContainsKey(currentNode.ParentId.Value))
                {
                    dictNodes[currentNode.ParentId.Value].Children.Add(currentNode);
                }
                else
                {
                    result.Add(currentNode);
                }
            }

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 获取左侧路由菜单
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public async Task<List<MenuRouterDto>> GetMenusForRouterAsync(IEnumerable<long> roleIds)
        {
            var result = new List<MenuRouterDto>();
            //所有菜单
            var allMenus = await _menuRepository.GetListAsync();
            //所有菜单角色关系
            var allRelations = await _relationRepository.GetListAsync();
            //角色拥有的菜单Ids
            var menusIds = allRelations.Where(x => roleIds.Contains(x.RoleId)).Select(x => x.MenuId).Distinct();
            //更加菜单Id获取菜单实体
            var menus = allMenus.Where(x => menusIds.Contains(x.Id));

            if (menus.Any())
            {
                var routerMenus = new List<MenuRouterDto>();

                var componentMenus = menus.Where(x => !string.IsNullOrWhiteSpace(x.Component));
                foreach (var menu in componentMenus)
                {
                    var routerMenu = ObjectMapper.Map<SysMenu, MenuRouterDto>(menu);
                    routerMenu.Path = menu.Url;
                    routerMenu.Meta = new MenuMetaDto
                    {
                        Icon = menu.Icon,
                        Title = menu.Code
                    };
                    routerMenus.Add(routerMenu);
                }

                foreach (var node in routerMenus)
                {
                    var parentNode = routerMenus.FirstOrDefault(x => x.Code == node.PCode);
                    if (parentNode != null)
                    {
                        node.ParentId = parentNode.Id;
                    }
                }

                var dictNodes = routerMenus.ToDictionary(x => x.Id);
                foreach (var pair in dictNodes.OrderBy(x => x.Value.Ordinal))
                {
                    var currentNode = pair.Value;
                    if (currentNode.ParentId.HasValue && dictNodes.ContainsKey(currentNode.ParentId.Value))
                    {
                        dictNodes[currentNode.ParentId.Value].Children.Add(currentNode);
                    }
                    else
                    {
                        result.Add(currentNode);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 获取指定角色的菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<MenuTreeDto> GetMenuTreeListByRoleIdAsync(long roleId)
        {
            var menuIds = _relationRepository.Where(x => x.RoleId == roleId).Select(s => s.MenuId).ToList();

            var roleTreeList = new List<ZTreeNodeDto<long, dynamic>>();

            var menus = _menuRepository.Where(w => true).OrderBy(o => o.Ordinal);

            foreach (var menu in menus)
            {
                var parentMenu = menus.FirstOrDefault(x => x.Code == menu.PCode);
                var node = new ZTreeNodeDto<long, dynamic>
                {
                    Id = menu.Id,
                    PID = parentMenu != null ? parentMenu.Id : 0,
                    Name = menu.Name,
                    Open = parentMenu != null,
                    Checked = menuIds.Contains(menu.Id)
                };
                roleTreeList.Add(node);
            }

            var nodes = ObjectMapper.Map<List<ZTreeNodeDto<long, dynamic>>, List<Node<long>>>(roleTreeList);
            foreach (var node in nodes)
            {
                foreach (var child in nodes)
                {
                    if (child.PID == node.Id)
                    {
                        node.Children.Add(child);
                    }
                }
            }

            var groups = roleTreeList.GroupBy(x => x.PID).Where(x => x.Key > 1);
            foreach (var group in groups)
            {
                roleTreeList.RemoveAll(x => x.Id == group.Key);
            }

            return await Task.FromResult(new MenuTreeDto
            {
                TreeData = nodes.Where(x => x.PID == 0),
                CheckedIds = roleTreeList.Where(x => x.Checked && x.PID != 0).Select(x => x.Id)
            });
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AppSrvResult> UpdateAsync(long id, MenuUpdationDto input)
        {
            var allMenus = await _menuRepository.GetListAsync();

            var isExistsCode = allMenus.Any(x => x.Code == input.Code && x.Id != id);
            if (isExistsCode)
                return Problem(HttpStatusCode.BadRequest, "该菜单编码已经存在");

            var isExistsName = allMenus.Any(x => x.Name == input.Name && x.Id != id);
            if (isExistsName)
                return Problem(HttpStatusCode.BadRequest, "该菜单名称已经存在");

            var parentMenu = ObjectMapper.Map<SysMenu, MenuDto>(allMenus.FirstOrDefault(x => x.Code == input.PCode));
            var updateDto = ProducePCodes(input, parentMenu);
            var menu = ObjectMapper.Map<MenuCreationDto, SysMenu>(updateDto);

            menu.Id = id;

            await _menuRepository.UpdateAsync(menu);

            return AppSrvResult();
        }



        private MenuCreationDto ProducePCodes(MenuCreationDto saveDto, MenuDto parentMenuDto)
        {
            if (saveDto.PCode.IsNullOrWhiteSpace() || saveDto.PCode.EqualsIgnoreCase("0"))
            {
                saveDto.PCode = "0";
                saveDto.PCodes = "[0],";
                saveDto.Levels = 1;
                return saveDto;
            }

            saveDto.Levels = parentMenuDto.Levels + 1;
            saveDto.PCodes = $"{parentMenuDto.PCodes}[{parentMenuDto.Code}]";

            return saveDto;
        }
    }
}
