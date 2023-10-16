using System.Text.Json.Serialization;

namespace ASignSdk.Responses;

public class DownloadContractResponse : ASignResponse<DownloadContractResponseDto>
{

}

public class DownloadContractResponseDto
{
    /// <summary>
    /// 文件名
    /// </summary>
    [JsonProperty("fileName")]
    [JsonPropertyName("fileName")]
    public string FileName { get; set; }

    /// <summary>
    /// 文件md5值
    /// </summary>
    [JsonProperty("md5")]
    [JsonPropertyName("md5")]
    public string Md5 { get; set; }

    /// <summary>
    /// 文件类型
    /// 0：PDF
    /// 1：ZIP
    /// </summary>
    [JsonProperty("fileType")]
    [JsonPropertyName("fileType")]
    public int FileType { get; set; }

    /// <summary>
    /// 文件大小
    /// </summary>
    [JsonProperty("size")]
    [JsonPropertyName("size")]
    public long Size { get; set; }

    /// <summary>
    /// Base64字符串数据
    /// </summary>
    [JsonProperty("data")]
    [JsonPropertyName("data")]
    public string Data { get; set; }
}