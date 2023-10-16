using System.Text.Json.Serialization;

namespace ASignSdk.Responses;

public class ASignResponse
{
    public bool Success => Code == 100000;

    /// <summary>
    /// 响应码，100000表示成功，其他表示异常
    /// </summary>
    [JsonProperty("code")]
    [JsonPropertyName("code")]
    public int Code { get; set; }

    /// <summary>
    /// 响应信息
    /// </summary>
    [JsonProperty("msg")]
    [JsonPropertyName("msg")]
    public string Message { get; set; }
}

public class ASignResponse<TData> : ASignResponse
{
    /// <summary>
    /// 响应数据
    /// </summary>
    [JsonProperty("data")]
    [JsonPropertyName("data")]
    public TData Data { get; set; }
}