# ASignSdk [![ASignSdk](https://img.shields.io/nuget/v/ASignSdk.svg)](https://www.nuget.org/packages/ASignSdk/)[![ASignSdk](https://img.shields.io/nuget/dt/ASignSdk.svg)](https://www.nuget.org/packages/ASignSdk/)

[爱签开放平台](https://web.asign.cn/platform)SDK,电子合同等

只封装了部分业务参数模型,可以根据自己的需求自行补充pr

## 安装

```
dotnet add package ASignSdk
```

## 使用示例

``` csharp

//添加爱签SDK
builder.Services.AddASignSdk(builder.Configuration, "ASignSdk");

//添加个人用户
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

```