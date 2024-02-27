using Android.App;
using Android.Runtime;
using VpnHood.Client.Device.Droid;

namespace VpnHood.Client.Samples.MauiAppSpaSample;


[Application]
public class MainApplication(IntPtr handle, JniHandleOwnership ownership) 
    : MauiApplication(handle, ownership)
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp(new AndroidDevice());
}