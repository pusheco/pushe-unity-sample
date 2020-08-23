using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;

public static class PusheInAppMessaging {
    private const string PushePath = "co.pushe.plus.Pushe";

    /// Trigger a local event. Local event is useless for sending data to server.
    /// Since no data is sent to server. Local events are only useful for triggering an in app messaging 
    /// which is stored before. If you want to send data to server 
    public static void TriggerEvent(string eventName) {
        Pushe.Log($"Triggering local event {eventName}");
        PiamService().Call("triggerEvent", eventName);
    }

    public static void EnableInAppMessaging() {
        Pushe.Log("Enabling InAppMessaging");
        PiamService().Call("enableInAppMessaging");
    }

    public static void DisableInAppMessaging() {
        Pushe.Log("Disabling InAppMessaging");
        PiamService().Call("disableInAppMessaging");
    }

    public static bool IsInAppMessagingEnabled() {
        return PiamService().Call<bool>("isInAppMessagingEnabled");
    }

    /// **VisibleForTesting**
    /// In order to test a message locally using code
    /// you can use this function to test the message that is used in API\
    /// Note that the function should ONLY BE USED FOR TESTING and does not send any info
    /// and will not trigger any callback
    /// <param name="message">Is a json string adopted from Restful-API body</param>
    /// <param name="instant">Tells if messsage should be triggered instantly or wait for it's display conditions.</param>
    public static void TestInAppMessage(string message, bool instant = false) {
        Pushe.Log($"Sending test message to InAppMessaging module\nMessage: {message}");
        PiamService().Call("testInAppMessage", new object[] {message, instant});
    }

    /// Listen to different events of InAppMessaging.
    /// Consists of "receive", "trigger", "click", "dismiss", "buttonClick"
    /// If a message occurred due to one of mentioned events, you will be notified.
    /// For isntance if you want to pause the game when an InAppMessage was triggered, you can listen to `trigger`
    /// and pause the game (No changes will happen on Game.Neighther pause, or anything else.)
    /// <param name="listener"> is the callback which is an interface and needs to be implemented</param>
    public static void SetInAppMessagingListener(IPusheInAppMessagingListener listener) {
        var callback = new InAppMessagingCallback(listener);
        PiamService().Call("setInAppMessagingListener", callback);
    }

    private static AndroidJavaObject PiamService()
    {
        return new AndroidJavaClass(PushePath).CallStatic<AndroidJavaObject>("getPusheService", "inappmessaging");
    }
}

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class InAppMessagingCallback : AndroidJavaProxy
{
    private IPusheInAppMessagingListener _listener;

    public InAppMessagingCallback(IPusheInAppMessagingListener listener) : base("co.pushe.plus.inappmessaging.PusheInAppMessagingListener")
    {
        _listener = listener;
    }

    public void onInAppMessageReceived (AndroidJavaObject inapp) {
        _listener.OnInAppMessageReceived(InAppMessage.FromAndroid(inapp));
    }
    public void onInAppMessageTriggered(AndroidJavaObject inapp) {
        _listener.OnInAppMessageTriggered(InAppMessage.FromAndroid(inapp));
    }
    public void onInAppMessageClicked(AndroidJavaObject inapp) {
        _listener.OnInAppMessageClicked(InAppMessage.FromAndroid(inapp));
    }
    public void onInAppMessageDismissed(AndroidJavaObject inapp) {
        _listener.OnInAppMessageDismissed(InAppMessage.FromAndroid(inapp));
    }
    public void onInAppMessageButtonClicked(AndroidJavaObject inapp, int index) {
        _listener.OnInAppMessageButtonClicked(InAppMessage.FromAndroid(inapp), index);
    }

    
}

[Serializable]
public class InAppMessage {
    public string title;
    public string content;

    public InAppMessageButton[] buttons;

    public static InAppMessage FromAndroid(AndroidJavaObject androidObject) {
        InAppMessage inapp = new InAppMessage();
        try {
            string inappJson = PusheUtils.Extension().CallStatic<string>("inAppToJson", androidObject);
            inapp = JsonUtility.FromJson<InAppMessage>(inappJson);
        } catch(Exception e) {
            Pushe.Log($"Failed to parse inapp message.\n {e}");
        }
        return inapp;
    }
}

[Serializable]
public class InAppMessageButton {
    public string text;
}