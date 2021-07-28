using System;
using Liar.Domain.Shared.Dtos;

namespace Liar.Application.Contracts.Dtos.Sys.User
{
    [Serializable]
    public class UserValidateDto : OutputDto
    {
        public override long Id { get; set; }

        public string Account { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string RoleIds { get; set; }

        public string Salt { get; set; }

        public string Password { get; set; }

        public int Status { get; set; }
    }
}
