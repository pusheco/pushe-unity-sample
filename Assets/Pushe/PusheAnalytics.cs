using UnityEngine;

public class PusheAnalytics
{
    private const string PushePath = "co.pushe.plus.Pushe";
    private const string PusheExtPath = "co.pushe.plus.ext.PusheExt";


    /// <summary>
    ///  Send an event to the server. You can pass push notifications according to happening on some events.
    /// </summary>
    /// <param name="eventName">A name chosen by you</param>
    /// <b>If You don't have and action, leave it be custom.</b>
    public static void SendEvent(string eventName)
    {
        PusheAnalyticsService().Call("sendEvent", eventName);
    }

    public static void SendEcommerceData(string name, double price, string category = null, long quantity = -1)
    {
        PusheExt().CallStatic("sendEcommerce", name, price, category, quantity);
    }

    // Util classes
    
    
    private static AndroidJavaClass Pushe()
    {
        return new AndroidJavaClass(PushePath);
    }

    private static AndroidJavaObject PusheAnalyticsService()
    {
        return Pushe().CallStatic<AndroidJavaObject>("getPusheService", "analytics");
    }

    private static AndroidJavaClass PusheExt()
    {
        return new AndroidJavaClass(PusheExtPath);
    }
}
