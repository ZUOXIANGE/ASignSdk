namespace ASignSdk.Requests;

public class ASignRequest
{
    /// <summary>
    /// 请求路径
    /// </summary>
    [JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual string Path { get; set; }

    /// <summary>
    /// 业务数据
    /// </summary>
    public virtual object Data { get; set; }
}


public class ASignRequest<TData> : ASignRequest
{
    public ASignRequest()
    {
    }

    public ASignRequest(TData data)
    {
        DataItem = data;
    }

    public override object Data => DataItem;

    /// <summary>
    /// 业务数据
    /// </summary>
    public TData DataItem { get; set; }
}

