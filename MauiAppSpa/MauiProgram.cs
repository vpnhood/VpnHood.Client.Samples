using Microsoft.Extensions.Logging;
using VpnHood.Client.App;
using VpnHood.Client.App.Resources;
using VpnHood.Client.App.WebServer;
using VpnHood.Client.App.Maui.Common;

namespace VpnHood.Client.Samples.MauiAppSpa;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp
            .CreateBuilder()
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        var resource = DefaultAppResource.Resource;
        resource.Strings.AppName = "VpnHood Client Sample";
        VpnHoodMauiApp.Init(new AppOptions { Resource = resource });

        // init web server with spa zip data
        ArgumentNullException.ThrowIfNull(VpnHoodApp.Instance.Resource.SpaZipData);
        using var memoryStream = new MemoryStream(VpnHoodApp.Instance.Resource.SpaZipData);
        VpnHoodAppWebServer.Init(memoryStream);

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}