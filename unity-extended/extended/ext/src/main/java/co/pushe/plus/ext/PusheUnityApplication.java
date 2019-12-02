package co.pushe.plus.ext;

import android.app.Application;
import android.util.Log;
import co.pushe.plus.Pushe;
import co.pushe.plus.notification.NotificationButtonData;
import co.pushe.plus.notification.NotificationData;
import co.pushe.plus.notification.PusheNotification;
import co.pushe.plus.notification.PusheNotificationListener;
import com.unity3d.player.UnityPlayer;
import java.util.Map;
import org.json.JSONException;
import org.json.JSONObject;
import static co.pushe.plus.ext.PusheExt.debug;

@SuppressWarnings("ALL")
/**
 * Handling callbacks and related codes for Unity class
 */
public class PusheUnityApplication extends Application {

    public static String EngineChannel = "PusheCallback",
            receiveMethod = "OnNotification",
            customContentMethod = "OnCustomContentNotification",
            clickMethod = "OnNotificationClick",
            dismissMethod = "OnNotificationDismiss",
            buttonClickMethod = "OnNotificationButtonClick";

    private static String DebugChannel = "PusheCallback";
    private static String debugMethod = "OnDebugMessage";

    @Override
    public void onCreate() {
        super.onCreate();
        initializeListeners();
    }

    public static void initializeListeners() {
        PusheNotification notification = Pushe.getPusheService(PusheNotification.class);
        if (notification != null) {
            notification.setNotificationListener(new PusheNotificationListener() {
                @Override
                public void onNotification(NotificationData notificationData) {
                    try {
                        String notificationDataString = parse(notificationData);
                        report(notificationDataString);
                        UnityPlayer.UnitySendMessage(EngineChannel, receiveMethod, notificationDataString);
                    } catch (Exception e) {
                        reportError("Error parsing received message", e);
                    }
                }

                @Override
                public void onCustomContentNotification(Map<String, Object> map) {
                    try {
                        String customContent = new JSONObject(map).toString();
                        report(customContent);
                        UnityPlayer.UnitySendMessage(EngineChannel, customContentMethod, customContent);
                    } catch (Exception e) {
                        reportError("Error parsing json", e);
                    }
                }

                @Override
                public void onNotificationClick(NotificationData notificationData) {
                    try {
                        String notificationDataString = parse(notificationData);
                        report(notificationDataString);
                        UnityPlayer.UnitySendMessage(EngineChannel, clickMethod, notificationDataString);
                    } catch (Exception e) {
                        reportError("Error parsing clicked message", e);
                    }
                }

                @Override
                public void onNotificationDismiss(NotificationData notificationData) {

                    try {
                        String notificationDataString = parse(notificationData);
                        report(notificationDataString);
                        UnityPlayer.UnitySendMessage(EngineChannel, dismissMethod, notificationDataString);
                    } catch (Exception e) {
                        reportError("Error parsing dismissed message", e);
                    }
                }

                @Override
                public void onNotificationButtonClick(NotificationButtonData notificationButtonData, NotificationData notificationData) {

                    try {
                        String notificationWithButtonData = parse(notificationData, notificationButtonData);
                        report(notificationWithButtonData);
                        UnityPlayer.UnitySendMessage(EngineChannel, buttonClickMethod, notificationWithButtonData);
                    } catch (Exception e) {
                        reportError("Error parsing clicked button or the message", e);
                    }
                }
            });
        }
    }

    private static String parse(NotificationData data) throws JSONException {
        JSONObject j = new JSONObject();
        j.put("messageId", data.getMessageId());
        if (data.getTitle() != null)
            j.put("title", data.getTitle());
        j.put("content", data.getContent());
        if (data.getBigTitle() != null)
            j.put("bigTitle", data.getBigTitle());
        if (data.getBigContent() != null)
            j.put("bigContent", data.getBigContent());
        if (data.getSummary() != null)
            j.put("summary", data.getSummary());
        if (data.getImageUrl() != null)
            j.put("imageUrl", data.getImageUrl());
        if (data.getIconUrl() != null)
            j.put("iconUrl", data.getIconUrl());
        if (data.getBigIconUrl() != null)
            j.put("bigIconUrl", data.getBigIconUrl());
        if (data.getCustomContent() != null)
            j.put("customContent", new JSONObject(data.getCustomContent()).toString());

        return j.toString();
    }

    private static String parse(NotificationButtonData data) throws JSONException {
        JSONObject j = new JSONObject();
        j.put("id", data.getId());
        if (data.getText() != null)
            j.put("text", data.getText());
        if (data.getIcon() != null)
            j.put("icon", data.getIcon());
        return j.toString();
    }

    private static String parse(NotificationData data, NotificationButtonData buttonData) throws JSONException {
        JSONObject j = new JSONObject();
        j.put("notificationData", parse(data));
        j.put("clickedButton", parse(buttonData));
        return j.toString();
    }

    /**
     * Prints the log in the logcat. Also it will send a debug message in a specific unity object
     * @param message is error message
     * @param e is the thrown exception
     */
    public static void reportError(String message, Exception e) {
        Log.e("Pushe", message, e);
        if (debug) {
            UnityPlayer.UnitySendMessage(DebugChannel, debugMethod, message + "\nStackTrace:\n" + e.getMessage());
        }
    }

    private static void report(String message) {
        if (debug) {
            Log.i("Pushe", message);
            UnityPlayer.UnitySendMessage(DebugChannel, debugMethod, message);
        }
    }
}
