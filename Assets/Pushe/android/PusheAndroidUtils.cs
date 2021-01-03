using System.Collections.Generic;
using UnityEngine;

namespace Pushe.android
{
    static class PusheAndroidUtils
    {

        private const string PushePath = "co.pushe.plus.Pushe";
        // Some additional functions for Pushe SDK
        private const string ExtPath = "co.pushe.plus.ext.PusheExt";

        public static AndroidJavaClass Extension(string module = "core")
        {
            switch (module)
            {
                case "core":
                    return new AndroidJavaClass("co.pushe.plus.ext.PusheExt");
                case "notification":
                    return new AndroidJavaClass("co.pushe.plus.notification.ext.PusheExt");
                case "analytics":
                    return new AndroidJavaClass("co.pushe.plus.analytics.ext.PusheExt");
                case "inappmessaging":
                    return new AndroidJavaClass("co.pushe.plus.inappmessaging.ext.PusheExt");
            
            }
            return new AndroidJavaClass(ExtPath);
        }

        public static AndroidJavaClass Native()
        {
            return new AndroidJavaClass(PushePath);
        }

        public static AndroidJavaObject CreateJavaArrayList(params string[] elements)
        {
            var list = new AndroidJavaObject("java.util.ArrayList");
            foreach (var element in elements)
            {
                list.Call<bool>("add", element);
            }

            return list;
        }

        public static AndroidJavaObject CreateJavaMapFromDictionary(IDictionary<string, string> parameters)
        {
            var javaMap = new AndroidJavaObject("java.util.HashMap");
            var putMethod = AndroidJNIHelper.GetMethodID(
                javaMap.GetRawClass(), "put",
                "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");

            var args = new object[2];
            foreach (var kvp in parameters)
            {
                using (var k = new AndroidJavaObject(
                    "java.lang.String", kvp.Key))
                {
                    using (var v = new AndroidJavaObject(
                        "java.lang.String", kvp.Value))
                    {
                        args[0] = k;
                        args[1] = v;
                        AndroidJNI.CallObjectMethod(javaMap.GetRawObject(),
                            putMethod, AndroidJNIHelper.CreateJNIArgArray(args));
                    }
                }
            }

            return javaMap;
        }
        
        
        
        public static AndroidJavaObject PusheAnalyticsService()
        {
            return PusheAndroidUtils.Native().CallStatic<AndroidJavaObject>("getPusheService", "analytics");
        }


        public static AndroidJavaObject PusheNotificationService()
        {
            return PusheAndroidUtils.Native().CallStatic<AndroidJavaObject>("getPusheService", "notification");
        }

        public static int SdkLevel()
        {
            using (var version = new AndroidJavaClass("android.os.Build$VERSION"))
            {
                var sdkInt = version.GetStatic<int>("SDK_INT");
                return sdkInt;
            }
        }
    }
}