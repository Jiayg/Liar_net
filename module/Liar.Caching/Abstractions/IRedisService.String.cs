using System;
using System.IO;
using System.Threading.Tasks;
using CSRedis;

namespace Liar.Caching.Abstractions
{
    public partial interface IRedisService
    {
        /// <summary>
        /// 如果 key 已经存在并且是一个字符串， APPEND 命令将指定的 value 追加到该 key 原来值（value）的末尾
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="value">字符串</param>
        /// <returns>追加指定值之后， key 中字符串的长度</returns>
        long Append(string key, object value);
        Task<long> AppendAsync(string key, object value);
        /// <summary>
        /// 计算给定位置被设置为 1 的比特位的数量
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="start">开始位置</param>
        /// <param name="end">结束位置</param>
        /// <returns></returns>
        long BitCount(string key, long start, long end);
        Task<long> BitCountAsync(string key, long start, long end);
        /// <summary>
        /// 对一个或多个保存二进制位的字符串 key 进行位元操作，并将结果保存到 destkey 上
        /// </summary>
        /// <param name="op">And | Or | XOr | Not</param>
        /// <param name="destKey">不含prefix前辍</param>
        /// <param name="keys">不含prefix前辍</param>
        /// <returns>保存到 destkey 的长度，和输入 key 中最长的长度相等</returns>
        long BitOp(RedisBitOp op, string destKey, params string[] keys);
        Task<long> BitOpAsync(RedisBitOp op, string destKey, params string[] keys);
        /// <summary>
        /// 对 key 所储存的值，查找范围内第一个被设置为1或者0的bit位
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="bit">查找值</param>
        /// <param name="start">开始位置，-1是最后一个，-2是倒数第二个</param>
        /// <param name="end">结果位置，-1是最后一个，-2是倒数第二个</param>
        /// <returns>返回范围内第一个被设置为1或者0的bit位</returns>
        long BitPos(string key, bool bit, long? start = null, long? end = null);
        Task<long> BitPosAsync(string key, bool bit, long? start = null, long? end = null);
        /// <summary>
        /// 获取指定 key 的值
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <returns></returns>
        string Get(string key);
        Task<string> GetAsync(string key);
        /// <summary>
        /// 获取指定 key 的值
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key">不含prefix前辍</param>
        /// <returns></returns>
        T Get<T>(string key);
        Task<T> GetAsync<T>(string key);

        /// <summary>
        /// 获取指定key的值 泛型同步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataRetriever"></param>
        /// <param name="expiration"></param>
        /// <returns></returns>
        T Get<T>(string key, Func<T> dataRetriever, TimeSpan expiration);

        /// <summary>
        /// 获取指定key的值 泛型异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataRetriever"></param>
        /// <param name="expiration"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key, Func<Task<T>> dataRetriever, TimeSpan expiration);

        /// <summary>
        /// 获取指定 key 的值（适用大对象返回）
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="destination">读取后写入目标流中</param>
        /// <param name="bufferSize">读取缓冲区</param>
        void Get(string key, Stream destination, int bufferSize = 1024);
        /// <summary>
        /// 对 key 所储存的值，获取指定偏移量上的位(bit)
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="offset">偏移量</param>
        /// <returns></returns>
        bool GetBit(string key, uint offset);
        Task<bool> GetBitAsync(string key, uint offset);
        /// <summary>
        /// 返回 key 中字符串值的子字符
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="end">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        string GetRange(string key, long start, long end);
        Task<string> GetRangeAsync(string key, long start, long end);
        /// <summary>
        /// 返回 key 中字符串值的子字符
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="end">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        T GetRange<T>(string key, long start, long end);
        Task<T> GetRangeAsync<T>(string key, long start, long end);
        /// <summary>
        /// 将给定 key 的值设为 value ，并返回 key 的旧值(old value)
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        string GetSet(string key, object value);
        Task<string> GetSetAsync(string key, object value);
        /// <summary>
        /// 将给定 key 的值设为 value ，并返回 key 的旧值(old value)
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        T GetSet<T>(string key, object value);
        Task<T> GetSetAsync<T>(string key, object value);
        /// <summary>
        /// 将 key 所储存的值加上给定的增量值（increment）
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="value">增量值(默认=1)</param>
        /// <returns></returns>
        long IncrBy(string key, long value = 1);
        Task<long> IncrByAsync(string key, long value = 1);
        /// <summary>
        /// 将 key 所储存的值加上给定的浮点增量值（increment）
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="value">增量值(默认=1)</param>
        /// <returns></returns>
        decimal IncrByFloat(string key, decimal value = 1);
        Task<decimal> IncrByFloatAsync(string key, decimal value = 1);
        /// <summary>
        /// 获取多个指定 key 的值(数组)
        /// </summary>
        /// <param name="keys">不含prefix前辍</param>
        /// <returns></returns>
        string[] MGet(params string[] keys);
        Task<string[]> MGetAsync(params string[] keys);
        /// <summary>
        /// 获取多个指定 key 的值(数组)
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="keys">不含prefix前辍</param>
        /// <returns></returns>
        T[] MGet<T>(params string[] keys);
        Task<T[]> MGetAsync<T>(params string[] keys);
        /// <summary>
        /// 同时设置一个或多个 key-value 对
        /// </summary>
        /// <param name="keyValues">key1 value1 [key2 value2]</param>
        /// <returns></returns>
        bool MSet(params object[] keyValues);
        Task<bool> MSetAsync(params object[] keyValues);
        /// <summary>
        /// 同时设置一个或多个 key-value 对，当且仅当所有给定 key 都不存在
        /// </summary>
        /// <param name="keyValues">key1 value1 [key2 value2]</param>
        /// <returns></returns>
        bool MSetNx(params object[] keyValues);
        Task<bool> MSetNxAsync(params object[] keyValues);
        /// <summary>
        /// 设置指定 key 的值,永不过期，所有写入参数object都支持string | byte[] | 数值 | 对象
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="value">值</param>
        /// <param name="expireSeconds">过期(秒单位)</param>
        /// <param name="exists">Nx, Xx</param>
        /// <returns></returns>
        bool Set(string key, object value, int expireSeconds = -1, RedisExistence? exists = null);
        Task<bool> SetAsync(string key, object value, int expireSeconds = -1, RedisExistence? exists = null);
        /// <summary>
        /// 设置指定 key 的值,带过期时间，所有写入参数object都支持string | byte[] | 数值 | 对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expire"></param>
        /// <param name="exists"></param>
        /// <returns></returns>
        bool Set(string key, object value, TimeSpan expire, RedisExistence? exists = null);
        Task<bool> SetAsync(string key, object value, TimeSpan expire, RedisExistence? exists = null);
        /// <summary>
        /// 对 key 所储存的字符串值，设置或清除指定偏移量上的位(bit)
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool SetBit(string key, uint offset, bool value);
        Task<bool> SetBitAsync(string key, uint offset, bool value);
        /// <summary>
        /// 只有在 key 不存在时设置 key 的值
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool SetNx(string key, object value);
        Task<bool> SetNxAsync(string key, object value);
        /// <summary>
        /// 用 value 参数覆写给定 key 所储存的字符串值，从偏移量 offset 开始
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">值</param>
        /// <returns>被修改后的字符串长度</returns>
        long SetRange(string key, uint offset, object value);
        Task<long> SetRangeAsync(string key, uint offset, object value);
        /// <summary>
        /// 返回 key 所储存的字符串值的长度
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <returns></returns>
        long StrLen(string key);
        Task<long> StrLenAsync(string key);
    }
}
