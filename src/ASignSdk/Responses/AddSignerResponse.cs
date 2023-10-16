using System.Text.Json.Serialization;

namespace ASignSdk.Responses;

public class AddSignerResponse : ASignResponse<AddSignerResponseDto>
{

}

public class AddSignerResponseDto
{
    /// <summary>
    /// 合同编号
    /// </summary>
    [JsonProperty("contractNo")]
    [JsonPropertyName("contractNo")]
    public string ContractNo { get; set; }

    /// <summary>
    /// 合同名称
    /// </summary>
    [JsonProperty("contractName")]
    [JsonPropertyName("contractName")]
    public string ContractName { get; set; }

    /// <summary>
    /// 合同有效期
    /// </summary>
    [JsonProperty("validityTime")]
    [JsonPropertyName("validityTime")]
    public string ValidityTime { get; set; }

    /// <summary>
    /// 合同预览链接
    /// </summary>
    [JsonProperty("previewUrl")]
    [JsonPropertyName("previewUrl")]
    public string PreviewUrl { get; set; }

    /// <summary>
    /// 合同用户信息
    /// </summary>
    [JsonProperty("signUser")]
    [JsonPropertyName("signUser")]
    public List<SignUserDto> SignUser { get; set; }
}

public class SignUserDto
{
    /// <summary>
    /// 用户唯一识别码
    /// </summary>
    [JsonProperty("account")]
    [JsonPropertyName("account")]
    public string Account { get; set; }

    /// <summary>
    /// 合同签署链接
    /// </summary>
    [JsonProperty("signUrl")]
    [JsonPropertyName("signUrl")]
    public string SignUrl { get; set; }

    /// <summary>
    /// 密码签署链接
    /// </summary>
    [JsonProperty("pwdSignUrl")]
    [JsonPropertyName("pwdSignUrl")]
    public string PasswordSignUrl { get; set; }

    /// <summary>
    /// 顺序签约的序号
    /// </summary>
    [JsonProperty("signOrder")]
    [JsonPropertyName("signOrder")]
    public int? SignOrder { get; set; }

    /// <summary>
    /// 用户姓名
    /// </summary>
    [JsonProperty("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// 用户身份证
    /// </summary>
    [JsonProperty("idCard")]
    [JsonPropertyName("idCard")]
    public string IdCard { get; set; }
}