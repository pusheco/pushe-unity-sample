using System.Collections.Generic;
using Pushe;
using UnityEngine;
// ReSharper disable All

/// In order to test the differnt functionality of Pushe, simply drag this file into your scene.
/// NOTE: For only being able to receive push no script and code is needed. Just setup and add the token in manifest.
public class SampleCode : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        PusheUnity.Log("Starting Pushe sample script");
        InitializeSomeMethods();
    }

    private void InitializeSomeMethods()
    {
        // Listen to Register
        PusheUnity.OnPusheRegistered(OnPusheRegisteredSuccessfully);
        // Listen to Initialize
        PusheUnity.OnPusheInitialized(OnPusheInitialized);
        // Set notification listener
        PusheNotification.SetNotificationListener(new PusheNotifListener());

        // Check if pushe is already registered
        PusheUnity.Log(PusheUnity.IsRegistered() ? "Pushe is registered" : "Pushe is NOT registered!");
    }


    /**
     * Called when Pushe is registered.
     */
    private void OnPusheRegisteredSuccessfully()
    {
        PusheUnity.Log(" --- Pushe has been REGISTERED to server successfully --- ");
        var adId = PusheUnity.GetGoogleAdvertisingId();
        PusheUnity.Log("Ad id: " + adId);
        var deviceId = PusheUnity.GetDeviceId();
        PusheUnity.Log("Device id : " + deviceId);
        
        // Pushe Notification

        PusheUnity.Log("Notification enabled? " + PusheNotification.IsNotificationEnabled());
        PusheUnity.Log("Custom sound enabled? " + PusheNotification.IsCustomSoundEnabled());
        PusheNotification.CreateNotificationChannel("CustomChannel", "CustomChannel");
        
        // Analytics
        PusheAnalytics.SendEvent("Some_Event");
        PusheAnalytics.SendEcommerceData("EcommerceData", 12.0);

        PusheUnity.Log("Subscribing to test1");
        PusheUnity.SubscribeTo("test1");

        PusheUnity.Log("Set 123123 as custom id");
        PusheUnity.SetCustomId("123123");
        PusheUnity.Log("CustomId is: " + PusheUnity.GetCustomId());

        var tags = new Dictionary<string, string> {{"name","Mohammad"}, {"age", "25"}, {"birthday","1435187386"}};
        PusheUnity.AddTags(tags);

        PusheUnity.RemoveTag("name", "age");

        PusheUnity.Log("Tags: " + PusheUnity.GetSubscribedTags());
        PusheUnity.Log("Topics: " + string.Join(",", PusheUnity.GetSubscribedTopics()));
        
        PusheInAppMessaging.DisableInAppMessaging();
        PusheUnity.Log("Is in app messaging enabled?" + PusheInAppMessaging.IsInAppMessagingEnabled());
        PusheInAppMessaging.EnableInAppMessaging();
        PusheUnity.Log("Is in app messaging enabled? " + PusheInAppMessaging.IsInAppMessagingEnabled());
        PusheInAppMessaging.TriggerEvent("qqq");
        PusheInAppMessaging.SetInAppMessagingListener(new InAppMessagingListener());
    }

    /**
     * Called when Pushe is initialized.
     * What is Initialization and what's the difference between `initialize` and `register`?
     * If Pushe modules codes were ready, it means Pushe has been initialized,
         while Pushe is registered when the device is submitted to Pushe server and is ready to receive notification.
     */
    private static void OnPusheInitialized()
    {
        PusheUnity.Log("Pushe Modules have initialized successfully.");
    }
}


/// <summary>
/// A sample to show how to use notification callback
/// PLEASE NOTICE that callbacks only work when app is up and running.
/// </summary>
public class PusheNotifListener : IPusheNotificationListener
{
    public void OnNotification(NotificationData notificationData)
    {
        PusheUnity.Log("Notification received: " + notificationData);
    }

    public void OnCustomContentReceived(string customJson)
    {
        PusheUnity.Log("Notification custom content received: " + customJson);
    }

    public void OnNotificationClick(NotificationData notificationData)
    {
        PusheUnity.Log("Notification clicked: " + notificationData);
    }

    public void OnNotificationDismiss(NotificationData notificationData)
    {
        PusheUnity.Log("Notification dismissed: " + notificationData);
    }

    public void OnButtonClick(NotificationButtonData notificationButtonData, NotificationData notificationData)
    {
        PusheUnity.Log("Notification button clicked\n Data: " + notificationData +
                        "\n ButtonData: " + notificationButtonData);
    }
}

public class InAppMessagingListener : IPusheInAppMessagingListener
{
    public void OnInAppMessageReceived(InAppMessage inAppMessage) {
        PusheUnity.Log("In app message received: " + inAppMessage.title);
    }

    public void OnInAppMessageTriggered(InAppMessage inAppMessage) {
        PusheUnity.Log("In app message triggered: " + inAppMessage.title);
    }

    public void OnInAppMessageClicked(InAppMessage inAppMessage) {
        PusheUnity.Log("In app message Clicked: " + inAppMessage.title);
    }

    public void OnInAppMessageDismissed(InAppMessage inAppMessage) {
        PusheUnity.Log("In app message dismissed: " + inAppMessage.title);
    }

    public void OnInAppMessageButtonClicked(InAppMessage inAppMessage, int index) {
        PusheUnity.Log("In app message button: " + inAppMessage.title + " index: " + index);
    }
}