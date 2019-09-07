using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace ContractManager.Web.Extensions
{
    public static class Globalization
    {
        public static void UseRequestLocalizationFromBrazil(this IApplicationBuilder app)
        {
            var supportedCultures = new[] { new CultureInfo("pt-BR"), new CultureInfo("en-US") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });
        }
    }
}
