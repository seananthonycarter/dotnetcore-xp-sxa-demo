using Sitecore.LayoutService.Client.Response.Model.Fields;

namespace dotnetcore_xp_sxa_demo.Models
{
    public class FieldUsageLink : HeadingAndDescription
    {
        public HyperLinkField ExternalLink { get; set; } = default!;

        public HyperLinkField InternalLink { get; set; } = default!;

        public HyperLinkField EmailLink { get; set; } = default!;

        public HyperLinkField ParamsLink { get; set; } = default!;
    }
}
