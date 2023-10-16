using System.Text.Json.Serialization;

namespace ASignSdk.Requests;

public class ModifyCompanyInfoRequest : ASignRequest<ModifyCompanyInfoRequestDto>
{
    public override string Path => "/user/modifyCompanyInfo";
}

public class ModifyCompanyInfoRequestDto
{
    /// <summary>
    /// 用户唯一识别码（可用企业社会统一信用代码、邮箱等作为唯一标识）
    /// </summary>
    [JsonPropertyName("account")]
    [JsonProperty("account")]
    public string Account { get; set; }

    /// <summary>
    /// 企业名称
    /// </summary>
    [JsonPropertyName("companyName")]
    [JsonProperty("companyName")]
    public string CompanyName { get; set; }

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
}