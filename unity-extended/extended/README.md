# Unity Extended plugin for Pushe

> This plugin is made for Unity plugin for Android projects

> Must use `Gradle` build system with mainTemplate.gradle

## Add dependency

* Include the repository

```java
allprojects {
   repositories {
      google()
      jcenter()
	  // Pushe Extended plugin
	  maven { url 'https://dl.bintray.com/pushe/plugin' }
      flatDir {
        dirs 'libs'
      }
   }
}
```

* Add the library

```java
dependencies {
	implementation fileTree(dir: 'libs', include: ['*.jar'])

	// Adding Pushe Plus
	implementation ('co.pushe.plus:unity-extended:0.4.3')
	// MultiDex -- Add it in case of method count overhead if your MINSDK is lower than 21 
	implementation 'com.android.support:multidex:1.0.3'
**DEPS**}
```

> This is already in the `mainTemplate.gradle` file. If you already have one just add it to this then.

## Usage

**For more information about the API reference, checkout documentations.**

## MultiDex

> If you use plus's `mainTemplate.gradle` file, It will do the trick and just do number (3) of below options.

If you came up with multiDex error, you should do various things. But with Plus:

1. Add `multiDexEnabled true` to defaultConfig of `mainTemplate.gradle`
2. Add `multiDex support library dependency` in `dependencies` block.
3. Override your `AndroidManifest` with `co.pushe.plus.ext.PusheMultiDexApplication` as `android:name` in `<application>` tag attributes.

## Callbacks

* Make a GameObject called `PusheCallback` (Exactly), and drag it to the scene. Attach `PusheCallback` script to it.
* Use `PusheNotification.SetNotificationListener()` and pass an interface to get the callbacks.
> Update: It is possible to use custom GameObject name, call `PusheCallback.setGameObjectName()` and callbacks will be sent to that GameObject.
> The PusheCallback.cs script still needs to be attached.

> **NOTE**: Callbacks will only trigger when engine is up and running. The library will call the objects anyway, but Unity won't get it.
> Use `PusheCallback.SetDebugMode(true)` to see the callbacks in the LogCat in all times.

Refer to `CHANGELOG.md` to see version changes.