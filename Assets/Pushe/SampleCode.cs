using UnityEngine;

public class SampleCode : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        Debug.Log("Starting script");
        InitializeSomeMethods();
    }

    private void InitializeSomeMethods()
    {
        Debug.Log("Started registering Pushe");
        // Listen to Register
        Pushe.OnPusheRegistered(OnPusheRegisteredSuccessfully);
        // Listen to Initialize
        Pushe.OnPusheInitialized(OnPusheInitialized);
        // Set notification listener
        PusheNotification.SetNotificationListener(new PusheNotifListener());

        // Check if pushe is already registered
        Debug.Log(Pushe.IsRegistered() ? "Pushe is registered" : "Pushe is NOT registered!");
    }


    /**
     * Called when Pushe is registered.
     */
    private void OnPusheRegisteredSuccessfully()
    {
        Debug.Log("Pushe has been registered successfully.");
    }

    /**
     * Called when Pushe is initialized.
     */
    private static void OnPusheInitialized()
    {
        Debug.Log("Pushe has initialized successfully.");
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
        Debug.Log("Notification received: " + notificationData);
    }

    public void OnCustomContentReceived(string customJson)
    {
        Debug.Log("Notification custom content received: " + customJson);
    }

    public void OnNotificationClick(NotificationData notificationData)
    {
        Debug.Log("Notification clicked: " + notificationData);
    }

    public void OnNotificationDismiss(NotificationData notificationData)
    {
        Debug.Log("Notification dismissed: " + notificationData);
    }

    public void OnButtonClick(NotificationButtonData notificationButtonData, NotificationData notificationData)
    {
        Debug.Log("Notification button clicked\n Data: " + notificationData +
                  "\n ButtonData: " + notificationButtonData);
    }
}