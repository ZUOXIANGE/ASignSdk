using System.Text.Json.Serialization;

namespace ASignSdk.Requests;

public class AddPersonalUserRequest : ASignRequest<AddPersonalUserRequestDto>
{
    public override string Path => "/v2/user/addPersonalUser";
}

public class AddPersonalUserRequestDto
{
    /// <summary>
    /// 用户唯一识别码（可用证件号、手机等具有唯一属性的标识作为参数传递）
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
    /// 用户姓名
    /// </summary>
    [JsonPropertyName("name")]
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// 个人身份证、台胞证、港澳通行证等证件号
    /// </summary>
    [JsonPropertyName("idCard")]
    [JsonProperty("idCard")]
    public string IdCard { get; set; }

    /// <summary>
    /// 证件类型，不传默认为1；
    /// 1：居民身份证
    /// 2：台湾居民来往大陆通行证
    /// 3：港澳居民来往内地通行证等...
    ///
    /// 更多类型见详细说明
    /// 【注】当选择非居民身份证类型时，爱签平台无法提供实名认证服务。该用户的实名认证由调用方，在同步用户之前自行完成。
    /// </summary>
    [JsonPropertyName("idCardType")]
    [JsonProperty("idCardType")]
    public int? IdCardType { get; set; }

    /// <summary>
    /// 手机号（用于接收短信验证码）
    /// </summary>
    [JsonPropertyName("mobile")]
    [JsonProperty("mobile")]
    public string Mobile { get; set; }

    /// <summary>
    /// 签约密码明文，如果为空我方将随机生成签署密码
    /// </summary>
    [JsonPropertyName("signPwd")]
    [JsonProperty("signPwd")]
    public string SignPwd { get; set; }

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
}