using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Pushe.android
{
    /// <summary>
    /// For more and detailed information and documentations please checkout <seealso ref="https://docs.pushe.co"/>
    /// </summary>
    public static class PusheCore
    {
        /// <summary>
        /// GDPR related.
        /// If the user consent was given about necessary data collection for Pushe,
        ///     use this function to let pushe registration begin.
        /// To be able to use GDPR, add "pushe_requires_user_consent" meta-data value to "true",
        /// After showing the dialog and getting user's consent, call this onAccept.
        /// NOTE: Calling this only once is enough.
        /// </summary>
        public static void Initialize()
        {
            PusheAndroidUtils.Native().CallStatic("initialize");
        }

        /// Set user's consent
        public static void SetUserConsentGiven()
        {
            PusheAndroidUtils.Native().CallStatic("setUserConsentGiven");
        }

        /// <summary>
        /// Check if pushe is registered to FCM
        /// </summary>
        public static bool IsRegistered()
        {
            return PusheAndroidUtils.Native().CallStatic<bool>("isRegistered");
        }

        /// <summary>
        /// Returns true if all pushe modules (Core, notification, etc.) were initialized.
        /// </summary>
        public static bool IsInitialized()
        {
            return PusheAndroidUtils.Native().CallStatic<bool>("isInitialized");
        }

        /**
        Simply pass a void no argument function to this function to handle registration
     */
        public static void OnPusheRegistered(PusheUnity.PusheDelegate pusheCallback)
        {
            PusheAndroidUtils.Native().CallStatic("setRegistrationCompleteListener", new RegisterCallback(pusheCallback));
        }

        /// <summary>
        /// Simply pass a void no argument function to this to handle initialization.
        ///
        /// * NOTE: This is not like Pushe.initialize() from Pushe 1.x. This is different.
        /// </summary>
        /// <param name="initCallback"></param>
        public static void OnPusheInitialized(PusheUnity.PusheDelegate initCallback)
        {
            PusheAndroidUtils.Native().CallStatic("setInitializationCompleteListener", new RegisterCallback(initCallback));
        }

        /// <summary>
        /// Get google advertising id
        /// </summary>
        /// <returns>Null if this feature was disabled by user and true otherwise</returns>
        [Obsolete("Use GetAdvertisingId instead")]
        public static string GetGoogleAdvertisingId()
        {
            return PusheAndroidUtils.Native().CallStatic<string>("getGoogleAdvertisingId");
        }
        
        /// <summary>
        /// Get device advertising id (Google: Google Ad id, Huawei: Huawei OAID)
        /// </summary>
        /// <returns>Null if this feature was disabled by user and true otherwise</returns>
        public static string GetAdvertisingId()
        {
            return PusheAndroidUtils.Native().CallStatic<string>("getAdvertisingId");
        }

        /// <summary>
        /// Returns unique id of device
        /// Following ID makes a unique ID that:
        /// - (Android 8.0 or higher): Is unique in an app and will be different on another app.
        /// - (Android lower that 8.0): Is a unique ID for all apps and each device will have only one id.
        /// </summary>
        public static string GetDeviceId()
        {
            return PusheAndroidUtils.Native().CallStatic<string>("getDeviceId");
        }

        public static void SubscribeTo(string topic)
        {
            PusheAndroidUtils.Native().CallStatic("subscribeToTopic", topic, null);
        }

        public static void UnsubscribeFrom(string topic)
        {
            PusheAndroidUtils.Native().CallStatic("unsubscribeFromTopic", topic, null);
        }

        public static string[] GetSubscribedTopics()
        {
            return PusheAndroidUtils.Extension().CallStatic<string>("getSubscribedTopicsCsv").Split(',');
        }

        public static void SetCustomId(string id)
        {
            PusheAndroidUtils.Native().CallStatic("setCustomId", id);
        }

        public static string GetCustomId()
        {
            return PusheAndroidUtils.Native().CallStatic<string>("getCustomId");
        }

        public static bool SetUserEmail(string email)
        {
            return PusheAndroidUtils.Native().CallStatic<bool>("setUserEmail", email);
        }

        public static string GetUserEmail()
        {
            return PusheAndroidUtils.Native().CallStatic<string>("getUserEmail");
        }

        public static bool SetUserPhoneNumber(string phone)
        {
            return PusheAndroidUtils.Native().CallStatic<bool>("setUserPhoneNumber", phone);
        }

        public static string GetUserPhoneNumber()
        {
            return PusheAndroidUtils.Native().CallStatic<string>("getUserPhoneNumber");
        }

        public static void AddTags(IDictionary<string, string> tags)
        {
            var mapOfTags = PusheAndroidUtils.CreateJavaMapFromDictionary(tags);
            PusheAndroidUtils.Native().CallStatic("addTags", mapOfTags);
        }

        public static void RemoveTags(params string[] tags)
        {
            var tagsToRemove = PusheAndroidUtils.CreateJavaArrayList(tags);
            PusheAndroidUtils.Native().CallStatic("removeTags", tagsToRemove);
        }

        public static string GetSubscribedTags()
        {
            
            return PusheAndroidUtils.Extension().CallStatic<string>("getSubscribedTagsJson");
        }

        
    }


    

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class RegisterCallback : AndroidJavaProxy
    {
        private readonly PusheUnity.PusheDelegate pusheSuccessDelegate;

        public RegisterCallback(PusheUnity.PusheDelegate pushe) : base("co.pushe.plus.Pushe$Callback")
        {
            pusheSuccessDelegate = pushe;
        }

        public void onComplete()
        {
            pusheSuccessDelegate();
        }
    }
}