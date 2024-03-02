using Microsoft.Extensions.Logging;
using VpnHood.Client.App;
using VpnHood.Client.App.Maui.Common;
using VpnHood.Client.App.Resources;

namespace VpnHood.Client.Samples.MauiAppForm;

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

        var resources = VpnHoodAppResource.Resources;
        resources.Strings.AppName = "VpnHood Client Sample";
        VpnHoodMauiApp.Init(new AppOptions { Resources = resources });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}