using System.Collections.Generic;
using Pushe.android;
using UnityEngine;

namespace Pushe
{
    public static class PusheUnity
    {
        //////

        public static void Initialize() {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheCore.Initialize();
    #elif UNITY_IOS && !UNITY_EDITOR
            PusheiOSUnityInterface.Initialize();
    #endif
        }

        public static bool IsInitialized() {
    #if UNITY_ANDROID && !UNITY_EDITOR
            return PusheCore.IsInitialized(); // Not registration
    #elif UNITY_IOS && !UNITY_EDITOR
            return true;
    #endif
            return false;
        }

        public static bool IsRegistered() {
    #if UNITY_ANDROID && !UNITY_EDITOR
            return PusheCore.IsRegistered();
    #elif UNITY_IOS && !UNITY_EDITOR
            return PusheiOSUnityInterface.IsRegistered();
    #endif
            return false;
        }

        public static void OnPusheInitialized(PusheDelegate onInitialized) {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheCore.OnPusheInitialized(onInitialized);
    #elif UNITY_IOS && !UNITY_EDITOR
            // not implemented
    #endif
        }

        public static void OnPusheRegistered(PusheDelegate onRegistered) {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheCore.OnPusheRegistered(onRegistered);
    #elif UNITY_IOS && !UNITY_EDITOR
            // not implemented
    #endif
        }
        
        public static string GetDeviceId() {
    #if UNITY_ANDROID && !UNITY_EDITOR
            return PusheCore.GetDeviceId();
    #elif UNITY_IOS && !UNITY_EDITOR
            return PusheiOSUnityInterface.GetDeviceId();
    #endif
            return null;
        }

        public static string GetGoogleAdvertisingId() {
    #if UNITY_ANDROID && !UNITY_EDITOR
            return PusheCore.GetGoogleAdvertisingId();
    #elif UNITY_IOS && !UNITY_EDITOR
            return PusheiOSUnityInterface.GetAdvertisingId();
    #endif
            return null;
        }
        public static string GetAdvertisingId() {
    #if UNITY_ANDROID && !UNITY_EDITOR
            return PusheCore.GetAdvertisingId();
    #elif UNITY_IOS && !UNITY_EDITOR
            return PusheiOSUnityInterface.GetAdvertisingId();
    #endif
            return null;
        }

        public static void SetCustomId(string customId) {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheCore.SetCustomId(customId);
    #elif UNITY_IOS && !UNITY_EDITOR
            PusheiOSUnityInterface.SetCustomId(customId);
    #endif
        }

        public static string GetCustomId() {
    #if UNITY_ANDROID && !UNITY_EDITOR
            return PusheCore.GetCustomId();
    #elif UNITY_IOS && !UNITY_EDITOR
            return PusheiOSUnityInterface.GetCustomId();
    #endif
            return null;
        }

        public static bool SetUserEmail(string email) {
    #if UNITY_ANDROID && !UNITY_EDITOR
            return PusheCore.SetUserEmail(email);
    #elif UNITY_IOS && !UNITY_EDITOR
            return PusheiOSUnityInterface.SetUserEmail(email);
    #endif
            return false;
        }

        public static string GetUserEmail() {
    #if UNITY_ANDROID && !UNITY_EDITOR
            return PusheCore.GetUserEmail();
    #elif UNITY_IOS && !UNITY_EDITOR
            return PusheiOSUnityInterface.GetUserEmail();
    #endif
            return null;
        }

        public static bool SetUserPhoneNumber(string number) {
    #if UNITY_ANDROID && !UNITY_EDITOR
            return PusheCore.SetUserPhoneNumber(number);
    #elif UNITY_IOS && !UNITY_EDITOR
            return PusheiOSUnityInterface.SetUserPhoneNumber(number);
    #endif
            return false;
        }

        public static string GetUserPhoneNumber() {
    #if UNITY_ANDROID && !UNITY_EDITOR
            return PusheCore.GetUserPhoneNumber();
    #elif UNITY_IOS && !UNITY_EDITOR
            return PusheiOSUnityInterface.GetUserPhoneNumber();
    #endif
            return null;
        }

        public static void AddTags(Dictionary<string, string> tags) {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheCore.AddTags(tags);
    #elif UNITY_IOS && !UNITY_EDITOR
            PusheiOSUnityInterface.AddTags(tags);
    #endif
        }

        public static void RemoveTags(params string[] keys) {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheCore.RemoveTags(keys);
    #elif UNITY_IOS && !UNITY_EDITOR
            PusheiOSUnityInterface.RemoveTags(keys);
    #endif
        }

        public static Dictionary<string, string> GetSubscribedTags() {
    #if UNITY_ANDROID && !UNITY_EDITOR
            return new Dictionary<string, string>{ {"tags", PusheCore.GetSubscribedTags()} };
    #elif UNITY_IOS && !UNITY_EDITOR
            return PusheiOSUnityInterface.GetSubscribedTags();
    #endif
            return null;
        }

        public static void Subscribe(string topic) {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheCore.SubscribeTo(topic);
    #elif UNITY_IOS && !UNITY_EDITOR
            PusheiOSUnityInterface.Subscribe(topic);
    #endif
        }

