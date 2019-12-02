package co.pushe.plus.ext;

import android.text.TextUtils;
import android.util.Log;
import android.content.Context;
import co.pushe.plus.Pushe;
import co.pushe.plus.analytics.PusheAnalytics;
import co.pushe.plus.analytics.event.Ecommerce;
import com.unity3d.player.UnityPlayer;
import java.util.Arrays;
import java.util.List;
import java.lang.Exception;
import org.json.JSONObject;



/**
 * The extended module is built on top of `Plus:Base` module and adds extra functionalities to the library
 *  The features include Listener to Unity bridge sender, API simpler, MultidexApplication class, etc.
 */
public class PusheExt {

    public static boolean debug = false;

    // Core

    /**
     * Get the tags that user has added as a string JSON
     */
    public static String getSubscribedTagsJson() {
        return new JSONObject(Pushe.getSubscribedTags()).toString();
    }

    public static void removeTags(String csvTags) {
        try {
            String[] arrayOfTags = csvTags.split(",");
            List<String> tags = Arrays.asList(arrayOfTags);
            Pushe.removeTags(tags);
        } catch (Exception e) {
            Log.e("Pushe","Failed to remove tags.", e);
        }
    }

    /**
     * Get the topics subscribed by user in a comma separated value format.
     */
    public static String getSubscribedTopicsCsv() {
        return TextUtils.join(",", Pushe.getSubscribedTopics());
    }

    /**
     * If you don't want to override application to get callbacks, instead call this method and your Unity game objects will receive callbacks.
     */
    public static void initializeNotificationListener() {
        PusheUnityApplication.initializeListeners();
    }


    // Analytics

    public static void sendEcommerce(String name, double price, String category, long quantity) {
        PusheAnalytics analytics = Pushe.getPusheService(PusheAnalytics.class);
        if (analytics != null) {
            analytics.sendEcommerceData(new Ecommerce.Builder(name, price).setCategory(category).setQuantity(quantity).build());
        }
    }

    /**
     * Change the debugging mode to see additional data in the log
     *     and receive them from Unity message bridge.
     * GameObject: "PusheCallbacks" MethodName: "OnDebugMessage"
     */
    public static void debuggingMode(boolean enable) {
        debug = enable;
    }

    public static void setCallbackGameObjectName(String objectName) {
        try {
            if (objectName != null && !objectName.isEmpty()) {
                PusheUnityApplication.EngineChannel = objectName;
            } else {
                PusheUnityApplication.reportError("GameObject must not be null or empty", new Exception());
            }
        } catch (Exception e) {
            PusheUnityApplication.reportError("Failed to set the Game object name. Still using PusheCallback", e);
        }
    }
}
