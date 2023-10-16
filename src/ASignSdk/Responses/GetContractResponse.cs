namespace ASignSdk.Responses;

public class GetContractResponse : ASignResponse<GetContractResponseDto>
{

}

public class GetContractResponseDto
{
    /// <summary>
    /// 合同唯一编号
    /// </summary>
    [JsonProperty("contractNo")]
    public string ContractNo { get; set; }

    /// <summary>
    /// 合同状态：
    /// 0：等待签约
    /// 1：签约中
    /// 2：已签约
    /// 3：过期
    /// 4：拒签
    /// 6：作废
    /// -2：状态异常
    /// </summary>
    [JsonProperty("status")]
    public int Status { get; set; }

    /// <summary>
    /// 合同名称
    /// </summary>
    [JsonProperty("contractName")]
    public string ContractName { get; set; }

    /// <summary>
    /// 合同有效期
    /// </summary>
    [JsonProperty("validityTime")]
    public string ValidityTime { get; set; }

    /// <summary>
    /// 合同预览链接
    /// </summary>
    [JsonProperty("previewUrl")]
    public string PreviewUrl { get; set; }

    /// <summary>
    /// 退回/拒签原因
    /// </summary>
    [JsonProperty("remark")]
    public string Remark { get; set; }

    /// <summary>
    /// 合同用户信息
    /// </summary>
    [JsonProperty("signUser")]
    public List<UserSignInfo> SignUser { get; set; }
}

/// <summary>
/// 用户签约信息
/// </summary>
public class UserSignInfo
{
    /// <summary>
    /// 用户唯一识别码
    /// </summary>
    [JsonProperty("account")]
    public string Account { get; set; }

    /// <summary>
    /// 合同签署链接
    /// </summary>
    [JsonProperty("signUrl")]
    public string SignUrl { get; set; }

    /// <summary>
    /// 密码签署链接
    /// </summary>
    [JsonProperty("pwdSignUrl")]
    public string PwdSignUrl { get; set; }

    /// <summary>
    /// 顺序签约序号（无序签约默认都是1）
    /// </summary>
    [JsonProperty("signOrder")]
    public int SignOrder { get; set; }

    /// <summary>
    /// 用户姓名
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// 用户身份证
    /// </summary>
    [JsonProperty("idCard")]
    public string IdCard { get; set; }

    /// <summary>
    /// 手机号（签约短信通知手机号）
    /// </summary>
    [JsonProperty("mobile")]
    public string Mobile { get; set; }

    /// <summary>
    /// 企业名称
    /// </summary>
    [JsonProperty("companyName")]
    public string CompanyName { get; set; }

    /// <summary>
    /// 用户类型：
    /// 1：企业
    /// 2：个人
    /// </summary>
    [JsonProperty("userType")]
    public int UserType { get; set; }

    /// <summary>
    /// 印章编号
    /// </summary>
    [JsonProperty("sealNo")]
    public string SealNo { get; set; }

    /// <summary>
    /// 签约类型：
    /// 1：代签
    /// 2：无感知签
    /// 3：有感知签
    /// </summary>
    [JsonProperty("signType")]
    public int SignType { get; set; }

    /// <summary>
    /// 签约校验方式：
    /// 1：验证码
    /// 2：签约密码
    /// 3：人脸识别
    /// 4：手写
    /// </summary>
    [JsonProperty("validateType")]
    public int ValidateType { get; set; }

    /// <summary>
    /// 签约状态：
    /// 1：签约中
    /// 2：已签约
    /// 3：待签约
    /// 4：拒签
    /// </summary>
    [JsonProperty("signStatus")]
    public int SignStatus { get; set; }

    /// <summary>
    /// 签约完成时间
    /// </summary>
    [JsonProperty("signFinishedTime")]
    public string SignFinishedTime { get; set; }
}
