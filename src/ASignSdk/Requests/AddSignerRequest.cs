using System.Text.Json.Serialization;

namespace ASignSdk.Requests;

public class AddSignerRequest : ASignRequest<List<AddSignerDto>>
{
    public override string Path => "/contract/addSigner";
}

public class AddSignerDto
{
    /// <summary>
    /// 合同唯一编码 (40位之内)
    /// </summary>
    [JsonProperty("contractNo")]
    public string ContractNo { get; set; }

    /// <summary>
    /// 用户唯一识别码
    /// </summary>
    [JsonProperty("account")]
    public string Account { get; set; }

    /// <summary>
    /// 签约方式：
    /// 2：无感知签约（需要开通权限）
    /// 3：有感知签约
    /// </summary>
    [JsonProperty("signType")]
    public int SignType { get; set; }

    /// <summary>
    /// 印章编号
    /// </summary>
    [JsonProperty("sealNo")]
    public string SealNo { get; set; }

    /// <summary>
    /// 签约短信通知手机号
    /// </summary>
    [JsonProperty("noticeMobile")]
    public string NoticeMobile { get; set; }

    /// <summary>
    /// 签约短信通知邮箱号
    /// </summary>
    [JsonProperty("noticeEmail")]
    public string NoticeEmail { get; set; }

    /// <summary>
    /// 使用顺序签约时签约顺序编号，无序签约都为1
    /// </summary>
    [JsonProperty("signOrder")]
    public string SignOrder { get; set; }

    /// <summary>
    /// 是否接收合同签署链接的短信通知，优先级高于添加用户接口同名参数：
    /// 0 - 否（默认）
    /// 1 - 是
    /// </summary>
    [JsonProperty("isNotice")]
    public int IsNotice { get; set; }

    /// <summary>
    /// 签署方式指定：（从以下分类中指定一种）
    /// 1：短信验证码签约（支持企业和个人）
    /// 2：签约密码签约（支持企业和个人）
    /// 3：人脸识别签约（支持企业和个人）
    /// 4：手写签名（不推荐，仅限个人）
    /// 6：手写识别签名+短信签约（仅限个人）
    /// 7：手写签名+短信签约（仅限个人）
    /// 8：手写签名+人脸识别签约（仅限个人）
    /// 9：手写识别签名+人脸识别签约（仅限个人）
    /// </summary>
    [JsonProperty("validateType")]
    public int ValidateType { get; set; }

    /// <summary>
    /// 人脸识别方式：
    /// 1：支付宝（不可在支付宝小程序中接入）
    /// 2：H5（默认）
    /// 4：微信小程序(采用微信小程序原生刷脸方式，无需唤起第三方小程序来完成刷脸，需联系商务人员开启权限后使用)
    /// 【注】签署方式包含人脸（3,8,9）时，可指定人脸识别方式，不传默认为H5
    /// </summary>
    [JsonProperty("faceAuthMode")]
    public int FaceAuthMode { get; set; }

    /// <summary>
    /// 组合签署方式指定：（从以上分类中指定多种以逗号间隔，示例：1,2,3）
    /// 允许开发者可以自主控制展示几种签署方式，让签约用户选择。
    /// 【注】 validateTypeList和validateType都有传值时，签署方式按照validateTypeList指定
    /// </summary>
    [JsonProperty("validateTypeList")]
    public string ValidateTypeList { get; set; }

    /// <summary>
    /// 自动切换签约方式：
    /// 开发者可以自主控制手写内容识别和人脸刷脸识别多次不通过时，是否允许用户切换方式
    /// 1 - 仅手写识别允许切换（默认）
    /// 2 - 仅人脸识别允许切换
    /// 3 - 全部允许
    /// 0 - 全部不允许
    /// 【注】手写识别三次失败时，会允许用户切换成宋体印章。人脸识别三次不通过，允许切换为短信验证码方式。
    /// </summary>
    [JsonProperty("autoSwitch")]
    public int AutoSwitch { get; set; }

    /// <summary>
    /// 合同签署完成后是否通知用户：
    /// 1 - 是，0 - 否（默认）
    /// </summary>
    [JsonProperty("isNoticeComplete")]
    public int IsNoticeComplete { get; set; }

    /// <summary>
    /// 是否在距底部10px中央位置添加日期水印：
    /// 1 - 是，0 - 否（默认）
    /// </summary>
    [JsonProperty("waterMark")]
    public int WaterMark { get; set; }

    /// <summary>
    /// 是否自动触发验证码短信：（仅短信验证码方式签署时生效）
    /// 1：是（默认）
    /// 0：否（需要用户手动点击“获取验证码”触发）
    /// </summary>
    [JsonProperty("autoSms")]
    public int AutoSms { get; set; }

    /// <summary>
    /// 签章位置策略：
    /// 0（默认）- 由该接口的参数signStrategyList或signStrikeList指定
    /// 1 - 签署用户在签署时自行拖动签章位置
    /// </summary>
    [JsonProperty("customSignFlag")]
    public int CustomSignFlag { get; set; }

    /// <summary>
    /// 签章策略
    /// </summary>
    [JsonProperty("signStrategyList")]
    public List<SignStrategyDto> SignStrategyList { get; set; }
}

public class SignStrategyDto
{
    /// <summary>
    /// 附件编号
    /// </summary>
    [JsonProperty("attachNo")]
    [JsonPropertyName("attachNo")]
    public int AttachNo { get; set; }

    /// <summary>
    /// 定位方式：2-坐标签章, 3-关键字签章, 4-模板坐标签章
    /// </summary>
    [JsonProperty("locationMode")]
    [JsonPropertyName("locationMode")]
    public int LocationMode { get; set; }

    /// <summary>
    /// 签章位置是否可以拖动
    /// 1：可以，其他值：不可以
    /// </summary>
    [JsonProperty("canDrag")]
    [JsonPropertyName("canDrag")]
    public int? CanDrag { get; set; }

    /// <summary>
    /// 关键字（定位方式为关键字签章时需传定位关键字，定位方式为模板坐标签章时需传模板中设置的签署区名称）
    /// </summary>
    [JsonProperty("signKey")]
    [JsonPropertyName("signKey")]
    public string SignKey { get; set; }

    /// <summary>
    /// 印章类型：1-签名（默认），2-时间
    /// </summary>
    [JsonProperty("signType")]
    [JsonPropertyName("signType")]
    public int? SignType { get; set; }

    /// <summary>
    /// 签章页码（定位方式为坐标签章时必传）
    /// </summary>
    [JsonProperty("signPage")]
    [JsonPropertyName("signPage")]
    public int? SignPage { get; set; }

    /// <summary>
    /// 签章位置与当前签约文件的左内边距与当前签约文件宽度的比例（定位方式为坐标签章时必传，精确到小数点后2位）
    /// </summary>
    [JsonProperty("signX")]
    [JsonPropertyName("signX")]
    public double? SignX { get; set; }

    /// <summary>
    /// 签章位置与当前签约文件的上内边距与当前签约文件高度的比例（位方式为坐标签章时必传，精确到小数点后2位）
    /// </summary>
    [JsonProperty("signY")]
    [JsonPropertyName("signY")]
    public double? SignY { get; set; }

    /// <summary>
    /// 坐标偏移量（像素PX）
    /// </summary>
    [JsonProperty("offsetX")]
    [JsonPropertyName("offsetX")]
    public double? OffsetX { get; set; }

    /// <summary>
    /// 坐标偏移量（像素PX）
    /// </summary>
    [JsonProperty("offsetY")]
    [JsonPropertyName("offsetY")]
    public double? OffsetY { get; set; }
}