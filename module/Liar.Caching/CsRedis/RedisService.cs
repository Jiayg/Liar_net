using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CSRedis;
using Liar.Caching.Abstractions;

namespace Liar.Caching.CsRedis
{
    public partial class RedisService : IRedisService
    {
        private CSRedisClient Instance;

        public RedisService(CSRedisClient Instance)
        {
            this.Instance = Instance;
        }

        #region 服务器命令
        /// <summary>
        /// 在所有分区节点上，执行服务器命令
        /// </summary>
        public CSRedisClient.NodesServerManagerProvider NodesServerManager => Instance.NodesServerManager;

        /// <summary>
        /// 在指定分区节点上，执行服务器命令
        /// </summary>
        /// <param name="node">节点</param>
        /// <returns></returns>
        public CSRedisClient.NodeServerManagerProvider NodeServerManager(string node) => Instance.NodeServerManager(node);
        #endregion

        #region 连接命令
        /// <summary>
        /// 打印字符串
        /// </summary>
        /// <param name="nodeKey">分区key</param>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public string Echo(string nodeKey, string message) => Instance.Echo(nodeKey, message);

        /// <summary>
        /// 打印字符串
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public string Echo(string message) => Instance.Echo(message);

        /// <summary>
        /// 查看服务是否运行
        /// </summary>
        /// <param name="nodeKey">分区key</param>
        /// <returns></returns>
        public bool Ping(string nodeKey) => Instance.Ping(nodeKey);

        /// <summary>
        /// 查看服务是否运行
        /// </summary>
        /// <returns></returns>
        public bool Ping() => Instance.Ping();
        #endregion

        /// <summary> 
        /// 开启分布式锁，若超时返回null
        /// </summary>
        /// <param name="name">锁名称</param>
        /// <param name="timeoutSeconds">超时（秒）</param>
        /// <param name="autoDelay">自动延长锁超时时间，看门狗线程的超时时间为timeoutSeconds/2 ， 在看门狗线程超时时间时自动延长锁的时间为timeoutSeconds。除非程序意外退出，否则永不超时。</param>
        /// <returns></returns>
        public CSRedisClientLock Lock(string name, int timeoutSeconds, bool autoDelay = true) => Instance.Lock(name, timeoutSeconds);
    }
}
