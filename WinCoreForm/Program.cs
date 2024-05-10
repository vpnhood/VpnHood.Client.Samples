using VpnHood.Client.Device.WinDivert;
using VpnHood.Common;

// ReSharper disable StringLiteralTypo
namespace VpnHood.Client.Samples.WinCoreConsole;

internal class Program
{
    private static void Main()
    {
        Console.WriteLine("Hello VpnClient!");

        // a clientId should be generated for each client
        var clientId = Guid.Parse("7BD6C156-EEA3-43D5-90AF-B118FE47ED0A");

        // accessKey must obtain from the server
        var accessKey = "vh://eyJ2Ijo0LCJuYW1lIjoiVnBuSG9vZCBTYW1wbGUiLCJzaWQiOiIxMzAwIiwidGlkIjoiYTM0Mjk4ZDktY2YwYi00MGEwLWI5NmMtZGJhYjYzMWQ2MGVjIiwiaWF0IjoiMjAyNC0wNS0xMFQwNjo1MDozNC42ODQ4NjI4WiIsInNlYyI6Im9wcTJ6M0M0ak9rdHNodXl3c0VKNXc9PSIsImFkIjpmYWxzZSwic2VyIjp7ImN0IjoiMjAyNC0wNC0xNVQxOTo0NDozOVoiLCJobmFtZSI6Im1vLmdpd293eXZ5Lm5ldCIsImhwb3J0IjowLCJpc3YiOmZhbHNlLCJzZWMiOiJ2YUJxVTlSQzNRSGFXNHhGNWliWUZ3PT0iLCJjaCI6IjNnWE9IZTVlY3VpQzlxK3NiTzdobExva1FiQT0iLCJ1cmwiOiJodHRwczovL3Jhdy5naXRodWJ1c2VyY29udGVudC5jb20vdnBuaG9vZC9WcG5Ib29kLkZhcm1LZXlzL21haW4vRnJlZV9lbmNyeXB0ZWRfdG9rZW4udHh0IiwiZXAiOlsiNTEuODEuMjEwLjE2NDo0NDMiLCJbMjYwNDoyZGMwOjIwMjozMDA6OjVjZV06NDQzIl19fQ==";
        var token = Token.FromAccessKey(accessKey);

        var packetCapture = new WinDivertPacketCapture();
        var vpnHoodClient = new VpnHoodClient(packetCapture, clientId, token, new ClientOptions());

        // connect to VpnHood server
        vpnHoodClient.Connect().Wait();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nIP logging is enabled on these servers. Please follow United States law, especially if using torrent. Read privacy policy before use: https://github.com/vpnhood/VpnHood/blob/main/PRIVACY.md\n");
        Console.ResetColor();

        Console.WriteLine("VpnHood Client Is Running! Open your browser and browse the Internet!");
        Console.WriteLine("This is a test server, and the session will be terminated in a few minutes.");
        Console.WriteLine("Press Ctrl+C to stop.");
        while (vpnHoodClient.State != ClientState.Disposed)
            Thread.Sleep(1000);
    }
}