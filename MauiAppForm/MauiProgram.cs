using Microsoft.Extensions.Logging;
using VpnHood.Client.App;
using VpnHood.Client.App.Maui.Common;
using VpnHood.Client.App.Resources;

namespace VpnHood.Client.Samples.MauiAppForm;

// ReSharper disable StringLiteralTypo
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

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}