using UnityEngine;

namespace Pushe
{
    public class Pushe : MonoBehaviour
    {
        private static readonly bool showGooglePlayDialog = true; //if it is true, user will see a dialog for installing GooglePlayService if it is not installed on her/his device

        private void Start()
        {
            try
            {
                InitializePushe();
            }
            catch
            {
                Debug.Log("Failed to initialize the project somehow!");
            }
        }

        private static void InitializePushe()
        {
            var activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            //getting context of unity activity
            var activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            //calling plugin class by package name
            var pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
            pluginClass.CallStatic("initialize", activityContext, showGooglePlayDialog);
        }

        /**
         * Call for subscribing to a topic. It has to be called after Pushe.initialize() has completed its work
         * So, call it with a reasonable delay (30 sec to 2 min) after Pushe.initialize()
         **/
        public static void Subscribe(string topic)
        {
            var activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            var pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
            pluginClass.CallStatic("subscribe", context, topic);
        }

        /**
         * Call for unsubscribing from a topic. It has to be called after Pushe.initialize() has completed its work
         * So, call it with a reasonable delay (30 sec to 2 min) after Pushe.initialize()
         **/
        public static void Unsubscribe(string topic)
        {
            var activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            var pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
            pluginClass.CallStatic("unsubscribe", context, topic);
        }

        /**
         * Call this method to enable publishing notification to user, if you already called SetNotificationOff()
         **/
        public static void NotificationOn()
        {
            var activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            var pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
            pluginClass.CallStatic("setNotificationOn", context);
        }

        /**
         * Call this method to disable publishing notification to user.
         * To enable showing notifications again, you need to call SetNotificationOn()
         **/
        public static void NotificationOff()
        {
            var activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            var pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
            pluginClass.CallStatic("setNotificationOff", context);
        }

        /**
         * Call this method to check if pushe is initialized.
         * It is needed before call to un/subscribe, and sendNotif to user methods
         **/
        public static bool PusheIsInitialized()
        {
            var activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            var pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
            return pluginClass.CallStatic<bool>("isPusheInitialized", context);
        }

        /**
         * Call this method to get this device pusheId.
         * It is needed for call to and sendNotif to user methods
         **/
        public static string GetPusheId()
        {
            var activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            var pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
            return pluginClass.CallStatic<string>("getPusheId", context);
        }

        /**
         * Call this method to send simple notification from client to another client.
         **/
        public static void SendSimpleNotifToUser(string userPusheId, string title, string content)
        {
            var activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            var pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
            pluginClass.CallStatic("sendSimpleNotifToUser", context, userPusheId, title, content);
        }

        /**
         * Call this method to send advanced notification from client to another client.
         * You need to prepare advanced notification as a valid json string.
         **/
        public static void SendAdvancedNotifToUser(string userPusheId, string notificationJson)
        {
            var activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            var pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
            pluginClass.CallStatic("sendAdvancedNotifToUser", context, userPusheId, notificationJson);
        }

        /**
         * Call this method to send any content you like to another client.
         * You need to prepare this content as a valid json string.
         **/
        public static void SendCustomJsonToUser(string userPusheId, string customJson)
        {
            var activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            var pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
            pluginClass.CallStatic("sendCustomJsonToUser", context, userPusheId, customJson);
        }

        /**
         * Create a custom notification channel. This methos works for android 8+
         * On lower android version, call to this method has no effect
         **/
        public static void CreateNotificationChannel(string channelId, string channelName,
            string description, int importance,
            bool enableLight, bool enableVibration,
            bool showBadge, int ledColor, long[] vibrationPattern)
        {
            var activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            var pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
            pluginClass.CallStatic("createNotificationChannel", context, channelId, channelName, description, importance, enableLight, enableVibration, showBadge, ledColor, vibrationPattern);
        }

        /**
         * Remove a custom notification channel. This methos works for android 8+
         * On lower android version, call to this method has no effect
         **/
        public static void RemoveNotificationChannel(string channelId)
        {
            var activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            var pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
            pluginClass.CallStatic("removeNotificationChannel", context, channelId);
        }
    }
}