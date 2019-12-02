using System.Collections.Generic;
using UnityEngine;
// ReSharper disable All

public class SampleCode : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        Pushe.Log("Starting Pushe sample script");
        PusheCallback.SetDebugMode(true);
        InitializeSomeMethods();
    }

    private void InitializeSomeMethods()
    {
        Pushe.Log("Initializing Pushe initialization and registration callbacks");
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
        
        // Pushe Notification
        
        PusheNotification.SendNotificationToUser(
            UserNotification.WithGoogleAdvertisementId(adId)
                .SetTitle("Hello user")
                .SetContent("How are you?")
        );

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

        PusheCallback.SetCallbackGameObject(gameObject.name);
        
        
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