using Sitecore.LayoutService.Client.Response.Model.Fields;

namespace dotnetcore_xp_sxa_demo.Models
{
    public class FieldUsageImage : HeadingAndDescription
    {
        public ImageField Sample1 { get; set; } = default!;

        public ImageField Sample2 { get; set; } = default!;
    }
}
