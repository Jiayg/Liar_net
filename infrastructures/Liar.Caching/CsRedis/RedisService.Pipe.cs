using System;
using CSRedis;

namespace Liar.Caching.CsRedis
{
    public partial class RedisService
    {
        /// <summary>
        /// 创建管道传输
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public object[] StartPipe(Action<CSRedisClientPipe<string>> handler) => Instance.StartPipe(handler);

        /// <summary>
        /// 创建管道传输，打包提交如：RedisHelper.StartPipe().Set("a", "1").HSet("b", "f", "2").EndPipe();
        /// </summary>
        /// <returns></returns>
        public CSRedisClientPipe<string> StartPipe() => Instance.StartPipe();
    }
}
