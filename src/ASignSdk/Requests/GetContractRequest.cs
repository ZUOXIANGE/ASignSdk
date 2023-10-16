namespace ASignSdk.Requests;

public class GetContractRequest : ASignRequest<GetContractRequestDto>
{
    public override string Path => "/contract/getContract";
}

public class GetContractRequestDto
{
    /// <summary>
    /// 合同唯一编码 (40位之内)
    /// </summary>
    [JsonProperty("contractNo")]
    public string ContractNo { get; set; }
}