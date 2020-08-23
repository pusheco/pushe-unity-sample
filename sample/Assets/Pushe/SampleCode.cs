using System.Collections.Generic;
using UnityEngine;
// ReSharper disable All

/// In order to test the differnt functionality of Pushe, simply drag this file into your scene.
/// NOTE: For only being able to receive push no script and code is needed. Just setup and add the token in manifest.
public class SampleCode : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        Pushe.Log("Starting Pushe sample script");
        InitializeSomeMethods();
    }

    private void InitializeSomeMethods()
    {
        // Listen to Register
        Pushe.OnPusheRegistered(OnPusheRegisteredSuccessfully);
        // Listen to Initialize
        Pushe.OnPusheInitialized(OnPusheInitialized);
        // Set notification listener
        PusheNotification.SetNotificationListener(new PusheNotifListener());

        // Check if pushe is already registered
        Pushe.Log(Pushe.IsRegistered() ? "Pushe is registered" : "Pushe is NOT registered!");
    }


    /**
     * Called when Pushe is registered.
     */
    private void OnPusheRegisteredSuccessfully()
    {
        Pushe.Log(" --- Pushe has been REGISTERED to server successfully --- ");
        var adId = Pushe.GetGoogleAdvertisingId();
        Pushe.Log("Ad id: " + adId);
        var deviceId = Pushe.GetDeviceId();
        Pushe.Log("Device id : " + deviceId);
        
        // Pushe Notification

        Pushe.Log("Notification enabled? " + PusheNotification.IsNotificationEnabled());
        Pushe.Log("Custom sound enabled? " + PusheNotification.IsCustomSoundEnabled());
        PusheNotification.CreateNotificationChannel("CustomChannel", "CustomChannel");
        
        // Analytics
        PusheAnalytics.SendEvent("Some_Event");
        PusheAnalytics.SendEcommerceData("EcommerceData", 12.0);

        Pushe.Log("Subscribing to test1");
        Pushe.SubscribeTo("test1");

        Pushe.Log("Set 123123 as custom id");
        Pushe.SetCustomId("123123");
        Pushe.Log("CustomId is: " + Pushe.GetCustomId());

        var tags = new Dictionary<string, string> {{"name","Mohammad"}, {"age", "25"}, {"birthday","1435187386"}};
        Pushe.AddTags(tags);

        Pushe.RemoveTag("name", "age");

        Pushe.Log("Tags: " + Pushe.GetSubscribedTags());
        Pushe.Log("Topics: " + string.Join(",", Pushe.GetSubscribedTopics()));
        
        PusheInAppMessaging.DisableInAppMessaging();
        Pushe.Log($"Is in app messaging enabled? {PusheInAppMessaging.IsInAppMessagingEnabled()}");
        PusheInAppMessaging.EnableInAppMessaging();
        Pushe.Log($"Is in app messaging enabled? {PusheInAppMessaging.IsInAppMessagingEnabled()}");
        PusheInAppMessaging.TriggerEvent("qqq");
        PusheInAppMessaging.SetInAppMessagingListener(new InAppMessagingListener());

        string testMessage = "{\"message_id\": \"notATestMessage\", \"type\":\"center\",\"title\":{\"text\":\"\u0633\u0644\u0627\u0645 \u062a\u06cc\u062a\u0631 \u0647\u0633\u062a\u0645\",\"dir\":\"left\", \"size\":18,\"color\":\"#000000\",\"dir\":\"left\"},\"content\":{\"text\":\"\u0627\u06cc\u0646\u062c\u0627 \u0645\u062a\u0646 \u0642\u0631\u0627\u0631 \u0645\u06cc\u06af\u06cc\u0631\u0647.\",\"size\":15,\"dir\":\"left\"},\"condition\":{\"event\":\"qqq\",\"count\":1,\"time_gap\":0},\"buttons\":[{\"action\":{\"action_type\":\"D\"},\"text\":\"\u0627\u062f\u0627\u0645\u0647\",\"color\":\"#fff000\",\"bg\":\"#000000\",\"dir\":\"center\"}],\"action\":{\"action_type\":\"D\"},\"bg\":\"#ffffff\",\"im_count\":0}";
        PusheInAppMessaging.TestInAppMessage(testMessage);
    }

    /**
     * Called when Pushe is initialized.
     * What is Initialization and what's the difference between `initialize` and `register`?
     * If Pushe modules codes were ready, it means Pushe has been initialized,
         while Pushe is registered when the device is submitted to Pushe server and is ready to receive notification.
     */
    private static void OnPusheInitialized()
    {
        Pushe.Log("Pushe Modules have initialized successfully.");
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
        Pushe.Log("Notification received: " + notificationData);
    }

    public void OnCustomContentReceived(string customJson)
    {
        Pushe.Log("Notification custom content received: " + customJson);
    }

    public void OnNotificationClick(NotificationData notificationData)
    {
        Pushe.Log("Notification clicked: " + notificationData);
    }

    public void OnNotificationDismiss(NotificationData notificationData)
    {
        Pushe.Log("Notification dismissed: " + notificationData);
    }

    public void OnButtonClick(NotificationButtonData notificationButtonData, NotificationData notificationData)
    {
        Pushe.Log("Notification button clicked\n Data: " + notificationData +
                  "\n ButtonData: " + notificationButtonData);
    }
}

public class InAppMessagingListener : IPusheInAppMessagingListener
{
    public void OnInAppMessageReceived(InAppMessage inAppMessage) {
        Pushe.Log($"In app message received: {inAppMessage.title}");
    }

    public void OnInAppMessageTriggered(InAppMessage inAppMessage) {
        Pushe.Log($"In app message triggered: {inAppMessage.title}");
    }

    public void OnInAppMessageClicked(InAppMessage inAppMessage) {
        Pushe.Log($"In app message Clicked: {inAppMessage.title}");
    }

    public void OnInAppMessageDismissed(InAppMessage inAppMessage) {
        Pushe.Log($"In app message dismissed: {inAppMessage.title}");
    }

    public void OnInAppMessageButtonClicked(InAppMessage inAppMessage, int index) {
        Pushe.Log($"In app message button: {inAppMessage.title}, index: {index}");
    }
}