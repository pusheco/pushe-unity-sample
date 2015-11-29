using UnityEngine;
using System.Collections;

public class Pushe : MonoBehaviour 
{
	private AndroidJavaObject activityContext = null;
	public bool showGooglePlayDialog = true;
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
					// subscribe to a channel
					pluginClass.CallStatic("subscribe", new object[2] { activityContext, channel});

					pluginClass.CallStatic("unsubscribe", new object[2] { activityContext, "dummyChannel"});
				}));



			}
		}
		catch
		{
		}
	}

}
