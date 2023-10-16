using System.Text.Json.Serialization;
using ASignSdk.Dtos;

namespace ASignSdk.Requests;

public class CreateContractRequest : ASignRequest<CreateContractRequestDto>
{
    public override string Path => "/contract/createContract";

}

public class CreateContractRequestDto
{
    /// <summary>
    /// 合同ID，合同唯一编号
    /// </summary>
    [JsonPropertyName("contractNo")]
    [JsonProperty("contractNo")]
    public string ContractNo { get; set; }

    /// <summary>
    /// 合同名称
    /// </summary>
    [JsonPropertyName("contractName")]
    [JsonProperty("contractName")]
    public string ContractName { get; set; }

    /// <summary>
    /// 合同有效天数（系统时间+该天数=在此日期之前可以签署合同日期）
    /// </summary>
    [JsonPropertyName("validityTime")]
    [JsonProperty("validityTime")]
    public int? ValidityTime { get; set; }

    /// <summary>
    /// 合同有效截至日期（在此日期之前可以签署合同）
    /// </summary>
    [JsonPropertyName("validityDate")]
    [JsonProperty("validityDate")]
    public string ValidityDate { get; set; }

    /// <summary>
    /// 签约方式：
    /// 1：无序签约（默认）
    /// 2：顺序签约
    /// </summary>
    [JsonPropertyName("signOrder")]
    [JsonProperty("signOrder")]
    public int SignOrder { get; set; }

    /// <summary>
    /// 合同附件（与合同模板必传其一）(支持多文件上传)
    /// </summary>
    [JsonPropertyName("contractFiles")]
    [JsonProperty("contractFiles")]
    [System.Text.Json.Serialization.JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public Dictionary<string, Stream> ContractFiles { get; set; }

    /// <summary>
    /// 合同模板列表（与合同附件必传其一）
    /// </summary>
    [JsonPropertyName("templates")]
    [JsonProperty("templates")]
    public List<ContractTemplateDto> Templates { get; set; }

    /// <summary>
    /// 强制阅读时间（秒）
    /// </summary>
    [JsonPropertyName("readSeconds")]
    [JsonProperty("readSeconds")]
    public int? ReadSeconds { get; set; }

    /// <summary>
    /// 强制阅读设置：
    /// 1：倒计时读秒方式
    /// 2：必须滑动到文件最底部（有多个文件务必逐个阅读）
    /// 3：必须点击打开查看（有多个文件务必逐个打开查看）
    /// </summary>
    [JsonPropertyName("readType")]
    [JsonProperty("readType")]
    public int? ReadType { get; set; }

    /// <summary>
    /// 同意协议开关：（开启后表示必须同意协议才可签署合同）
    /// 1 - 开，0 - 关（默认）
    /// </summary>
    [JsonPropertyName("needAgree")]
    [JsonProperty("needAgree")]
    public int? NeedAgree { get; set; }

    /// <summary>
    /// 多文件时，是否自动展开文件列表
    /// 1 - 展开， 0 - 不展开（默认）
    /// </summary>
    [JsonPropertyName("autoExpand")]
    [JsonProperty("autoExpand")]
    public int? AutoExpand { get; set; }

    /// <summary>
    /// 合同签署完成后（合同状态 status=2）回调通知地址
    /// </summary>
    [JsonPropertyName("notifyUrl")]
    [JsonProperty("notifyUrl")]
    public string NotifyUrl { get; set; }

    /// <summary>
    /// 某个用户签署完成（用户签署状态 signStatus=2）之后回调地址
    /// </summary>
    [JsonPropertyName("userNotifyUrl")]
    [JsonProperty("userNotifyUrl")]
    public string UserNotifyUrl { get; set; }

    /// <summary>
    /// 合同签署完成后同步回调地址
    /// </summary>
    [JsonPropertyName("redirectUrl")]
    [JsonProperty("redirectUrl")]
    public string RedirectUrl { get; set; }

    /// <summary>
    /// 合同签署页退回按钮开关：
    /// 1 - 开启，0 - 关闭（默认）
    /// </summary>
    [JsonPropertyName("refuseOn")]
    [JsonProperty("refuseOn")]
    public int? RefuseOn { get; set; }

    /// <summary>
    /// 当前签署人签署完成自动跳转至下一签署人签署开关（仅对顺序签合同生效）:
    /// 1 - 开启，0 - 关闭（默认）
    /// </summary>
    [JsonPropertyName("autoContinue")]
    [JsonProperty("autoContinue")]
    public int? AutoContinue { get; set; }

    /// <summary>
    /// 合同签署完是否允许可以通过链接查看合同内容:
    /// 1：不允许查看
    /// 不传值：可以查看（默认）
    /// </summary>
    [JsonPropertyName("viewFlg")]
    [JsonProperty("viewFlg")]
    public int? ViewFlg { get; set; }
}