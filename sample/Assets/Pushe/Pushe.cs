using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// For more and detailed information and documentations please checkout <seealso ref="https://pushe.co/docs/"/>
/// </summary>
public static class Pushe
{
    private const string PushePath = "co.pushe.plus.Pushe";
    // Some additional functions for Pushe SDK
    private const string PusheExtPath = "co.pushe.plus.ext.PusheExt";

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
        PusheNative().CallStatic("initialize");
    }

    /// <summary>
    /// Set user's consent
    /// <summary>
    public static void setUserConsentGiven()
    {
        PusheNative().CallStatic("setUserConsentGiven");
    }

    /// <summary>
    /// Check if pushe is registered to FCM
    /// </summary>
    public static bool IsRegistered()
    {
        return PusheNative().CallStatic<bool>("isRegistered");
    }

    /// <summary>
    /// Returns true if all pushe modules (Core, notification, etc.) were initialized.
    /// </summary>
    public static bool IsInitialized()
    {
        return PusheNative().CallStatic<bool>("isInitialized");
    }

    /**
        Simply pass a void no argument function to this function to handle registration
     */
    public static void OnPusheRegistered(RegisterDelegate registerCallback)
    {
        PusheNative().CallStatic("setRegistrationCompleteListener", new RegisterCallback(registerCallback));
    }

    /// <summary>
    /// Simply pass a void no argument function to this to handle initialization.
    ///
    /// * NOTE: This is not like Pushe.initialize() from Pushe 1.x. This is different.
    /// </summary>
    /// <param name="initCallback"></param>
    public static void OnPusheInitialized(RegisterDelegate initCallback)
    {
        PusheNative().CallStatic("setInitializationCompleteListener", new RegisterCallback(initCallback));
    }

    /// <summary>
    /// Get google advertising id
    /// </summary>
    /// <returns>Null if this feature was disabled by user and true otherwise</returns>
    public static string GetGoogleAdvertisingId()
    {
        return PusheNative().CallStatic<string>("getGoogleAdvertisingId");
    }

    /// <summary>
    /// Returns android id of device
    /// </summary>
    [Obsolete("GetAndroidId is deprecated, please use GetDeviceId instead.")]
    public static string GetAndroidId()
    {
        return PusheNative().CallStatic<string>("getAndroidId");
    }

    /// <summary>
    /// Returns unique id of device
    /// Following ID makes a unique ID that:
    /// - (Android 8.0 or higher): Is unique in an app and will be different on another app.
    /// - (Android lower that 8.0): Is a unique ID for all apps and each device will have only one id.
    /// </summary>
    public static string GetDeviceId()
    {
        return PusheNative().CallStatic<string>("getDeviceId");
    }

    public static void SubscribeTo(string topic)
    {
        PusheNative().CallStatic("subscribeToTopic", topic, null);
    }

    public static void UnsubscribeFrom(string topic)
    {
        Debug.Log("Unsubscribe from topic...");
        PusheNative().CallStatic("unsubscribeFromTopic", topic, null);
    }
    
    public static string[] GetSubscribedTopics()
    {
        return PusheExt().CallStatic<string>("getSubscribedTopicsCsv").Split(',');
    }

    public static void SetCustomId(string id)
    {
        PusheNative().CallStatic("setCustomId", id);
    }

    public static string GetCustomId()
    {
        return PusheNative().CallStatic<string>("getCustomId");
    }

    public static bool SetUserEmail(string email)
    {
        return PusheNative().CallStatic<bool>("setUserEmail", email);
    }

    public static string GetUserEmail()
    {
        return PusheNative().CallStatic<string>("getUserEmail");
    }

    public static bool SetUserPhoneNumber(string phone)
    {
        return PusheNative().CallStatic<bool>("setUserPhoneNumber", phone);
    }

    public static string GetUserPhoneNumber()
    {
        return PusheNative().CallStatic<string>("getUserPhoneNumber");
    }

    public static void AddTags(IDictionary<string, string> tags)
    {
        var mapOfTags = CreateJavaMapFromDictionary(tags);
        PusheNative().CallStatic("addTags", mapOfTags);
    }

    public static void RemoveTag(params string[] tags)
    {
        var tagsToRemove = CreateJavaArrayList(tags);
        PusheNative().CallStatic("removeTags", tagsToRemove);
    }

    public static string GetSubscribedTags()
    {
        return PusheExt().CallStatic<string>("getSubscribedTagsJson");
    }

    // Private section

    private static AndroidJavaClass PusheNative()
    {
        return new AndroidJavaClass(PushePath);
    }

    private static AndroidJavaObject CreateJavaArrayList(params string[] elements)
    {
        var list = new AndroidJavaObject("java.util.ArrayList");
        foreach (var element in elements)
        {
            list.Call<bool>("add", element);
        }

        return list;
    }

    private static AndroidJavaObject CreateJavaMapFromDictionary(IDictionary<string, string> parameters)
    {
        var javaMap = new AndroidJavaObject("java.util.HashMap");
        var putMethod = AndroidJNIHelper.GetMethodID(
            javaMap.GetRawClass(), "put",
            "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");

        var args = new object[2];
        foreach (var kvp in parameters)
        {
            using (var k = new AndroidJavaObject(
                "java.lang.String", kvp.Key))
            {
                using (var v = new AndroidJavaObject(
                    "java.lang.String", kvp.Value))
                {
                    args[0] = k;
                    args[1] = v;
                    AndroidJNI.CallObjectMethod(javaMap.GetRawObject(),
                        putMethod, AndroidJNIHelper.CreateJNIArgArray(args));
                }
            }
        }

        return javaMap;
    }

    public static void Log(string message)
    {
        Debug.Log("Pushe [Unity]: " + message);
    }

    public static AndroidJavaClass PusheExt()
    {
        return new AndroidJavaClass(PusheExtPath);
    }
}


public delegate void RegisterDelegate();

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class RegisterCallback : AndroidJavaProxy
{
    private readonly RegisterDelegate _registerSuccessDelegate;

    public RegisterCallback(RegisterDelegate register) : base("co.pushe.plus.Pushe$Callback")
    {
        _registerSuccessDelegate = register;
    }

    public void onComplete()
    {
        _registerSuccessDelegate();
    }
}