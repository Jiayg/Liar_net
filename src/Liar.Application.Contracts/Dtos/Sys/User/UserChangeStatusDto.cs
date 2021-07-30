using Liar.Domain.Shared.Dtos;

namespace Liar.Application.Contracts.Dtos.Sys.User
{
    public class UserChangeStatusDto : IDto
    {
        public long[] UserIds { get; set; }

        public int Status { get; set; }
    }
}
