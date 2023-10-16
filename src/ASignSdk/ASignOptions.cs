namespace ASignSdk;

public class ASignOptions
{
    /// <summary>
    /// 请求地址
    /// </summary>
    public string RequestUrl { get; set; }

    /// <summary>
    /// appid
    /// </summary>
    public string AppId { get; set; }

    /// <summary>
    /// 私钥
    /// </summary>
    public string PrivateKey { get; set; }

    /// <summary>
    /// 公钥
    /// </summary>
    public string PublicKey { get; set; }

    /// <summary>
    /// 超时时间
    /// </summary>
    public int Timeout { get; set; } = 3;

}