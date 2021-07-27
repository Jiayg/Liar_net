using AutoMapper;
using Volo.Abp.Application.Services;

namespace Liar.Application.Contracts
{
    public interface ILiarAppService : IApplicationService
    {
        IObjectMapper Mapper { get; set; }
    }
}
