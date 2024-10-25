using Sitecore.LayoutService.Client.Response.Model.Fields;

namespace dotnetcore_xp_sxa_demo.Models
{
    public class Tab
    {
        public TextField Title { get; set; } = default!;

        public RichTextField Content { get; set; } = default!;
    }
}
