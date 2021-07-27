using AutoMapper;
using Liar.Application.Contracts.Dtos.Sys.User;
using Liar.Domain.Sys;

namespace Liar
{
    public class LiarApplicationAutoMapperProfile : Profile
    {
        public LiarApplicationAutoMapperProfile()
        {
            CreateMap<UserCreationDto, SysUser>();

            CreateMap<SysUser, UserProfileDto>();
        }
    }
}
