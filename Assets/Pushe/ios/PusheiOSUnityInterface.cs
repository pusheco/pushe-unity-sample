using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

public class PusheiOSUnityInterface {
#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void xcodeLog(string message);

    [DllImport("__Internal")]
    private static extern void initialize();

    [DllImport("__Internal")]
    private static extern bool isRegistered();

    [DllImport("__Internal")]
    private static extern string getDeviceId();

    [DllImport("__Internal")]
    private static extern string getAdvertisingId();

    [DllImport("__Internal")]
    private static extern void subscribeTo(string topic);

    [DllImport("__Internal")]
    private static extern void unsubscribeFrom(string topic);

    [DllImport("__Internal")]
    private static extern string getSubscribedTopics();

    [DllImport("__Internal")]
    private static extern void addTags(string tags);

    [DllImport("__Internal")]
    private static extern void removeTags(string keys);

    [DllImport("__Internal")]
    private static extern string getSubscribedTags();

    [DllImport("__Internal")]
    private static extern void setCustomId(string id);

    [DllImport("__Internal")]
    private static extern string getCustomId();

    [DllImport("__Internal")]
    private static extern bool setUserEmail(string email);

    [DllImport("__Internal")]
    private static extern string getUserEmail();

    [DllImport("__Internal")]
    private static extern bool setUserPhoneNumber(string phoneNumber);

    [DllImport("__Internal")]
    private static extern string getUserPhoneNumber();

    [DllImport("__Internal")]
    private static extern void sendEvent(string eventName);
#endif

    public static void XcodeLog(string message) {
#if UNITY_IOS && !UNITY_EDITOR
        xcodeLog(message);
#endif
    }

    public static void Initialize() {
#if UNITY_IOS && !UNITY_EDITOR
        initialize();
#endif
    }

    public static bool IsRegistered() {
#if UNITY_IOS && !UNITY_EDITOR
        return isRegistered();
#endif
        return false;
    }

    public static string GetDeviceId() {
#if UNITY_IOS && !UNITY_EDITOR
        return getDeviceId();
#endif
        return null;
    }

    public static string GetAdvertisingId() {
#if UNITY_IOS && !UNITY_EDITOR
        return getAdvertisingId();
#endif
        return null;
    }

    public static void Subscribe(string topic) {
#if UNITY_IOS && !UNITY_EDITOR
        subscribeTo(topic);
#endif
    }

    public static void Unsubscribe(string topic) {
#if UNITY_IOS && !UNITY_EDITOR
        unsubscribeFrom(topic);
#endif
    }

    public static string[] GetSubscribedTopics() {
#if UNITY_IOS && !UNITY_EDITOR
         string result = getSubscribedTopics();
         result = result.TrimStart('[');
         result = result.TrimEnd(']');
         string[] des = result.Split(',');
         for (int i = 0; i < des.Length; i++) {
             des[i] = des[i].TrimStart('"');
             des[i] = des[i].TrimEnd('"');
         }
         return des;
#endif
        return null;
    }

    public static void AddTags(Dictionary<string, string> tags) {
        string stringForm = "";
        for (int i = 0; i < tags.Count; i++) {
            if (i != 0) {
                stringForm += ",";
            }
            string key = tags.ElementAt(i).Key;
            string value = tags.ElementAt(i).Value;
            stringForm += key + ":" + value;                                
        } 
#if UNITY_IOS && !UNITY_EDITOR
        addTags(stringForm);
#endif
    }

    public static void RemoveTags(string[] keys) {
        string stringForm = "";
        for (int i = 0; i < keys.Length; i++) {
            if (i != 0) {
                stringForm += ",";
            }
            stringForm += keys[i];
        }
#if UNITY_IOS && !UNITY_EDITOR
        removeTags(stringForm);
#endif
    }

    // {"key1":"value1","key3":"value3","key2":"value2"}
    public static Dictionary<string, string> GetSubscribedTags() {
#if UNITY_IOS && !UNITY_EDITOR
        string stringForm = getSubscribedTags();
        stringForm = stringForm.TrimStart('{');
        stringForm = stringForm.TrimEnd('}');
        string[] dicItems = stringForm.Split(',');
        Dictionary<string, string> result = new Dictionary<string, string>();
        for (int i = 0; i < dicItems.Length; i++) {
            string[] temp = dicItems[i].Split(':') ;
            result.Add(temp[0], temp[1]);
        }
        return result;
#endif
        return new Dictionary<string, string>();
    }

    public static void SendEvent(string eventName) {
#if UNITY_IOS && !UNITY_EDITOR
        sendEvent(eventName);
#endif
    }

    public static void SetCustomId(string id) {
#if UNITY_IOS && !UNITY_EDITOR
        setCustomId(id);
#endif
    }

    public static string GetCustomId() {
#if UNITY_IOS && !UNITY_EDITOR
        return getCustomId();
#endif
        return null;
    }

    public static bool SetUserEmail(string email) {
#if UNITY_IOS && !UNITY_EDITOR
        return setUserEmail(email);
#endif
        return false;
    }

    public static string GetUserEmail() {
#if UNITY_IOS && !UNITY_EDITOR
        return getUserEmail();
#endif
        return null;
    }

    public static bool SetUserPhoneNumber(string phoneNumber) {
#if UNITY_IOS && !UNITY_EDITOR
        return setUserPhoneNumber(phoneNumber);
#endif
        return false;
    }

    public static string GetUserPhoneNumber() {
#if UNITY_IOS && !UNITY_EDITOR
        return getUserPhoneNumber();
#endif
        return null;
    }
}