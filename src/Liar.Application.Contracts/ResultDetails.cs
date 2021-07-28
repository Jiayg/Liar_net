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
    public sealed class ResultDetails
    {
        public ResultDetails()
        {
        }

        public ResultDetails(int status, bool success, object data, string msg = null)
        {
            Status = status;
            Msg = msg;
            Success = success;
            Data = data;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, SystemTextJsonHelper.GetAdncDefaultOptions());
        }

        [JsonPropertyName("msg")]
        public string Msg { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; } = 200;

        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("data")]
        public object Data { get; set; }

    }
}
