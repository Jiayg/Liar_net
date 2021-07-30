using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Liar.Application.Contracts.Dtos.Sys.Dept;
using Liar.Application.Contracts.Dtos.Sys.Menu;
using Liar.Application.Contracts.Dtos.Sys.Role;
using Liar.Application.Contracts.Dtos.Sys.User;
using Liar.Domain.Shared;
using Liar.Domain.Sys;

namespace Liar
{
    public class LiarApplicationAutoMapperProfile : Profile
    {
        public LiarApplicationAutoMapperProfile()
        {
            CreateMap<UserCreationDto, SysUser>();
            CreateMap<SysUser, UserProfileDto>();
            CreateMap<SysUser, UserValidateDto>();
            CreateMap<SysUser, UserDto>();

            CreateMap<SysDept, DeptCreationDto>();
            CreateMap<DeptCreationDto, SysDept>();
            CreateMap<DeptUpdationDto, SysDept>();

            CreateMap<SysMenu, MenuDto>();
            CreateMap<MenuCreationDto, SysMenu>();
            CreateMap<IOrderedQueryable<SysMenu>, List<MenuNodeDto>>();
            CreateMap<List<ZTreeNodeDto<long, dynamic>>, List<Node<long>>>();
            CreateMap<SysMenu, MenuRouterDto>();

            CreateMap<RoleCreationDto, SysRole>();
            CreateMap<SysRole, RoleDto>();
        }
    }
}
