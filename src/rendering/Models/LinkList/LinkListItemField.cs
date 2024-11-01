using Sitecore.AspNetCore.SDK.LayoutService.Client.Response.Model.Fields;
using System.Text.Json.Serialization;

namespace aspnet_core_demodotcomsite.Models;

public class LinkListItemField
{
    [JsonPropertyName("link")]
    public HyperLinkField? Link { get; set; }
}