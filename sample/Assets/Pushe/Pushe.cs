using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// For more and detailed information and documentations please checkout <seealso ref="https://docs.pushe.co"/>
/// </summary>
public static class Pushe
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
        PusheUtils.Native().CallStatic("initialize");
    }

    /// <summary>
    /// Set user's consent
    /// <summary>
    public static void setUserConsentGiven()
    {
        PusheUtils.Native().CallStatic("setUserConsentGiven");
    }

    /// <summary>
    /// Check if pushe is registered to FCM
    /// </summary>
    public static bool IsRegistered()
    {
        return PusheUtils.Native().CallStatic<bool>("isRegistered");
    }

    /// <summary>
    /// Returns true if all pushe modules (Core, notification, etc.) were initialized.
    /// </summary>
    public static bool IsInitialized()
    {
        return PusheUtils.Native().CallStatic<bool>("isInitialized");
    }

    /**
        Simply pass a void no argument function to this function to handle registration
     */
    public static void OnPusheRegistered(RegisterDelegate registerCallback)
    {
        PusheUtils.Native().CallStatic("setRegistrationCompleteListener", new RegisterCallback(registerCallback));
    }

    /// <summary>
    /// Simply pass a void no argument function to this to handle initialization.
    ///
    /// * NOTE: This is not like Pushe.initialize() from Pushe 1.x. This is different.
    /// </summary>
    /// <param name="initCallback"></param>
    public static void OnPusheInitialized(RegisterDelegate initCallback)
    {
        PusheUtils.Native().CallStatic("setInitializationCompleteListener", new RegisterCallback(initCallback));
    }

    /// <summary>
    /// Get google advertising id
    /// </summary>
    /// <returns>Null if this feature was disabled by user and true otherwise</returns>
    public static string GetGoogleAdvertisingId()
    {
        return PusheUtils.Native().CallStatic<string>("getGoogleAdvertisingId");
    }

    /// <summary>
    /// Returns android id of device
    /// </summary>
    [Obsolete("GetAndroidId is deprecated, please use GetDeviceId instead.")]
    public static string GetAndroidId()
    {
        return PusheUtils.Native().CallStatic<string>("getAndroidId");
    }

    /// <summary>
    /// Returns unique id of device
    /// Following ID makes a unique ID that:
    /// - (Android 8.0 or higher): Is unique in an app and will be different on another app.
    /// - (Android lower that 8.0): Is a unique ID for all apps and each device will have only one id.
    /// </summary>
    public static string GetDeviceId()
    {
        return PusheUtils.Native().CallStatic<string>("getDeviceId");
    }

    public static void SubscribeTo(string topic)
    {
        PusheUtils.Native().CallStatic("subscribeToTopic", topic, null);
    }

    public static void UnsubscribeFrom(string topic)
    {
        PusheUtils.Native().CallStatic("unsubscribeFromTopic", topic, null);
    }
    
    public static string[] GetSubscribedTopics()
    {
        return PusheUtils.Extension().CallStatic<string>("getSubscribedTopicsCsv").Split(',');
    }

    public static void SetCustomId(string id)
    {
        PusheUtils.Native().CallStatic("setCustomId", id);
    }

    public static string GetCustomId()
    {
        return PusheUtils.Native().CallStatic<string>("getCustomId");
    }

    public static bool SetUserEmail(string email)
    {
        return PusheUtils.Native().CallStatic<bool>("setUserEmail", email);
    }

    public static string GetUserEmail()
    {
        return PusheUtils.Native().CallStatic<string>("getUserEmail");
    }

    public static bool SetUserPhoneNumber(string phone)
    {
        return PusheUtils.Native().CallStatic<bool>("setUserPhoneNumber", phone);
    }

    public static string GetUserPhoneNumber()
    {
        return PusheUtils.Native().CallStatic<string>("getUserPhoneNumber");
    }

    public static void AddTags(IDictionary<string, string> tags)
    {
        var mapOfTags = PusheUtils.CreateJavaMapFromDictionary(tags);
        PusheUtils.Native().CallStatic("addTags", mapOfTags);
    }

    public static void RemoveTag(params string[] tags)
    {
        var tagsToRemove = PusheUtils.CreateJavaArrayList(tags);
        PusheUtils.Native().CallStatic("removeTags", tagsToRemove);
    }

    public static string GetSubscribedTags()
    {
        return PusheUtils.Extension().CallStatic<string>("getSubscribedTagsJson");
    }

    public static void Log(string message)
    {
        Debug.Log("Pushe [Unity]: " + message);
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