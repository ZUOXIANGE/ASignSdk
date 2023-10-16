using System.Text.Json.Serialization;

namespace ASignSdk.Responses;

public class CreateContractResponse : ASignResponse<CreateContractResponseDto>
{

}

public class CreateContractResponseDto
{
    /// <summary>
    /// 合同预览链接（预览链接3小时内有效，过期后可通过查询合同信息接口重新获取）
    /// </summary>
    [JsonProperty("previewUrl")]
    [JsonPropertyName("previewUrl")]
    public string PreviewUrl { get; set; }

    /// <summary>
    /// 合同文件信息（文件名称，附件编号，页数）
    /// </summary>
    [JsonProperty("contractFiles")]
    [JsonPropertyName("contractFiles")]
    public List<ContractFileItem> ContractFiles { get; set; }
}

public class ContractFileItem
{
    /// <summary>
    /// 文件名称
    /// </summary>
    [JsonProperty("fileName")]
    [JsonPropertyName("fileName")]
    public string FileName { get; set; }

    /// <summary>
    /// 附件编号
    /// </summary>
    [JsonProperty("attachNo")]
    [JsonPropertyName("attachNo")]
    public int AttachNo { get; set; }

    /// <summary>
    /// 页数
    /// </summary>
    [JsonProperty("page")]
    [JsonPropertyName("page")]
    public int Page { get; set; }
}