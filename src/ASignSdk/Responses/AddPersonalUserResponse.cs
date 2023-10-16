using System.Text.Json.Serialization;

namespace ASignSdk.Responses;

public class AddPersonalUserResponse : ASignResponse<AddPersonalUserResponseDto>
{

}

public class AddPersonalUserResponseDto
{
    /// <summary>
    /// 默认印章编号
    /// </summary>
    [JsonPropertyName("sealNo")]
    [JsonProperty("sealNo")]
    public string SealNo { get; set; }
}