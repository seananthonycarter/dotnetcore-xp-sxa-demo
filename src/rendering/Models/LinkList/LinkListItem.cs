using System.Text.Json.Serialization;

namespace aspnet_core_demodotcomsite.Models;

public class LinkListItem 
{
    [JsonPropertyName("field")]
    public LinkListItemField? Field { get; set; }
}