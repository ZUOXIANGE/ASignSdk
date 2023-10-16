using System.Text.Json.Serialization;

namespace ASignSdk.Requests;

public class DownloadContractRequest : ASignRequest<DownloadContractRequestDto>
{
    public override string Path => "/contract/downloadContract";
}

public class DownloadContractRequestDto
{
    /// <summary>
    /// 合同唯一编码
    /// </summary>
    [JsonProperty("contractNo")]
    [JsonPropertyName("contractNo")]
    public string ContractNo { get; set; }

    /// <summary>
    /// 文件本地路径（下载的文件，写到本地，当文件数为1时，下载的文件类型是pdf，否则为zip）
    /// </summary>
    [JsonProperty("outfile")]
    [JsonPropertyName("outfile")]
    public string Outfile { get; set; }

    /// <summary>
    /// 强制下载标识
    /// 0（默认）：未签署完的无法下载
    /// 1：无论什么状态都强制下载
    /// </summary>
    [JsonProperty("force")]
    [JsonPropertyName("force")]
    public int? Force { get; set; }

    /// <summary>
    /// 下载文件类型，多附件下载：
    /// 1：PDF文件
    /// 2：多个单张PNG文件，含PDF文件，单附件对应单张图片
    /// 3：分页PNG压缩文件，含PDF文件
    /// 4：合同单张图片，不含PDF文件
    /// 5：所有分页图片，不含PDF文件
    /// </summary>
    [JsonProperty("downloadFileType")]
    [JsonPropertyName("downloadFileType")]
    public int? DownloadFileType { get; set; }
}