namespace Liar.Domain.Shared.ConfigModels
{
    public class MysqlConfig
    {
        /// <summary>
        /// 主库
        /// </summary>
        public string MainConnectionString { get; set; }

        /// <summary>
        /// 日志库
        /// </summary>
        public string LogConnectionString { get; set; }
    }
}