        public static void Unsubscribe(string topic) {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheCore.UnsubscribeFrom(topic);
    #elif UNITY_IOS && !UNITY_EDITOR
            PusheiOSUnityInterface.Unsubscribe(topic);
    #endif
        }

        public static string[] GetSubscribedTopics() {
    #if UNITY_ANDROID && !UNITY_EDITOR
            return PusheCore.GetSubscribedTopics();
    #elif UNITY_IOS && !UNITY_EDITOR
            return PusheiOSUnityInterface.GetSubscribedTopics();
    #endif
            return null;
        }

        public static void EnableNotifications() {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheNotification.EnableNotification();
    #elif UNITY_IOS && !UNITY_EDITOR
            // not implemented
    #endif
        }

        public static void DisableNotifications() {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheNotification.DisableNotification();
    #elif UNITY_IOS && !UNITY_EDITOR
            // not implemented
    #endif
        }

        public static bool IsNotificationsEnabled() {
    #if UNITY_ANDROID && !UNITY_EDITOR
            return PusheNotification.IsNotificationEnabled();
    #elif UNITY_IOS && !UNITY_EDITOR
            // not implemented
            return true;
    #endif
            return false;
        }

        public static void EnableCustomSound() {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheNotification.EnableCustomSound();
    #elif UNITY_IOS && !UNITY_EDITOR
            // not implemented
    #endif
        }

        public static void DisableCustomSound() {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheNotification.DisableCustomSound();
    #elif UNITY_IOS && !UNITY_EDITOR
            // not implemented
    #endif
        }

        public static bool IsCustomSoundEnabled() {
    #if UNITY_ANDROID && !UNITY_EDITOR
            return PusheNotification.IsCustomSoundEnabled();
    #elif UNITY_IOS && !UNITY_EDITOR
            // not implemented
            return true;
    #endif
            return false;
        }

        public static void CreateNotificationChannel(
            string channelId,
            string channelName,
            string description = "",
            int importance = 4,
            bool enableLight = true,
            bool enableVibration = true,
            long[] vibrationLengths = null,
            bool showBadge = true,
            int ledColor = 0)
        {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheNotification.CreateNotificationChannel(channelId, channelName, description, importance, enableLight, enableVibration, vibrationLengths, showBadge, ledColor);
    #elif UNITY_IOS && !UNITY_EDITOR
            // not implemented
    #endif
        }

        public static void RemoveNotificationChannel(string channelId) {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheNotification.RemoveNotificationChannel(channelId);
    #elif UNITY_IOS && !UNITY_EDITOR
            // not implemented
    #endif
        }

        public static void SendNotificationToUser(UserNotification notification) {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheNotification.SendNotificationToUser(notification);
    #elif UNITY_IOS && !UNITY_EDITOR
            // not implemented
    #endif
        }

        public static void SetNotificationListener(IPusheNotificationListener listener) {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheNotification.SetNotificationListener(listener);
    #elif UNITY_IOS && !UNITY_EDITOR
            // not implemented
    #endif
        }

        public static void SendEvent(string eventName) {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheAnalytics.SendEvent(eventName);
    #elif UNITY_IOS && !UNITY_EDITOR
            // not implemented
    #endif
        }
        
        // SendEvent(Event)
        
        public static void SendEcommerceData(string name, double price, string category = null, long quantity = -1) {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheAnalytics.SendEcommerceData(name, price, category, quantity);
    #elif UNITY_IOS && !UNITY_EDITOR
            // not implemented
    #endif
        }
        
        public static void TriggerEvent(string eventName) {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheInAppMessaging.TriggerEvent(eventName);
    #elif UNITY_IOS && !UNITY_EDITOR
            // not implemented
    #endif
        }

        public static void EnableInAppMessaging() {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheInAppMessaging.EnableInAppMessaging();
    #elif UNITY_IOS && !UNITY_EDITOR
            // not implemented
    #endif
        }

        public static void DisableInAppMessaging() {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheInAppMessaging.DisableInAppMessaging();
    #elif UNITY_IOS && !UNITY_EDITOR
            // not implemented
    #endif
        }

        public static bool IsInAppMessagingEnabled() {
    #if UNITY_ANDROID && !UNITY_EDITOR
            return PusheInAppMessaging.IsInAppMessagingEnabled();
    #elif UNITY_IOS && !UNITY_EDITOR
            // not implemented
            return false;
    #endif
            return false;
        }

        public static void SetInAppMessagingListener(IPusheInAppMessagingListener listener) {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheInAppMessaging.SetInAppMessagingListener(listener);
    #elif UNITY_IOS && !UNITY_EDITOR
            // not implemented
    #endif
        }

        public static void TestInAppMessaging(string inAppMessage) {
    #if UNITY_ANDROID && !UNITY_EDITOR
            PusheInAppMessaging.TestInAppMessage(inAppMessage);
    #elif UNITY_IOS && !UNITY_EDITOR
            // not implemented
    #endif
        }
        
        public static void Log(string message)
        {
            Debug.Log("Pushe [Unity]: " + message);
        }
        
        // Used when registered
        public delegate void PusheDelegate();
    }
}