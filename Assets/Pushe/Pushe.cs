using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;


/// <summary>
/// For more and detailed information and documentations please checkout <seealso ref="https://pushe.co/docs/"/>
/// </summary>
public static class Pushe
{
    private const string PushePath = "co.pushe.plus.Pushe";


    /// <summary>
    /// Check if pushe is registered to FCM
    /// </summary>
    public static bool IsRegistered()
    {
        return PusheNative().CallStatic<bool>("isRegistered");
    }

    /// <summary>
    /// Returns true if all pushe modules Core, notification, etc.) were initialized.
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
    public static string GetAndroidId()
    {
        return PusheNative().CallStatic<string>("getAndroidId");
    }

    /// <summary>
    /// Please refer to https://pushe.co/docs/ for more information about pushe id.
    /// </summary>
    /// <returns></returns>
    [Obsolete("GetPusheId is deprecated, please use GetAndroidId or GetGoogleAdvertisingId instead.")]
    public static string GetPusheId()
    {
        return PusheNative().CallStatic<string>("getPusheId");
    }

    public static void SubscribeTo(string topic)
    {
        Debug.Log("Subscribing to topic...");
        PusheNative().CallStatic("subscribeToTopic", topic, null);
    }

    public static void UnsubscribeFrom(string topic)
    {
        Debug.Log("Unsubscribe from topic...");
        PusheNative().CallStatic("unsubscribeFromTopic", topic, null);
    }

    public static void SetCustomId(string id)
    {
        Debug.Log("Setting custom id");
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

    public static void AddTag(string tag)
    {
        PusheNative().CallStatic("addTag", tag);
    }

    public static void RemoveTag(string tag)
    {
        PusheNative().CallStatic("removeTag", tag);
    }

    private static AndroidJavaClass PusheNative()
    {
        return new AndroidJavaClass(PushePath);
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