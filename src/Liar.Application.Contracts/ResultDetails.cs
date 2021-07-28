using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Liar.Core.Helper;

namespace Liar.Application.Contracts
{
    /// <summary>
    /// 请求返回信息类
    /// </summary>
    [Serializable]
    public class ResultDetails<T>
    {
        [JsonPropertyName("msg")]
        public string Msg { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; } = 200;

        [JsonPropertyName("success")]
        public bool IsSuccess { get; set; }

        [JsonPropertyName("data")]
        public object Data { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public static ResultDetails<T> Success(string msg)
        {
            return Message(true, msg, default);
        }
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="response">数据</param>
        /// <returns></returns>
        public static ResultDetails<T> Success(string msg, T response)
        {
            return Message(true, msg, response);
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ResultDetails<T> Fail(string msg)
        {
            return Message(false, msg, default);
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static ResultDetails<T> Fail(string msg, T response)
        {
            return Message(false, msg, response);
        }

        /// <summary>
        /// 返回消息体
        /// </summary>
        /// <param name="success"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static ResultDetails<T> Message(bool success, string msg, T data)
        {
            return new ResultDetails<T>() { IsSuccess = success, Msg = msg, Data = data, };
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, SystemTextJsonHelper.GetAdncDefaultOptions());
        }

    }

    public class ResultDetails
    {
        [JsonPropertyName("msg")]
        public string Msg { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; } = 200;

        [JsonPropertyName("success")]
        public bool IsSuccess { get; set; }

        [JsonPropertyName("data")]
        public object Data { get; set; }
    }
}
