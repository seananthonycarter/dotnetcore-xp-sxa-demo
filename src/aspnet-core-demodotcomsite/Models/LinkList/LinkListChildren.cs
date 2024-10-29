using System.Text.Json.Serialization;

namespace aspnet_core_demodotcomsite.Models;

public class LinkListChildren
{
    [JsonPropertyName("results")]
    public List<LinkListItem>? Results { get; set; }
}