namespace Liar.Caching.CsRedis
{
    public partial class RedisService
    {
        /// <summary>
        /// 执行脚本
        /// </summary>
        /// <param name="script">Lua 脚本</param>
        /// <param name="key">用于定位分区节点，不含prefix前辍</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public object Eval(string script, string key, params object[] args) => Instance.Eval(script, key, args);

        /// <summary>
        /// 执行脚本
        /// </summary>
        /// <param name="sha1">脚本缓存的sha1</param>
        /// <param name="key">用于定位分区节点，不含prefix前辍</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public object EvalSHA(string sha1, string key, params object[] args) => Instance.EvalSHA(sha1, key, args);

        /// <summary>
        /// 校验所有分区节点中，脚本是否已经缓存。任何分区节点未缓存sha1，都返回false。
        /// </summary>
        /// <param name="sha1">脚本缓存的sha1</param>
        /// <returns></returns>
        public bool[] ScriptExists(params string[] sha1) => Instance.ScriptExists(sha1);

        /// <summary>
        /// 清除所有分区节点中，所有 Lua 脚本缓存
        /// </summary>
        public void ScriptFlush() => Instance.ScriptFlush();

        /// <summary>
        /// 杀死所有分区节点中，当前正在运行的 Lua 脚本
        /// </summary>
        public void ScriptKill() => Instance.ScriptKill();

        /// <summary>
        /// 在所有分区节点中，缓存脚本后返回 sha1（同样的脚本在任何服务器，缓存后的 sha1 都是相同的）
        /// </summary>
        /// <param name="script">Lua 脚本</param>
        /// <returns></returns>
        public string ScriptLoad(string script) => Instance.ScriptLoad(script);
    }
}
