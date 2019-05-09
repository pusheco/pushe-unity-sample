using UnityEngine;

namespace Pushe
{
	public class Pushe : MonoBehaviour {

	private AndroidJavaObject activityContext = null;
	public bool showGooglePlayDialog = true; //if it is true, user will see a dialog for installing GooglePlayService if it is not installed on her/his device
	public string channel = "pusheUnityChannel";

	void Start() 
	{
		try
		{
			AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

			//getting context of unity activity
			activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
			//calling plugin class by package name
			AndroidJavaClass pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");

			activityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
			{
				//calling initialize static method
				pluginClass.CallStatic("initialize", new object[2] { activityContext, showGooglePlayDialog });
				//sample usage of Subscribe and other methods: use subscribe and sentNotifToUser after a chack on 'PusheIsInitialized()'
				print("unity sample: pusheID is " + GetPusheId());
				if(PusheIsInitialized()) {
					//Subscribe("unity_test_topic");
					//print("pusheID is " + GetPusheId());
				}

			} ) );

		}
		catch
		{
			Debug.Log("Failed to initialize the project somehow!");
		}
	}

	void Update(){

	}

	/**
	 * Call for subscribing to a topic. It has to be called after Pushe.initialize() has completed its work
	 * So, call it with a reasonable delay (30 sec to 2 min) after Pushe.initialize()
	 **/
	public static void Subscribe(string topic){
		AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
		if (pluginClass != null) {
			pluginClass.CallStatic("subscribe", new object[2] {context, topic});
		}
	}

	/**
	 * Call for unsubscribing from a topic. It has to be called after Pushe.initialize() has completed its work
	 * So, call it with a reasonable delay (30 sec to 2 min) after Pushe.initialize()
	 **/
	public static void Unsubscribe(string topic){
		AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
		if (pluginClass != null) {
			pluginClass.CallStatic("unsubscribe", new object[2] {context, topic});
		}
	}

	/**
	 * Call this method to enable publishing notification to user, if you already called SetNotificationOff()
	 **/
	public static void NotificationOn(){
		AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
		if (pluginClass != null) {
			pluginClass.CallStatic("setNotificationOn", new object[1] {context});
		}
	}

	/**
	 * Call this method to disable publishing notification to user.
	 * To enable showing notifications again, you need to call SetNotificationOn()
	 **/
	public static void NotificationOff(){
		AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
		if (pluginClass != null) {
			pluginClass.CallStatic("setNotificationOff", new object[1] {context});
		}
	}

	/**
	 * Call this method to check if pushe is initialized.
	 * It is needed before call to un/subscribe, and sendNotif to user methods
	 **/
	public static bool PusheIsInitialized(){
		AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
		if (pluginClass != null) {
			return pluginClass.CallStatic<bool>("isPusheInitialized", new object[1] {context});
		}
		return false;
	}

	/**
	 * Call this method to get this device pusheId.
	 * It is needed for call to and sendNotif to user methods
	 **/
	public static string GetPusheId(){
		AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
		if (pluginClass != null) {
			return pluginClass.CallStatic<string>("getPusheId", new object[1] {context});
		}
		return "";
	}

	/**
	 * Call this method to send simple notification from client to another client.
	 **/
	public static void SendSimpleNotifToUser(string userPusheId, string title, string content){
		AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
		if (pluginClass != null) {
			pluginClass.CallStatic("sendSimpleNotifToUser", new object[4] {context, userPusheId, title, content});
		}
	}

	/**
	 * Call this method to send advanced notification from client to another client.
	 * You need to prepare advanced notification as a valid json string.
	 **/
	public static void SendAdvancedNotifToUser(string userPusheId, string notificationJson){
		AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
		if (pluginClass != null) {
			pluginClass.CallStatic("sendAdvancedNotifToUser", new object[3] {context, userPusheId, notificationJson});
		}
	}
		
	/**
	 * Call this method to send any content you like to another client.
	 * You need to prepare this content as a valid json string.
	 **/
	public static void SendCustomJsonToUser(string userPusheId, string customJson){
		AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
		if (pluginClass != null) {
			pluginClass.CallStatic("sendCustomJsonToUser", new object[3] {context, userPusheId, customJson});
		}
	}

	/**
	 * Create a custom notification channel. This methos works for android 8+
	 * On lower android version, call to this method has no effect
	 **/
	public static void CreateNotificationChannel(string channelId, string channelName,
		string description, int importance,
		bool enableLight, bool enableVibration,
		bool showBadge, int ledColor, long[] vibrationPattern){
		AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
		if (pluginClass != null) {
			pluginClass.CallStatic("createNotificationChannel", 
				new object[10] {context, channelId, channelName, description, importance, enableLight, 
					enableVibration, showBadge, ledColor, vibrationPattern});
		}
	}

	/**
	 * Remove a custom notification channel. This methos works for android 8+
	 * On lower android version, call to this method has no effect
	 **/
	public static void RemoveNotificationChannel(string channelId){
		AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject context = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
		if (pluginClass != null) {
			pluginClass.CallStatic("removeNotificationChannel", 
				new object[2] {context, channelId});
		}
	}
	}
}
