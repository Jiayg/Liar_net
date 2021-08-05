using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CSRedis;

namespace Liar.Caching.CsRedis
{
    /// <summary>
    /// string
    /// </summary>
    public partial class RedisService
    {
        /// <summary>
        /// 如果 key 已经存在并且是一个字符串， APPEND 命令将指定的 value 追加到该 key 原来值（value）的末尾
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="value">字符串</param>
        /// <returns>追加指定值之后， key 中字符串的长度</returns>
        public long Append(string key, object value) => Instance.Append(key, value);
        public async Task<long> AppendAsync(string key, object value) => await Instance.AppendAsync(key, value);

        /// <summary>
        /// 计算给定位置被设置为 1 的比特位的数量
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="start">开始位置</param>
        /// <param name="end">结束位置</param>
        /// <returns></returns>
        public long BitCount(string key, long start, long end) => Instance.BitCount(key, start, end);
        public async Task<long> BitCountAsync(string key, long start, long end) => await Instance.BitCountAsync(key, start, end);

        /// <summary>
        /// 对一个或多个保存二进制位的字符串 key 进行位元操作，并将结果保存到 destkey 上
        /// </summary>
        /// <param name="op">And | Or | XOr | Not</param>
        /// <param name="destKey">不含prefix前辍</param>
        /// <param name="keys">不含prefix前辍</param>
        /// <returns>保存到 destkey 的长度，和输入 key 中最长的长度相等</returns>
        public long BitOp(RedisBitOp op, string destKey, params string[] keys) => Instance.BitOp(op, destKey, keys);
        public async Task<long> BitOpAsync(RedisBitOp op, string destKey, params string[] keys) => await Instance.BitOpAsync(op, destKey, keys);

        /// <summary>
        /// 对 key 所储存的值，查找范围内第一个被设置为1或者0的bit位
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="bit">查找值</param>
        /// <param name="start">开始位置，-1是最后一个，-2是倒数第二个</param>
        /// <param name="end">结果位置，-1是最后一个，-2是倒数第二个</param>
        /// <returns>返回范围内第一个被设置为1或者0的bit位</returns>
        public long BitPos(string key, bool bit, long? start = null, long? end = null) => Instance.BitPos(key, bit, start, end);
        public async Task<long> BitPosAsync(string key, bool bit, long? start = null, long? end = null) => await Instance.BitPosAsync(key, bit, start, end);

        /// <summary>
        /// 获取指定 key 的值
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <returns></returns>
        public string Get(string key) => Instance.Get(key);
        public async Task<string> GetAsync(string key) => await Instance.GetAsync(key);

        /// <summary>
        /// 获取指定 key 的值
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key">不含prefix前辍</param>
        /// <returns></returns>
        public T Get<T>(string key) => Instance.Get<T>(key);
        public async Task<T> GetAsync<T>(string key) => await Instance.GetAsync<T>(key);

