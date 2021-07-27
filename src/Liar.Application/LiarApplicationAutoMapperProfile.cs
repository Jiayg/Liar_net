using AutoMapper;
using Liar.Application.Contracts.Dtos;
using Liar.Domain.Entities;

namespace Liar
{
    public class LiarApplicationAutoMapperProfile : Profile
    {
        public LiarApplicationAutoMapperProfile()
        {
            CreateMap<Post, PostDto>(); 
        }
    }
}
