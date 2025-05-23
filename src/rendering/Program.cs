using Microsoft.AspNetCore.Localization;
using Sitecore.AspNetCore.SDK.ExperienceEditor.Extensions;
using System.Globalization;
using aspnet_core_demodotcomsite.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

SitecoreSettings? sitecoreSettings = builder.Configuration.GetSection(SitecoreSettings.Key).Get<SitecoreSettings>();
ArgumentNullException.ThrowIfNull(sitecoreSettings);

builder.Services.AddRouting()
                .AddLocalization()
                .AddMvc();

builder.Services.AddSitecoreLayoutService()
                .AddGraphQlHandler("default", sitecoreSettings.DefaultSiteName!, sitecoreSettings.ExperienceEdgeToken!, sitecoreSettings.LayoutServiceUri!)
                .AsDefaultHandler();

builder.Services.AddSitecoreRenderingEngine(options =>
{
    options.AddStarterKitViews()
           .AddDefaultPartialView("_ComponentNotFound");
})
                .ForwardHeaders()
                .WithExperienceEditor(options => { options.JssEditingSecret = sitecoreSettings.EditingSecret ?? string.Empty; });

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

if (sitecoreSettings.EnableEditingMode)
{
    app.UseSitecoreExperienceEditor();
}

app.UseRouting();
app.UseStaticFiles();

const string defaultLanguage = "en";
app.UseRequestLocalization(options =>
{
    // If you add languages in Sitecore which this site / Rendering Host should support, add them here.
    List<CultureInfo> supportedCultures = [new CultureInfo(defaultLanguage)];
    options.DefaultRequestCulture = new RequestCulture(defaultLanguage, defaultLanguage);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.UseSitecoreRequestLocalization();
});

app.MapControllerRoute(
        "error",
        "error",
        new { controller = "Default", action = "Error" }
    );

app.MapSitecoreLocalizedRoute("sitecore", "Index", "Default");
app.MapFallbackToController("Index", "Default");

app.Run();