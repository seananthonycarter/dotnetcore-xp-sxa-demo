using Sitecore.LayoutService.Client.Response.Model.Fields;

namespace dotnetcore_xp_sxa_demo.Models
{
    public class FieldUsageDate : HeadingAndDescription
    {
        public DateField Date { get; set; } = default!;

        public DateField DateTime { get; set; } = default!;
    }
}
