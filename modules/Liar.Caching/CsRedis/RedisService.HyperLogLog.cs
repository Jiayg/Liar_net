namespace Liar.Caching.CsRedis
{
    public partial class RedisService
    {
        /// <summary>
        /// 添加指定元素到 HyperLogLog
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="elements">元素</param>
        /// <returns></returns>
        public bool PfAdd<T>(string key, params T[] elements) => Instance.PfAdd(key, elements);

        /// <summary>
        /// 返回给定 HyperLogLog 的基数估算值<para></para>
        /// 注意：分区模式下，若keys分散在多个分区节点时，将报错
        /// </summary>
        /// <param name="keys">不含prefix前辍</param>
        /// <returns></returns>
        public long PfCount(params string[] keys) => Instance.PfCount(keys);

        /// <summary>
        /// 将多个 HyperLogLog 合并为一个 HyperLogLog<para></para>
        /// 注意：分区模式下，若keys分散在多个分区节点时，将报错
        /// </summary>
        /// <param name="destKey">新的 HyperLogLog，不含prefix前辍</param>
        /// <param name="sourceKeys">源 HyperLogLog，不含prefix前辍</param>
        /// <returns></returns>
        public bool PfMerge(string destKey, params string[] sourceKeys) => Instance.PfMerge(destKey, sourceKeys);
    }
}
