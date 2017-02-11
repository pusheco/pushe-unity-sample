//using UnityEngine;
//using System.Collections;
//
//public class Pushe : MonoBehaviour 
//{
//	private AndroidJavaObject activityContext = null;
//	public bool showGooglePlayDialog = true;
//	public string channel = "pusheUnityChannel";
//	
//	
//	void Start() 
//	{
//		try
//		{
//			AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//			
//			//getting context of unity activity
//			activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
//			//calling plugin class by package name
//			AndroidJavaClass pluginClass = new AndroidJavaClass("co.ronash.pushe.Pushe");
//			
//			if (pluginClass != null)
//			{
//				
//				activityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
//				                                                              {
//					//calling initialize static method
//					pluginClass.CallStatic("initialize", new object[2] { activityContext, showGooglePlayDialog });
//					// subscribe to a channel
//					pluginClass.CallStatic("subscribe", new object[2] { activityContext, channel});
//					// unsubscribe from a channel
//					pluginClass.CallStatic("unsubscribe", new object[2] { activityContext, "subscribed channel name"});
//				}));
//				
//				
//				
//			}
//		}
//		catch
//		{
//		}
//	}
//	
//}
using UnityEngine;
using System.Collections;

public class Pushe : MonoBehaviour 
{
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

			if (pluginClass != null)
			{
				activityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
					{
						//calling initialize static method
						pluginClass.CallStatic("initialize", new object[2] { activityContext, showGooglePlayDialog });
					} ) );

			}
			//sample usage of Subscribe method: use it after a chack on 'PusheIsInitialized()'
			//if(PusheIsInitialized())
			//	Subscribe("unity_test_topic");
		}
		catch
		{
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
	 * Call this method to disable publishing notification to user.
	 * To enable showing notifications again, you need to call SetNotificationOn()
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

}
