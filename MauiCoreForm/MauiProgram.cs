using Microsoft.Extensions.Logging;
using VpnHood.Core.Client.Device;

namespace VpnHood.Client.Samples.MauiCoreForm;

public static class MauiProgram
{
    public static IUiContext? CurrentUiContext { get; set; }
    public static IDevice VpnHoodDevice { get; private set; } = default!;
    public static MauiApp CreateMauiApp(IDevice vpnHoodDevice)
    {
        VpnHoodDevice = vpnHoodDevice;

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

        return builder.Build();
    }
}