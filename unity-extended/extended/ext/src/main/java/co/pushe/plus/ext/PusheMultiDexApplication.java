package co.pushe.plus.ext;

import android.content.Context;


/**
 * If app needs MultiDex support library, this class can handle the MultiDex support if added to Manifest as the Application class.
 * It also supports the callbacks and features in PusheUnityApplication since it extends it instead of android.app.Application
 */
public class PusheMultiDexApplication extends PusheUnityApplication {

    @Override
    protected void attachBaseContext(Context base) {
        super.attachBaseContext(base);
        android.support.multidex.MultiDex.install(this);
    }

    @Override
    public void onCreate() {
        super.onCreate();
    }
}
