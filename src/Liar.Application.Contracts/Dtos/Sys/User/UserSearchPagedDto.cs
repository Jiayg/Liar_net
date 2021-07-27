using Liar.Domain.Shared.Dtos;

namespace Liar.Application.Contracts.Dtos.Sys.User
{
    public class UserSearchPagedDto: SearchPagedDto
    {
        public string Name { get; set; }

        public string Account { get; set; }
    }
}
