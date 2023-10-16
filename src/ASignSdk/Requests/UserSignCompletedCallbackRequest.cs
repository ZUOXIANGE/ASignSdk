using System.Text.Json.Serialization;

namespace ASignSdk.Requests;

/// <summary>
/// 某个用户签署完成回调
/// </summary>
public class UserSignCompletedCallbackRequest : SignCallbackRequest
{
    /// <summary>
    /// 用户唯一识别号
    /// </summary>
    [JsonProperty("account")]
    [JsonPropertyName("account")]
    public string Account { get; set; }

    /// <summary>
    /// 合同号
    /// </summary>
    [JsonProperty("contractNo")]
    [JsonPropertyName("contractNo")]
    public string ContractNo { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [JsonProperty("status")]
    [JsonPropertyName("status")]
    public string Status { get; set; }

    /// <summary>
    /// 签署时间
    /// </summary>
    [JsonProperty("signTime")]
    [JsonPropertyName("signTime")]
    public string SignTime { get; set; }

    /// <summary>
    /// 有效时间
    /// </summary>
    [JsonProperty("validityTime")]
    [JsonPropertyName("validityTime")]
    public string ValidityTime { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [JsonProperty("remark")]
    [JsonPropertyName("remark")]
    public string Remark { get; set; }
}