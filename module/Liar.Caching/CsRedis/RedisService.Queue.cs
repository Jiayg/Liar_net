using System;
using System.Collections.Generic;
using CSRedis;

namespace Liar.Caching.CsRedis
{
    public partial class RedisService
    {
        #region Pub/Sub
        /// <summary>
        /// 用于将信息发送到指定分区节点的频道，最终消息发布格式：1|message
        /// </summary>
        /// <param name="channel">频道名</param>
        /// <param name="message">消息文本</param>
        /// <returns></returns>
        public long Publish(string channel, string message) => Instance.Publish(channel, message);

        /// <summary>
        /// 用于将信息发送到指定分区节点的频道，与 Publish 方法不同，不返回消息id头，即 1|
        /// </summary>
        /// <param name="channel">频道名</param>
        /// <param name="message">消息文本</param>
        /// <returns></returns>
        public long PublishNoneMessageId(string channel, string message) => Instance.PublishNoneMessageId(channel, message);

        /// <summary>
        /// 查看所有订阅频道
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public string[] PubSubChannels(string pattern) => Instance.PubSubChannels(pattern);

        /// <summary>
        /// 查看所有模糊订阅端的数量<para></para>
        /// 注意：分区模式下，其他客户端的订阅可能不会返回
        /// </summary>
        /// <returns></returns>
        public long PubSubNumPat() => Instance.PubSubNumPat();

        /// <summary>
        /// 查看所有订阅端的数量<para></para>
        /// 注意：分区模式下，其他客户端的订阅可能不会返回
        /// </summary>
        /// <param name="channels">频道</param>
        /// <returns></returns>
        public Dictionary<string, long> PubSubNumSub(params string[] channels) => Instance.PubSubNumSub(channels);

        /// <summary>
        /// 订阅，根据分区规则返回SubscribeObject，Subscribe(("chan1", msg => Console.WriteLine(msg.Body)), ("chan2", msg => Console.WriteLine(msg.Body)))
        /// </summary>
        /// <param name="channels">频道和接收器</param>
        /// <returns>返回可停止订阅的对象</returns>
        public CSRedisClient.SubscribeObject Subscribe(params (string, Action<CSRedisClient.SubscribeMessageEventArgs>)[] channels) => Instance.Subscribe(channels);

        /// <summary>
        /// 模糊订阅，订阅所有分区节点(同条消息只处理一次），返回SubscribeObject，PSubscribe(new [] { "chan1*", "chan2*" }, msg => Console.WriteLine(msg.Body))
        /// </summary>
        /// <param name="channelPatterns">模糊频道</param>
        /// <param name="pmessage">接收器</param>
        /// <returns>返回可停止模糊订阅的对象</returns>
        public CSRedisClient.PSubscribeObject PSubscribe(string[] channelPatterns, Action<CSRedisClient.PSubscribePMessageEventArgs> pmessage) => Instance.PSubscribe(channelPatterns, pmessage);
        #endregion

        #region 使用列表实现订阅发布 lpush + blpop
        /// <summary>
        /// 使用lpush + blpop订阅端（多端非争抢模式），都可以收到消息
        /// </summary>
        /// <param name="listKey">list key（不含prefix前辍）</param>
        /// <param name="clientId">订阅端标识，若重复则争抢，若唯一必然收到消息</param>
        /// <param name="onMessage">接收消息委托</param>
        /// <returns></returns>
        public CSRedisClient.SubscribeListBroadcastObject SubscribeListBroadcast(string listKey, string clientId, Action<string> onMessage) => Instance.SubscribeListBroadcast(listKey, clientId, onMessage);

        /// <summary>
        /// 使用lpush + blpop订阅端（多端争抢模式），只有一端收到消息
        /// </summary>
        /// <param name="listKey">list key（不含prefix前辍）</param>
        /// <param name="onMessage">接收消息委托</param>
        /// <returns></returns>
        public CSRedisClient.SubscribeListObject SubscribeList(string listKey, Action<string> onMessage) => Instance.SubscribeList(listKey, onMessage);

        /// <summary>
        /// 使用lpush + blpop订阅端（多端争抢模式），只有一端收到消息
        /// </summary>
        /// <param name="listKeys">支持多个 key（不含prefix前辍）</param>
        /// <param name="onMessage">接收消息委托，参数1：key；参数2：消息体</param>
        /// <returns></returns>
        public CSRedisClient.SubscribeListObject SubscribeList(string[] listKeys, Action<string, string> onMessage) => Instance.SubscribeList(listKeys, onMessage);
        #endregion 
    }
}
