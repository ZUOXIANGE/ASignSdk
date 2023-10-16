using System.Text.Json.Serialization;

namespace ASignSdk.Requests;

public class SignCallbackRequest
{
    /// <summary>
    /// 动作
    /// </summary>
    [JsonProperty("action")]
    [JsonPropertyName("action")]
    public string Action { get; set; }

    /// <summary>
    /// 时间戳
    /// </summary>
    [JsonProperty("timestamp")]
    [JsonPropertyName("timestamp")]
    public string Timestamp { get; set; }

    /// <summary>
    /// 签名
    /// </summary>
    [JsonProperty("sign")]
    [JsonPropertyName("sign")]
    public string Sign { get; set; }

}