        /// <summary>
        /// 获取指定key的值 泛型同步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="dataRetriever"></param>
        /// <param name="expiration"></param>
        /// <returns></returns>
        public T Get<T>(string cacheKey, Func<T> dataRetriever, TimeSpan expiration)
        {
            var result = Instance.Get<T>(cacheKey);
            if (result != null)
                return result;

            try
            {
                var item = dataRetriever();
                if (item != null)
                {
                    Instance.Set(cacheKey, item, expiration);
                    return item;
                }
                else
                {
                    return default;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        /// <summary>
        /// 获取指定key的值 泛型异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="dataRetriever"></param>
        /// <param name="expiration"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string cacheKey, Func<Task<T>> dataRetriever, TimeSpan expiration)
        {
            var result = await Instance.GetAsync<T>(cacheKey);
            if (result != null)
                return result;

            try
            {
                var item = await dataRetriever();
                if (item != null)
                {
                    await Instance.SetAsync(cacheKey, item, expiration);
                    return item;
                }
                else
                {
                    return default;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取指定 key 的值（适用大对象返回）
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="destination">读取后写入目标流中</param>
        /// <param name="bufferSize">读取缓冲区</param>
        public void Get(string key, Stream destination, int bufferSize = 1024) => Instance.Get(key, destination, bufferSize);

        /// <summary>
        /// 对 key 所储存的值，获取指定偏移量上的位(bit)
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="offset">偏移量</param>
        /// <returns></returns>
        public bool GetBit(string key, uint offset) => Instance.GetBit(key, offset);
        public async Task<bool> GetBitAsync(string key, uint offset) => await Instance.GetBitAsync(key, offset);

        /// <summary>
        /// 返回 key 中字符串值的子字符
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="end">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        public string GetRange(string key, long start, long end) => Instance.GetRange(key, start, end);
        public async Task<string> GetRangeAsync(string key, long start, long end) => await Instance.GetRangeAsync(key, start, end);

        /// <summary>
        /// 返回 key 中字符串值的子字符
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="end">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        public T GetRange<T>(string key, long start, long end) => Instance.GetRange<T>(key, start, end);
        public async Task<T> GetRangeAsync<T>(string key, long start, long end) => await Instance.GetRangeAsync<T>(key, start, end);

        /// <summary>
        /// 将给定 key 的值设为 value ，并返回 key 的旧值(old value)
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public string GetSet(string key, object value) => Instance.GetSet(key, value);
        public async Task<string> GetSetAsync(string key, object value) => await Instance.GetSetAsync(key, value);

        /// <summary>
        /// 将给定 key 的值设为 value ，并返回 key 的旧值(old value)
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public T GetSet<T>(string key, object value) => Instance.GetSet<T>(key, value);
        public async Task<T> GetSetAsync<T>(string key, object value) => await Instance.GetSetAsync<T>(key, value);

        /// <summary>
        /// 将 key 所储存的值加上给定的增量值（increment）
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="value">增量值(默认=1)</param>
        /// <returns></returns>
        public long IncrBy(string key, long value = 1) => Instance.IncrBy(key, value);
        public async Task<long> IncrByAsync(string key, long value = 1) => await Instance.IncrByAsync(key, value);

        /// <summary>
        /// 将 key 所储存的值加上给定的浮点增量值（increment）
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="value">增量值(默认=1)</param>
        /// <returns></returns>
        public decimal IncrByFloat(string key, decimal value = 1) => Instance.IncrByFloat(key, value);
        public async Task<decimal> IncrByFloatAsync(string key, decimal value = 1) => await Instance.IncrByFloatAsync(key, value);

        /// <summary>
        /// 获取多个指定 key 的值(数组)
        /// </summary>
        /// <param name="keys">不含prefix前辍</param>
        /// <returns></returns>
        public string[] MGet(params string[] keys) => Instance.MGet(keys);
        public async Task<string[]> MGetAsync(params string[] keys) => await Instance.MGetAsync(keys);

        /// <summary>
        /// 获取多个指定 key 的值(数组)
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="keys">不含prefix前辍</param>
        /// <returns></returns>
        public T[] MGet<T>(params string[] keys) => Instance.MGet<T>(keys);
        public async Task<T[]> MGetAsync<T>(params string[] keys) => await Instance.MGetAsync<T>(keys);

        /// <summary>
        /// 同时设置一个或多个 key-value 对
        /// </summary>
        /// <param name="keyValues">key1 value1 [key2 value2]</param>
        /// <returns></returns>
        public bool MSet(params object[] keyValues) => Instance.MSet(keyValues);
        public async Task<bool> MSetAsync(params object[] keyValues) => await Instance.MSetAsync(keyValues);

        /// <summary>
        /// 同时设置一个或多个 key-value 对，当且仅当所有给定 key 都不存在
        /// </summary>
        /// <param name="keyValues">key1 value1 [key2 value2]</param>
        /// <returns></returns>
        public bool MSetNx(params object[] keyValues) => Instance.MSetNx(keyValues);
        public async Task<bool> MSetNxAsync(params object[] keyValues) => await Instance.MSetNxAsync(keyValues);

        /// <summary>
        /// 设置指定 key 的值,永不过期，所有写入参数object都支持string | byte[] | 数值 | 对象
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="value">值</param>
        /// <param name="expireSeconds">过期(秒单位)</param>
        /// <param name="exists">Nx, Xx</param>
        /// <returns></returns>
        public bool Set(string key, object value, int expireSeconds = -1, RedisExistence? exists = null) => Instance.Set(key, value, expireSeconds, exists);
        public async Task<bool> SetAsync(string key, object value, int expireSeconds = -1, RedisExistence? exists = null) => await Instance.SetAsync(key, value, expireSeconds, exists);

        /// <summary>
        /// 设置指定 key 的值,带过期时间，所有写入参数object都支持string | byte[] | 数值 | 对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expire"></param>
        /// <param name="exists"></param>
        /// <returns></returns>
        public bool Set(string key, object value, TimeSpan expire, RedisExistence? exists = null) => Instance.Set(key, value, expire, exists);
        public async Task<bool> SetAsync(string key, object value, TimeSpan expire, RedisExistence? exists = null) => await Instance.SetAsync(key, value, expire, exists);

        /// <summary>
        /// 对 key 所储存的字符串值，设置或清除指定偏移量上的位(bit)
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool SetBit(string key, uint offset, bool value) => Instance.SetBit(key, offset, value);
        public async Task<bool> SetBitAsync(string key, uint offset, bool value) => await Instance.SetBitAsync(key, offset, value);

        /// <summary>
        /// 只有在 key 不存在时设置 key 的值
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool SetNx(string key, object value) => Instance.SetNx(key, value);
        public async Task<bool> SetNxAsync(string key, object value) => await Instance.SetNxAsync(key, value);

        /// <summary>
        /// 用 value 参数覆写给定 key 所储存的字符串值，从偏移量 offset 开始
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">值</param>
        /// <returns>被修改后的字符串长度</returns>
        public long SetRange(string key, uint offset, object value) => Instance.SetRange(key, offset, value);
        public async Task<long> SetRangeAsync(string key, uint offset, object value) => await Instance.SetRangeAsync(key, offset, value);

        /// <summary>
        /// 返回 key 所储存的字符串值的长度
        /// </summary>
        /// <param name="key">不含prefix前辍</param>
        /// <returns></returns>
        public long StrLen(string key) => Instance.StrLen(key);
        public async Task<long> StrLenAsync(string key) => await Instance.StrLenAsync(key);

    }
}
