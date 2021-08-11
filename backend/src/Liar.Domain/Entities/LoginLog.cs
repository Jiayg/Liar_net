using System;

namespace Liar.Domain.Entities
{
    public class LoginLog
    {
        public string Account { get; set; }
        public bool Succeed { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreateTime { get; set; }
        public string Device { get; set; }
        public string RemoteIpAddress { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
