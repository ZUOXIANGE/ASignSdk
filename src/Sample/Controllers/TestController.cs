using ASignSdk;
using ASignSdk.Requests;
using ASignSdk.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Sample.Controllers;

/// <summary>
/// 测试
/// </summary>
[Route("api/test")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly IASignSdk _aSignSdk;

    public TestController(IASignSdk aSignSdk)
    {
        _aSignSdk = aSignSdk;
    }

    [HttpGet("ping")]
    public string Ping()
    {

        return "pong";
    }

    /// <summary>
    /// 添加个人用户
    /// </summary>
    /// <param name="name"></param>
    /// <param name="no"></param>
    /// <returns></returns>
    [HttpGet("addUser")]
    public async Task<string> AddUser(string name, string no)
    {
        var res = await _aSignSdk.ExecuteAsync<AddPersonalUserRequest, AddPersonalUserResponse>(new AddPersonalUserRequest
        {
            DataItem = new AddPersonalUserRequestDto
            {
                Account = no,
                IdCardType = 1,
                Name = name,
                IdCard = no,
            }
        });

        return res.Message;
    }

    /// <summary>
    /// 合同签署完成回调
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("contractCallback")]
    [AllowAnonymous]
    public async Task<IActionResult> PickupContractCallback([FromForm] SignCompletedCallbackRequest request)
    {
        request.Sign = WebUtility.UrlDecode(request.Sign);
        //request.Sign = request.Sign?.Replace(" ", "+");
        request.SignTime = WebUtility.UrlDecode(request.SignTime);
        request.ValidityTime = WebUtility.UrlDecode(request.ValidityTime);
        request.Remark = WebUtility.UrlDecode(request.Remark);

        var res = "ok";
        if (!_aSignSdk.CheckSign(request, request.Sign))
            res = "签名不正确";

        //执行业务逻辑
        await Task.Delay(10);

        return new ContentResult
        {
            Content = res
        };
    }
}