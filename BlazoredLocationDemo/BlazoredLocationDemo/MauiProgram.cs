using BlazoredLocation.Services;
using BlazoredLocationDemo.Services;
using BlazoredLocationDemo.Shared.Services;
using Microsoft.Extensions.Logging;

namespace BlazoredLocationDemo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the BlazoredLocationDemo.Shared project
            builder.Services.AddScoped<IFormFactor, FormFactor>();
            builder.Services.AddScoped<IDeviceLocation, DeviceLocation>();

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
