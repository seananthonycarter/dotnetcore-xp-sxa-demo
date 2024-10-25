using Sitecore.LayoutService.Client.Response.Model.Fields;

namespace dotnetcore_xp_sxa_demo.Models
{
    public class FieldUsageNumber : HeadingAndDescription
    {
        public NumberField Sample { get; set; } = default!;
    }
}
