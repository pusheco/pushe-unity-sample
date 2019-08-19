using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Pushe 
{
    /// <summary>
    /// Pushe SDK utility script.
    ///
    /// <b>Usage:</b>
    ///     You can simply drag this script to your own game object (like the MainCamera) in order to Initialize Pushe on your device.
    ///     Or you can use all functions statically.
    ///     To initialize pushe in your own script call <code>Pushe.Initialize()</code>
    ///
    /// <b>Note:</b>
    ///     Since it's using `AndroidJavaObject` and `AndroidJavaClass`, any problem can cause the calling to throw an exception.
    ///     So it's recommended to wrap all usages in try/catch block and catch the exception.
    ///
    /// </summary>
    [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
    public class Pushe : MonoBehaviour
    {

        private void Start()
        {
            try
            {
                Initialize();
            }
            catch (Exception e)
            {
                Debug.Log("Failed to initialize Pushe. Error: " + e);
                Debug.Log("Error StackTrace: " + e.StackTrace);
            }
        }

        
        // ------ SDK methods ------

        public static void Initialize(bool showGooglePlayDialog = true)
        {
            var activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            //getting context of unity activity
            var activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            //calling plugin class by package name
            var pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
            //calling initialize static method
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
         * Call for unSubscribing from a topic. It has to be called after Pushe.initialize() has completed its work
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
        public static void SetNotificationOn()
        {
            var activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            var pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
            if (pluginClass != null)
            {
                pluginClass.CallStatic("setNotificationOn", context);
            }
        }

        /**
         * Call this method to disable publishing notification to user.
         * To enable showing notifications again, you need to call SetNotificationOn()
         **/
        public static void SetNotificationOff()
        {
            var activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            var pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
            if (pluginClass != null)
            {
                pluginClass.CallStatic("setNotificationOff", context);
            }
        }

        /**
         * Call this method to check if pushe is initialized.
         * It is needed before call to un/subscribe, and sendNotif to user methods
         **/
        public static bool IsPusheInitialized()
        {
            var activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            var pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
            return pluginClass != null && pluginClass.CallStatic<bool>("isPusheInitialized", context);
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
            return pluginClass != null ? pluginClass.CallStatic<string>("getPusheId", context) : "";
        }

        /**
         * Call this method to send simple notification from client to another client.
         **/
        public static void SendSimpleNotifToUser(string userPusheId, string title, string content)
        {
            var activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            var pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
            if (pluginClass != null)
            {
                pluginClass.CallStatic("sendSimpleNotifToUser", context, userPusheId, title, content);
            }
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
            if (pluginClass != null)
            {
                pluginClass.CallStatic("sendAdvancedNotifToUser", context, userPusheId, notificationJson);
            }
        }

        /**
         * Create a custom notification channel. This method works for android 8+
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
            if (pluginClass != null)
            {
                pluginClass.CallStatic("createNotificationChannel", context, channelId, channelName, description, importance, enableLight, enableVibration, showBadge, ledColor, vibrationPattern);
            }
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
            if (pluginClass != null)
            {
                pluginClass.CallStatic("removeNotificationChannel", context, channelId);
            }
        }
    }
}