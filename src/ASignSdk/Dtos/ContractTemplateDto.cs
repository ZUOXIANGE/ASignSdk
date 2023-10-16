using System.Text.Json.Serialization;

namespace ASignSdk.Dtos;

public class ContractTemplateDto
{
    /// <summary>
    /// 合同模板编号
    /// </summary>
    [JsonPropertyName("templateNo")]
    [JsonProperty("templateNo")]
    public string TemplateNo { get; set; }

    /// <summary>
    /// 合同编号（此处可传已完成签署的合同编号，实现追加签章的场景）
    /// </summary>
    [JsonPropertyName("contractNo")]
    [JsonProperty("contractNo")]
    public string ContractNo { get; set; }

    [JsonPropertyName("componentData")]
    [JsonProperty("componentData")]
    public List<ComponentDto> ComponentData { get; set; }

    [JsonPropertyName("tableDatas")]
    [JsonProperty("tableDatas")]
    public List<TableDto> TableData { get; set; }

}

public class ComponentDto
{
    /// <summary>
    /// 组件类型：2：单选 3：勾选 9：复选 11：图片
    /// </summary>
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public int Type { get; set; }

    /// <summary>
    /// 参数名称
    /// </summary>
    [JsonProperty("keyword")]
    [JsonPropertyName("keyword")]
    public string Keyword { get; set; }

    /// <summary>
    /// 当填充类型为勾选（type=3）时填写：Yes：选中 Off：不选中
    /// </summary>
    [JsonProperty("defaultValue")]
    [JsonPropertyName("defaultValue")]
    public string DefaultValue { get; set; }

    /// <summary>
    /// 图片资源
    /// </summary>
    [JsonProperty("imageByte")]
    [JsonPropertyName("imageByte")]
    public byte[] ImageByte { get; set; }

    /// <summary>
    /// 选项内容
    /// </summary>
    [JsonProperty("options")]
    [JsonPropertyName("options")]
    public List<OptionDto> Options { get; set; }
}

public class OptionDto
{
    /// <summary>
    /// 下标：从0开始（选项的位置）
    /// </summary>
    [JsonProperty("index")]
    [JsonPropertyName("index")]
    public int Index { get; set; }

    /// <summary>
    /// 选中标记（true为选中）
    /// </summary>
    [JsonProperty("selected")]
    [JsonPropertyName("selected")]
    public bool Selected { get; set; }
}

public class TableDto
{
    /// <summary>
    /// 表名称
    /// </summary>
    [JsonProperty("keyword")]
    [JsonPropertyName("keyword")]
    public string Keyword { get; set; }

    /// <summary>
    /// 表格填充值
    /// </summary>
    [JsonProperty("rowValues")]
    [JsonPropertyName("rowValues")]
    public List<TableRowDto> RowValues { get; set; }
}

public class TableRowDto
{
    /// <summary>
    /// 是否新增行，默认false
    /// </summary>
    [JsonProperty("insertRow")]
    [JsonPropertyName("insertRow")]
    public bool InsertRow { get; set; }

    /// <summary>
    /// 动态表格行填充内容，第一列至最后一列依次的填充
    /// </summary>
    [JsonProperty("colValues")]
    [JsonPropertyName("colValues")]
    public List<string> ColValues { get; set; }
}