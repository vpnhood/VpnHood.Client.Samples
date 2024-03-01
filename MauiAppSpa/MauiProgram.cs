﻿using Microsoft.Extensions.Logging;
using VpnHood.Client.App;
using VpnHood.Client.App.Resources;
using VpnHood.Client.Device;

namespace VpnHood.Client.Samples.MauiAppSpa;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp(IDevice vpnHoodDevice)
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var resources = VpnHoodAppResource.Resources;
        resources.Strings.AppName = "VpnHood Maui App SPA Sample";
        VpnHoodApp.Init(vpnHoodDevice, new AppOptions() { Resources = resources });

        return builder.Build();
    }
}