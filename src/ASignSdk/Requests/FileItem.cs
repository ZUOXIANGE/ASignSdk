namespace ASignSdk.Requests;

public class FileItem
{
    /// <summary>
    /// 文件key
    /// </summary>
    public string FileKey { get; set; }

    /// <summary>
    /// 文件列表  文件名-文件流
    /// </summary>
    public Dictionary<string, Stream> Files { get; set; }
}