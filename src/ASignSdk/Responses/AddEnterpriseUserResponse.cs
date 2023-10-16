using System.Text.Json.Serialization;

namespace ASignSdk.Responses;

public class AddEnterpriseUserResponse : ASignResponse<AddEnterpriseUserResponseDto>
{

}

public class AddEnterpriseUserResponseDto
{
    /// <summary>
    /// 默认印章编号
    /// </summary>
    [JsonPropertyName("sealNo")]
    [JsonProperty("sealNo")]
    public string SealNo { get; set; }
}