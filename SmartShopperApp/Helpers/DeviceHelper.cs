using Microsoft.Maui.Devices;

namespace SmartShopperApp;

public static class DeviceHelper
{
    public static bool IsSimulator()
    {
#if IOS
        return DeviceInfo.DeviceType == DeviceType.Virtual;
#else
        return false;
#endif
    }
}