global using System.Security.Cryptography;
global using System.Text;
global using Newtonsoft.Json;
global using Newtonsoft.Json.Serialization;

using ASignSdk.Requests;
using ASignSdk.Responses;

namespace ASignSdk;

public interface IASignSdk
{
    /// <summary>
    /// 执行请求
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="request"></param>
    /// <param name="canLog"></param>
    /// <returns></returns>
    public Task<TResponse> ExecuteAsync<TRequest, TResponse>(TRequest request, bool canLog = true)
        where TRequest : ASignRequest, new()
        where TResponse : ASignResponse, new();

    /// <summary>
    /// 签名检查
    /// </summary>
    /// <param name="content"></param>
    /// <param name="sign"></param>
    /// <returns></returns>
    public bool CheckSign(SignCallbackRequest content, string sign);

}