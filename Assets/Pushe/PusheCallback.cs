using System;
using UnityEngine;

// ReSharper disable CheckNamespace

/// <inheritdoc />
/// <summary>
/// Callback scripts are handled here.
/// In order to work this script must be attached to a game object (Which already is, to Pushe.prefab)
/// It will be called by PusheCallback when a message was arrived.
///
/// NOTE: Callback will not work if the app was not on foreground. The unity engine won't work on background.
/// This class doesn't need to be modified by the user.
/// </summary>
public class PusheCallback : MonoBehaviour
{
    public void PusheOnNotification(string notificationData)
    {
        Debug.Log(notificationData);
        var data = JsonUtility.FromJson<NotificationData>(notificationData);
        if (PusheNotification.Listener != null)
        {
            PusheNotification.Listener.OnNotification(data);
        }
    }

    public void PusheOnCustomContentNotification(string notificationData)
    {
        Debug.Log(notificationData);
        var data = JsonUtility.FromJson<NotificationData>(notificationData);
        if (PusheNotification.Listener == null) return;
        PusheNotification.Listener.OnCustomContentReceived(data.customContent);
    }

    public void PusheOnNotificationClick(string notificationData)
    {
        Debug.Log(notificationData);
        var data = JsonUtility.FromJson<NotificationData>(notificationData);
        if (PusheNotification.Listener != null)
        {
            PusheNotification.Listener.OnNotificationClick(data);
        }
    }

    public void PusheOnNotificationDismiss(string notificationData)
    {
        Debug.Log(notificationData);
        var data = JsonUtility.FromJson<NotificationData>(notificationData);
        if (PusheNotification.Listener != null)
        {
            PusheNotification.Listener.OnNotificationDismiss(data);
        }
    }

    public void PusheOnNotificationButtonClick(string notificationDataAndButtonData)
    {
        Debug.Log("Button clicked\n" + notificationDataAndButtonData);
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
}