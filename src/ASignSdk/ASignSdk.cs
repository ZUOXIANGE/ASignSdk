using ASignSdk.Requests;
using ASignSdk.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ASignSdk;

public class ASignSdk : IASignSdk
{
    #region 构造注入

    private static readonly DateTime Orginal = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private readonly ASignOptions _options;
    private readonly IHttpClientFactory _clientFactory;
    private readonly ILogger<ASignSdk> _logger;
    private static readonly JsonSerializerSettings JsonSetting = new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        DateFormatString = "yyyy-MM-dd HH:mm:ss",
        ContractResolver = new OrderedContractResolver()
    };

    public ASignSdk(
        IOptions<ASignOptions> options,
        IHttpClientFactory clientFactory,
        ILogger<ASignSdk> logger)
    {
        _clientFactory = clientFactory;
        _logger = logger;
        _options = options.Value;
    }

    #endregion

    /// <summary>
    /// 执行
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="request"></param>
    /// <param name="canLog"></param>
    /// <returns></returns>
    public async Task<TResponse> ExecuteAsync<TRequest, TResponse>(TRequest request, bool canLog = true)
        where TRequest : ASignRequest, new()
        where TResponse : ASignResponse, new()
    {
        if (string.IsNullOrEmpty(_options.RequestUrl)) return null;
        var url = _options.RequestUrl + request.Path;
        var appId = _options.AppId;
        var timestamp = GetTimeStamp().ToString();

        //签名
        var bizJson = JsonConvert.SerializeObject(request.Data, JsonSetting);

        using var md5 = MD5.Create();
        var result = md5.ComputeHash(Encoding.UTF8.GetBytes(bizJson));
        var md5Str = BitConverter.ToString(result).Replace("-", "").ToLower();
        var rsaSuffix = bizJson + md5Str + appId + timestamp;
        var sign = MakeSign(rsaSuffix, _options.PrivateKey);

        if (canLog)
            _logger.LogInformation("爱签请求:{url} {bizJson} {timestamp} {sign}", url, bizJson, timestamp, sign);

        var client = _clientFactory.CreateClient(nameof(ASignSdk));
        //请求头
        client.DefaultRequestHeaders.Add("sign", sign);
        client.DefaultRequestHeaders.Add("timestamp", timestamp);
        //请求体
        var dic = new Dictionary<string, string>
        {
            { "appId", _options.AppId },
            { "timestamp", timestamp },
            { "bizData", bizJson }
        };

        string respJson;
        if (request is CreateContractRequest contractRequest)//TODO 这里暂时直接根据类型硬编码
        {
            respJson = await PostFormAsync(client, url, dic, "contractFiles", contractRequest.DataItem.ContractFiles);
        }
        else
        {
            respJson = await PostFormAsync(client, url, dic);
        }

        if (canLog)
            _logger.LogInformation("爱签返回:{respJson}", respJson);

        var data = JsonConvert.DeserializeObject<TResponse>(respJson);
        if (!canLog && (data == null || !data.Success))
        {
            _logger.LogWarning("爱签请求:{url} {bizJson} {sign}", url, bizJson, sign);
            _logger.LogWarning("爱签返回:{respJson}", respJson);
        }

        return data;
    }

    /// <summary>
    /// form请求
    /// </summary>
    /// <param name="client"></param>
    /// <param name="requestUrl"></param>
    /// <param name="dic"></param>
    /// <param name="fileKey"></param>
    /// <param name="files"></param>
    /// <returns></returns>
    public static async Task<string> PostFormAsync(HttpClient client, string requestUrl,
        Dictionary<string, string> dic, string fileKey = null, Dictionary<string, Stream> files = null)
    {
        var req = new HttpRequestMessage(HttpMethod.Post, requestUrl);
        var content = new MultipartFormDataContent();
        foreach (var item in dic)
        {
            content.Add(new StringContent(item.Value), item.Key);
        }

        if (files != null && fileKey != null)
        {
            foreach (var (fileName, fileStream) in files)
            {
                content.Add(new StreamContent(fileStream), fileKey, fileName);
            }
        }

        req.Content = content;
        var res = await client.SendAsync(req);
        res.EnsureSuccessStatusCode();
        var data = await res.Content.ReadAsStringAsync();
        return data;
    }

    /// <summary>
    /// 获取时间戳
    /// </summary>
    /// <returns></returns>
    private static long GetTimeStamp()
    {
        var timeStamp = (long)(DateTime.Now.AddMinutes(10).ToUniversalTime() - Orginal).TotalMilliseconds;
        return timeStamp;
    }

    /// <summary>
    /// 签名
    /// </summary>
    /// <param name="content">待签名字符串</param>
    /// <param name="privateKey">私钥</param>
    /// <returns>签名后字符串</returns>
    private static string MakeSign(string content, string privateKey)
    {
        //加载私钥
        using var rsa = RSA.Create();
        var bytes = Convert.FromBase64String(privateKey);
        rsa.ImportPkcs8PrivateKey(bytes, out _);
        //签名
        var data = Encoding.UTF8.GetBytes(content);
        var signature = rsa.SignData(data, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
        return Convert.ToBase64String(signature);
    }

    /// <summary>
    /// 签名检查
    /// </summary>
    /// <param name="content"></param>
    /// <param name="sign"></param>
    /// <returns></returns>
    public bool CheckSign(SignCallbackRequest content, string sign)
    {
        //注意这里的json key顺序和value是否经过url encode
        var json = JsonConvert.SerializeObject(content, JsonSetting);
        var param = JsonConvert.DeserializeObject<SortedDictionary<string, object>>(json);
        param.Remove("sign");
        json = JsonConvert.SerializeObject(param);
        _logger.LogInformation("爱签回调参数:{json}  {sign}", json, sign);
        using var rsa = RSA.Create();
        var bytes = Convert.FromBase64String(_options.PublicKey);
        rsa.ImportSubjectPublicKeyInfo(bytes, out _);

        var data = Encoding.UTF8.GetBytes(json);
        var signBytes = Convert.FromBase64String(sign);

        return rsa.VerifyData(data, signBytes, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
    }

}

/// <summary>
/// 序列化字段排序
/// </summary>
public class OrderedContractResolver : DefaultContractResolver
{
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
        return base.CreateProperties(type, memberSerialization).OrderBy(p => p.PropertyName).ToList();
    }
}