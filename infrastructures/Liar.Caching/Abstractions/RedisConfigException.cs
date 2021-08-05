using System;

namespace Liar.Caching.Abstractions
{
    public class RedisConfigException : Exception
    {
        public RedisConfigException() : base()
        {
        }

        public RedisConfigException(string message) : base(message)
        {
        }

        public RedisConfigException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
