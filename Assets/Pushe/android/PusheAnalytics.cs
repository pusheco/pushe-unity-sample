using UnityEngine;

namespace Pushe.android
{
    public static class PusheAnalytics
    {


        /// <summary>
        ///  Send an event to the server. You can pass push notifications according to happening on some events.
        /// </summary>
        /// <param name="eventName">A name chosen by you</param>
        /// <b>If You don't have and action, leave it be custom.</b>
        public static void SendEvent(string eventName)
        {
            
            PusheAndroidUtils.PusheAnalyticsService().Call("sendEvent", eventName);
        }

        public static void SendEcommerceData(string name, double price, string category = null, long quantity = -1)
        {
            PusheAndroidUtils.Extension("analytics").CallStatic("sendEcommerce", name, price, category, quantity);
        }

        

    }
}
