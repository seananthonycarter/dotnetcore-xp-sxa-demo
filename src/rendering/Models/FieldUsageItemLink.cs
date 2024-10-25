using Sitecore.AspNet.RenderingEngine.Binding.Attributes;
using Sitecore.LayoutService.Client.Response.Model.Fields;

namespace dotnetcore_xp_sxa_demo.Models
{
    public class FieldUsageItemLink : HeadingAndDescription
    {
        [SitecoreComponentField(Name = "localItemLink")]
        public ItemLinkField<LinkItemTemplate> LocalItemLink { get; set; } = default!;

        [SitecoreComponentField(Name = "sharedItemLink")]
        public ItemLinkField<LinkItemTemplate> SharedItemLink { get; set; } = default!;
    }
}
