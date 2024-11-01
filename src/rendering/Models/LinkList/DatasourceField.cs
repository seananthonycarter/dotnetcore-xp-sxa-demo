using System.Text.Json.Serialization;

namespace aspnet_core_demodotcomsite.Models;

public class DatasourceField
{
    [JsonPropertyName("field")]
    public LinkListField? Field { get; set; }

    [JsonPropertyName("children")]
    public LinkListChildren? Children { get; set; }
}