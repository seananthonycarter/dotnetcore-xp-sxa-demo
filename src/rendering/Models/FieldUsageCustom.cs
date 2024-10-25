using Sitecore.LayoutService.Client.Response.Model;

namespace dotnetcore_xp_sxa_demo.Models
{
    public class FieldUsageCustom : HeadingAndDescription
    {
        public Field<int> CustomIntField { get; set; } = default!;
    }
}
