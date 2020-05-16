using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Notification Utility APIs provided by Pushe library
/// This file contains all modules and APIs needed to work with pushe.
/// </summary>
public static class PusheNotification
{
    private const string PushePath = "co.pushe.plus.Pushe";
    public static IPusheNotificationListener Listener;

    /// <summary>
    /// For API 26 and above you can create channel for your app.
    /// Please refer to android official docs for more information
    ///
    /// NOTE: If lower than 26, nothing will happen.
    /// </summary>
    public static void CreateNotificationChannel(
        string channelId,
        string channelName,
        string description = "",
        int importance = 4,
        bool enableLight = true,
        bool enableVibration = true,
        long[] vibrationLengths = null,
        bool showBadge = true,
        int ledColor = 0
    )
    {
        if (SdkLevel() >= 26)
        {
            PusheNotificationService().Call("createNotificationChannel", channelId, channelName,
                description, importance, enableLight, enableVibration, showBadge, ledColor, vibrationLengths);
        }
        else
        {
            Debug.Log("Lower than android 8.0. Channel not created!");
        }
    }

    public static void RemoveNotificationChannel(string channelId)
    {
        if (SdkLevel() >= 26)
        {
            PusheNotificationService().Call("removeNotificationChannel", channelId);
        }
        else
        {
            Debug.Log("Lower than android 8.0. No channel to remove :|");
        }
    }

    public static void EnableNotification()
    {
        PusheNotificationService().Call("enableNotifications");
    }

    public static void DisableNotification()
    {
        PusheNotificationService().Call("disableNotifications");
    }

    public static bool IsNotificationEnabled()
    {
        return PusheNotificationService().Call<bool>("isNotificationEnable");
    }

    public static void EnableCustomSound()
    {
        PusheNotificationService().Call("enableCustomSound");
    }

    public static void DisableCustomSound()
    {
        PusheNotificationService().Call("disableCustomSound");
    }

    public static bool IsCustomSoundEnabled()
    {
        return PusheNotificationService().Call<bool>("isCustomSoundEnable");
    }

    /// <summary>
    /// <b>NOTE</b>: You must add co.pushe.plus.ext.PusheUnityApplication or co.pushe.plus.ext.PusheMultiDexApplication
    ///  as your Application class in your manifest in order to get the callbacks.
    /// 
    /// If you use another custom application class, make sure to call `PusheExt.initializeNotificationListener` in it's onCreate method
    ///
    /// To use callback feature of Pushe you must implement an interface and pass it to this.
    /// Native library will call your interfaces using UnitySendMessage to PusheCallback (If default param not entered)
    /// </summary>
    /// <param name="listener">Will be the implemented code by developer to handle notification stuff</param>
    public static void SetNotificationListener(IPusheNotificationListener listener)
    {
        Listener = listener;
    }

    /// <summary>
    /// Same as `SetNotificationListener(Listener), but it will send message to a custom game object.
    /// for instance: SetNotificationListener(gameObject.name, listener); will send the message to this object.
    /// **NOTE**: PusheCallback.cs MUST be attached. Otherwise this does not work.
    /// </summary>
    public static void SetNotificationListener(string objectName, IPusheNotificationListener listener)
    {
        PusheCallback.SetCallbackGameObject(objectName);
        Listener = listener;
    }

    

    /// <summary>
    /// With this function you can send notification from user to user.
    /// </summary>
    /// <exception cref="Exception">Will be thrown if needed param was entered as a null value</exception>
    public static void SendNotificationToUser(UserNotification userNotification)
    {
        if (userNotification.Id == null || userNotification.Title == null || userNotification.Content == null)
        {
            throw new Exception("id, title and content must be set.");
        }

        var userNotifClass = new AndroidJavaClass("co.pushe.plus.notification.UserNotification");
        AndroidJavaObject userNotif;

        switch (userNotification.Type)
        {
            case UserNotification.IdType.CustomId:
                userNotif = userNotifClass.CallStatic<AndroidJavaObject>("withCustomId", userNotification.Id);
                break;
            case UserNotification.IdType.AndroidId:
                userNotif = userNotifClass.CallStatic<AndroidJavaObject>("WithAndroidId", userNotification.Id);
                break;
            case UserNotification.IdType.GoogleAdId:
                userNotif = userNotifClass.CallStatic<AndroidJavaObject>("withAdvertisementId",
                    userNotification.Id);
                break;
            default:
                throw new Exception("IdType is not valid");
        }

        userNotif = userNotif.Call<AndroidJavaObject>("setTitle", userNotification.Title)
            .Call<AndroidJavaObject>("setContent", userNotification.Content);
        if (userNotification.IconUrl != null)
        {
            userNotif = userNotif.Call<AndroidJavaObject>("setIconUrl", userNotification.IconUrl);
        }

        if (userNotification.BigTitle != null)
        {
            userNotif = userNotif.Call<AndroidJavaObject>("setBigTitle", userNotification.BigTitle);
        }

        if (userNotification.BigContent != null)
        {
            userNotif = userNotif.Call<AndroidJavaObject>("setBigContent", userNotification.BigContent);
        }

        if (userNotification.ImageUrl != null)
        {
            userNotif = userNotif.Call<AndroidJavaObject>("setImageUrl", userNotification.ImageUrl);
        }

        if (userNotification.CustomContent != null)
        {
            userNotif = userNotif.Call<AndroidJavaObject>("setCustomContent", userNotification.CustomContent);
        }

        if (userNotification.AdvancedJson != null)
        {
            userNotif = userNotif.Call<AndroidJavaObject>("setAdvancedNotification", userNotification.AdvancedJson);
        }

        if (userNotification.NotifIcon != null)
        {
            userNotif = userNotif.Call<AndroidJavaObject>("setNotifIcon", userNotification.NotifIcon);
        }

        try
        {
            PusheNotificationService().Call("sendNotificationToUser", userNotif);
        }
        catch (Exception e)
        {
            Console.WriteLine("Couldn't send a notification. Error:\n" + e.Message);
        }
    }

