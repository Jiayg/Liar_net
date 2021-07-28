namespace Liar.Domain.Shared.UserContext
{
    public interface IUserContext
    {
        long Id { get; set; }
        string Account { get; set; }
        string Name { get; set; }
        string RemoteIpAddress { get; set; }
        string Device { get; set; }
        string Email { get; set; }
        long[] RoleIds { get; set; }
    }

    public class UserContext : IUserContext
    {
        public long Id { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string RemoteIpAddress { get; set; }
        public string Device { get; set; }
        public string Email { get; set; }
        public long[] RoleIds { get; set; }
    }
}
