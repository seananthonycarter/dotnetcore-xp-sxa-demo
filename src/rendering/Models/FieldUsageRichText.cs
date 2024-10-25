using Sitecore.LayoutService.Client.Response.Model.Fields;

namespace dotnetcore_xp_sxa_demo.Models
{
    public class FieldUsageRichText : HeadingAndDescription
    {
        public RichTextField Sample { get; set; } = default!;

        public RichTextField Sample2 { get; set; } = default!;
    }
}