    // Util Methods

    private static AndroidJavaClass Pushe()
    {
        return new AndroidJavaClass(PushePath);
    }

    private static AndroidJavaObject PusheNotificationService()
    {
        return Pushe().CallStatic<AndroidJavaObject>("getPusheService", "notification");
    }

    private static int SdkLevel()
    {
        using (var version = new AndroidJavaClass("android.os.Build$VERSION"))
        {
            var sdkInt = version.GetStatic<int>("SDK_INT");
            return sdkInt;
        }
    }
}

/// <summary>
/// This class is used to handle notification management in order to be used for sending notification.
/// </summary>
public class UserNotification
{
    public readonly IdType Type;
    public readonly string Id;


    public string AdvancedJson;
    public string Title;
    public string Content;
    public string BigTitle;
    public string BigContent;
    public string ImageUrl;
    public string IconUrl;
    public string NotifIcon;
    public string CustomContent;

    private UserNotification(IdType type, string id)
    {
        Type = type;
        Id = id;
    }

    public static UserNotification WithCustomId(string customId)
    {
        return new UserNotification(IdType.CustomId, customId);
    }

    public static UserNotification WithGoogleAdvertisementId(string adId)
    {
        return new UserNotification(IdType.GoogleAdId, adId);
    }

    public static UserNotification WithAndroidId(string androidId)
    {
        return new UserNotification(IdType.AndroidId, androidId);
    }

    public UserNotification SetTitle(string title)
    {
        Title = title;
        return this;
    }

    public UserNotification SetContent(string content)
    {
        Content = content;
        return this;
    }

    public UserNotification SetBigTitle(string bigTitle)
    {
        BigTitle = bigTitle;
        return this;
    }

    public UserNotification SetBigContent(string bigContent)
    {
        BigContent = bigContent;
        return this;
    }

    public UserNotification SetImageUrl(string imageUrl)
    {
        ImageUrl = imageUrl;
        return this;
    }

    public UserNotification SetIconUrl(string iconUrl)
    {
        IconUrl = iconUrl;
        return this;
    }

    public UserNotification SetNotifIcon(string notifIcon)
    {
        NotifIcon = notifIcon;
        return this;
    }

    public UserNotification SetCustomContent(string customContent)
    {
        CustomContent = customContent;
        return this;
    }

    public UserNotification SetAdvancedNotification(string advancedNotificationJson)
    {
        AdvancedJson = advancedNotificationJson;
        return this;
    }

    public enum IdType
    {
        CustomId,
        GoogleAdId,
        AndroidId
    }
}

/// <summary>
/// A class to hold data received from a FCM notification.
/// </summary>
[Serializable]
public class NotificationData
{
    public string messageId,
        title,
        content,
        bigTitle,
        bigContent,
        summary,
        imageUrl,
        iconUrl,
        bigIconUrl,
        customContent;

    public NotificationButtonData[] buttons;


    public override string ToString()
    {
        return "\n" + messageId + "/" +
               title + "/" +
               content + "/" +
               bigTitle + "/" +
               bigContent + "/" +
               summary + "/" +
               imageUrl + "/" +
               iconUrl + "/" +
               bigIconUrl + "/" +
               customContent + "/" +
               buttons;
    }
}

[Serializable]
public class NotificationButtonData
{
    public string id, text, icon;

    public override string ToString()
    {
        return id + "/" + text + "/" + icon;
    }
}