using System.Text.Json.Serialization;

namespace aspnet_core_demodotcomsite.Models;

public class DataField
{
    [JsonPropertyName("datasource")]
    public DatasourceField? Datasource { get; set; }
}