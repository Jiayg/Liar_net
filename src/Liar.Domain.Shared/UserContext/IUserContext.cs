namespace Liar.Domain.Shared.UserContext
{
    public interface IUserContext
    {
        string Id { get; set; }
        string Account { get; set; }
        string Name { get; set; }
        string RemoteIpAddress { get; set; }
        string Device { get; set; }
        string Email { get; set; }
        long[] RoleIds { get; set; }
    }

    public class UserContext : IUserContext
    {
        public string Id { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string RemoteIpAddress { get; set; }
        public string Device { get; set; }
        public string Email { get; set; }
        public long[] RoleIds { get; set; }
    }
}
