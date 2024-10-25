using Sitecore.AspNet.RenderingEngine.Binding.Attributes;
using Sitecore.LayoutService.Client.Response.Model.Fields;

namespace dotnetcore_xp_sxa_demo.Models
{
    public class FieldUsageContentList : HeadingAndDescription
    {
        [SitecoreComponentField(Name = "localContentList")]
        public ContentListField<LinkItemTemplate> LocalContentList { get; set; } = default!;

        [SitecoreComponentField(Name = "sharedContentList")]
        public ContentListField<LinkItemTemplate> SharedContentList { get; set; } = default!;
    }
}
