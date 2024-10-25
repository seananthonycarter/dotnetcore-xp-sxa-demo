using Sitecore.LayoutService.Client.Response.Model.Fields;

namespace dotnetcore_xp_sxa_demo.Models
{
    public class FieldUsageCheckbox : HeadingAndDescription
    {
        public CheckboxField Checkbox { get; set; } = default!;

        public CheckboxField Checkbox2 { get; set; } = default!;
    }
}
