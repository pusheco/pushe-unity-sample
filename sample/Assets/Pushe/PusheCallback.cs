using System;
using UnityEngine;

// ReSharper disable CheckNamespace

/// <inheritdoc />
/// <summary>
/// Callback scripts are handled here.
/// In order to work this script must be attached to a game object (Which already is, to Pushe.prefab)
/// It will be called by PusheCallback when a message was arrived.
///
/// NOTE: This script is connected to the android native code using it's names. DO NOT rename the script and method names.
/// 
/// NOTE: Callback will not work if the app was not on foreground. The unity engine won't work on background.
/// This class doesn't need to be modified by the user.
/// </summary>
public class PusheCallback : MonoBehaviour
{

    /// <summary>
    /// If you are not willing to override your application class for any reasons, you can call this function instead.
    /// NOTE: The callbacks will be called once this function gets called and app is not closed.
    /// NOTE: If you call this, don't override your application class, otherwise you'll get callbacks two times for each event.
    /// </summary>
    public static void InitializeNotificationListeners()
    {
        Pushe.PusheExt().CallStatic("initializeNotificationListener");
    }
    
    public void OnNotification(string notificationData)
    {
        Pushe.Log(notificationData);
        var data = JsonUtility.FromJson<NotificationData>(notificationData);
        if (PusheNotification.Listener != null)
        {
            PusheNotification.Listener.OnNotification(data);
        }
    }

    public void OnCustomContentNotification(string notificationData)
    {
        Pushe.Log(notificationData);
        var data = JsonUtility.FromJson<NotificationData>(notificationData);
        if (PusheNotification.Listener == null) return;
        PusheNotification.Listener.OnCustomContentReceived(data.customContent);
    }

    public void OnNotificationClick(string notificationData)
    {
        Pushe.Log(notificationData);
        var data = JsonUtility.FromJson<NotificationData>(notificationData);
        if (PusheNotification.Listener != null)
        {
            PusheNotification.Listener.OnNotificationClick(data);
        }
    }

    public void OnNotificationDismiss(string notificationData)
    {
        Pushe.Log(notificationData);
        var data = JsonUtility.FromJson<NotificationData>(notificationData);
        if (PusheNotification.Listener != null)
        {
            PusheNotification.Listener.OnNotificationDismiss(data);
        }
    }

    public void OnNotificationButtonClick(string notificationDataAndButtonData)
    {
        Pushe.Log("Button clicked\n" + notificationDataAndButtonData);
        var data = JsonUtility.FromJson<NotificationDataAndButtonData>(notificationDataAndButtonData);
        if (PusheNotification.Listener != null)
        {
            PusheNotification.Listener.OnButtonClick(data.clickedButton, data.notificationData);
        }
    }

    [Serializable]
    public class NotificationDataAndButtonData
    {
        public NotificationData notificationData;
        public NotificationButtonData clickedButton;

        public override string ToString()
        {
            return "\nButtonData: " + notificationData + "///" + clickedButton;
        }
    }

    public static void SetCallbackGameObject(string objectName)
    {
        try
        {
            Pushe.PusheExt().CallStatic("setCallbackGameObjectName", objectName);
        }
        catch (Exception e)
        {
            Pushe.Log("Failed to set game object name. Still PusheCallback. " + e);
        }
    }
    
    
    // util

    public static void SetDebugMode(bool enabled)
    {
        Pushe.PusheExt().CallStatic("debuggingMode", enabled);
    }
    
}