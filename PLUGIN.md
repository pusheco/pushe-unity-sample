# How to make a plugin for Unity engine?

## Step 1: Know the build systems (Gradle, JarResolver)

### Gradle

Ofcourse we know what gradle is. Since Unity 5, engine does support Gradle to add android dependencies. We can add dependency code like Pushe's. We'll discuss about it in it's own section.

### Unity jar resolver

[The repo](https://github.com/googlesamples/unity-jar-resolver)

Jar resolver is a unity extension which uses Gradle as well to add the dependencies. Check out it's Github **and** the Unity v1 plugin to know how to use it. **It's more popular than Gradle itself and other plugins are added like this**.

> **Why plugin is using Gradle and not Jar resolver?**
> 
> Jar resolver can't handle `exclude` dependencies (For our custom RxJava) and also `MultiDex`ing (Since pushe is damn heavy, it will need multidex to be applied for Api 21 or lower -- Or game will **crash**). Thus the Gradle itself which will handle it is used.

## Step 2: How to make the Plugin

### Gradle

There's a file in `Pushe` folder in plugin called `mainTemplate.gradle`. When building the Game using unity, the Engine will use this file to handle Gradle and add libraries to classPath.
The file needs to be at `Assets/Plugins/Android` (Create the folder if they don't exist).

> **Note**: When adding the Plugin this file will replace the old one (user might already have one, so adding Pushe will cause his own to be deleted for ever). So leave it in `Pushe` and tell user to add or merge with his own.

The rest is the Android native and building the Game will add Pushe to APK dex files.

### Jar resolver

Malv wrote an artical in virgool about this. [Link](http://vrgl.ir/RElqJ)

## Step 3: How Game devs use Pushe APIs?

### Make it simple

Calling the native functions via Unity scripts is possible using `AndroidJavaObject` and `AndroidJavaClass` classes.

> Unity will call scripts when they are added to GameObjects in game (Read more about it in Unity docs. It's too much to be brought here).

**Make users use their own language**

We should interact with Pushe plus ourselves and make functions in scripts and tell users to use those functions (Check out the scripts in `Pushe` folder).

For instance, user wants to check for registration using `Pushe.isRegistered`. Normally he must do this:

```cs
bool isRsgistered = new AndroidJavaClass("co.pushe.plus.Pushe").CallStatic<bool>("isRegistered");
```

But we made a function at the midlevel and tell user to use this:

```cs
bool isIt = Pushe.IsRegistered();
```

## Advanced

### Callbacks

There's no way to add it easy. Unity does not work on background, so nothing works when game is closed (even in pausing Unity does not call scripts and will pause the Game).

**UnitySendMessage(gameObjectName, functionName, param)**

In plugin we used this in our own native class to handle it.

* We made and application class
* Tell users to add this in the manifest
* Make a game object with the name we chose
* add our script to it
* The callback will be called when message is passed to it.

> Checkout `PusheCallback` scripts and also `PusheUnityApplication` class in `unity-extended` folder.

## Why we made a Native module?

To use `UnitySendMessage` to pass callbacks to Unity, we needed native classes. So they could be added using the Plus library itself (which is maddness, unity is not needed in all framework), or add it using an external library. So an external library called `co.pushe.plus:unity-extended` is created to add `Pushe plus` and `extension classes`.

## Step 4: Export

In Unity by right clicking on `Assets` and selecting export, you can select which files to export.

> Add `Pushe` folder and don't add scenes and extra stuff.

A file with `.unitypackage` format will be created which user will download and use.

## Step 4: Test

* Make a unity project (Unity is great on Windows and Mac (Graphic driver is natively supported) and better than nothing in linux)
* Select `Android platform` on build settings
* Import the plugin
* Submit it in console and follow beta docs and run your app.