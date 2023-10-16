using System.Text.Json.Serialization;

namespace ASignSdk.Requests;

public class AddEnterpriseUserRequest : ASignRequest<AddEnterpriseUserRequestDto>
{
    public override string Path => "/v2/user/addEnterpriseUser";
}

public class AddEnterpriseUserRequestDto
{
    /// <summary>
    /// 用户唯一识别码（可用企业社会统一信用代码、邮箱等作为唯一标识）
    /// </summary>
    [JsonPropertyName("account")]
    [JsonProperty("account")]
    public string Account { get; set; }

    /// <summary>
    /// 实名认证流水号
    /// </summary>
    [JsonPropertyName("serialNo")]
    [JsonProperty("serialNo")]
    public string SerialNo { get; set; }

    /// <summary>
    /// 企业名称
    /// </summary>
    [JsonPropertyName("companyName")]
    [JsonProperty("companyName")]
    public string CompanyName { get; set; }

    /// <summary>
    /// 企业社会统一信用代码
    /// </summary>
    [JsonPropertyName("creditCode")]
    [JsonProperty("creditCode")]
    public string CreditCode { get; set; }

    /// <summary>
    /// 法人姓名
    /// </summary>
    [JsonPropertyName("name")]
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// 法人身份证、台胞证、港澳通行证等证件号
    /// </summary>
    [JsonPropertyName("idCard")]
    [JsonProperty("idCard")]
    public string IdCard { get; set; }

    /// <summary>
    /// 证件类型，不传默认为1：
    /// 1 - 居民身份证
    /// 2 - 台湾居民来往大陆通行证
    /// 3 - 港澳居民来往内地通行证等...
    /// </summary>
    [JsonPropertyName("idCardType")]
    [JsonProperty("idCardType")]
    public int? IdCardType { get; set; }

    /// <summary>
    /// 法人手机号（需要法人本人实名登记）
    /// </summary>
    [JsonPropertyName("identifyMobile")]
    [JsonProperty("identifyMobile")]
    public string IdentifyMobile { get; set; }

    /// <summary>
    /// 是否将签署密码以短信形式通知用户：
    /// 0 - 不通知（默认）
    /// 1 - 通知
    /// </summary>
    [JsonPropertyName("isSignPwdNotice")]
    [JsonProperty("isSignPwdNotice")]
    public int? IsSignPwdNotice { get; set; }

    /// <summary>
    /// 用户发起合同或需要签署时是否进行短信通知：
    /// 0 - 否（默认）
    /// 1 - 是
    /// </summary>
    [JsonPropertyName("isNotice")]
    [JsonProperty("isNotice")]
    public int? IsNotice { get; set; }

    /// <summary>
    /// 签约密码明文，如果为空则随机生成签署密码
    /// </summary>
    [JsonPropertyName("signPwd")]
    [JsonProperty("signPwd")]
    public string SignPwd { get; set; }

    /// <summary>
    /// 签约手机，该手机号将用于企业用户合同签署时短信验证，请确保真实有效
    /// </summary>
    [JsonPropertyName("mobile")]
    [JsonProperty("mobile")]
    public string Mobile { get; set; }

    /// <summary>
    /// 联系人姓名（与企业的法定代表人可以是同一个人）
    /// </summary>
    [JsonPropertyName("contactName")]
    [JsonProperty("contactName")]
    public string ContactName { get; set; }

    /// <summary>
    /// 联系人身份证号（与企业的法定代表人可以是同一个人）
    /// </summary>
    [JsonPropertyName("contactIdCard")]
    [JsonProperty("contactIdCard")]
    public string ContactIdCard { get; set; }

    /// <summary>
    /// 用户实名认证模式为强制认证时，需要选择法人实名认证类型：
    /// 1 - 法人银行卡四要素认证（仅限银联卡）
    /// 3 - 法人运营商三要素认证
    /// </summary>
    [JsonPropertyName("identifyType")]
    [JsonProperty("identifyType")]
    public int? IdentifyType { get; set; }

}