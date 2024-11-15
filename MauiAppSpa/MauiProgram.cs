using Microsoft.Extensions.Logging;
using VpnHood.Client.App;
using VpnHood.Client.App.Resources;
using VpnHood.Client.App.WebServer;
using VpnHood.Client.App.Maui.Common;

namespace VpnHood.Client.Samples.MauiAppSpa;

// ReSharper disable StringLiteralTypo
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
        const string accessKey = ClientOptions.SampleAccessKey; // This is for test purpose only and can not be used in production
        VpnHoodMauiApp.Init(new AppOptions
        {
            AppId = "com.vpnhood.client.sample",
            Resource = resource, 
            AccessKeys = [accessKey]
        });

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