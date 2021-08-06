using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Liar.MongoDB.MongoEntities
{
    public class LoginLog : FullAuditedAggregateRoot<Guid>
    {
        public string Device { get; set; }

        public string Message { get; set; }

        public bool Succeed { get; set; }

        public int StatusCode { get; set; }

        public long? UserId { get; set; }

        public string Account { get; set; }

        public string UserName { get; set; }

        public string RemoteIpAddress { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
