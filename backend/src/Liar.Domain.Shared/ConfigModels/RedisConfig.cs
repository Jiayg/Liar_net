using System.Collections.Generic;

namespace Liar.Domain.Shared.ConfigModels
{
    public class RedisConfig
    {
        public List<RedisClientOptions> Clients { get; set; }
    }
    public class RedisClientOptions
    {
        public string Name { get; set; }

        public string[] ConnectionStrings { get; set; }
    }

}
