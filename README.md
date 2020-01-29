# Unity SDK for Pushe plus

## Content

* Root folder is the unity project that will be exported as `unitypackage` file.
* `extended` folder contains source code of `unity-extended` native android module which is used using bintray gradle as an android library.
* `CHANGELOG` file

## Using the plugin

Two ways to add `Pushe` in the game:
- Add it right inside the project
- Export the project and add `Pushe`

## Adding to the project

#### Add the plugin

* **Double click** on it when project is open to be imported, or **Right click on Assets** and import it as a `Custom package`.

- Enable using `Gradle` build system.


#### Import the plugin

* Copy `AndroidManifest` and `mainTemplate.gradle` file from `Pushe/Setup` to `Assets/Plugins/Android` (Create if you don't have).


> For more information see the `Sample.cs` script (or even add it to a GameObject to see the logs).

## Adding to Exported project

#### Add the plugin

Download the latest `unitypackage` from documents or right here, **Right click on Assets** and import the downloaded `unitypackage` as a `Custom package`.

#### Export

* Use export option in `Build` window of UnityEditor.
* Open the exported folder with `Android Studio`.

#### Importing the plugin

In the `build.gradle`:

* Add the repository

```groovy
allprojects {
   repositories {
      // ...
	  // Pushe Extended plugin
	  maven { url 'https://dl.bintray.com/pushe/plugin' }
   }
}
```

* Enable multiDex (If `minSDK < 21`)

```groovy
android {
	// ...

	defaultConfig {
        // ...
		// Enable MultiDex -- Pushe natively adds multidex support library.
		multiDexEnabled true
    }
}
```

* Add the library

```groovy
// Adding Pushe Plus
implementation ("co.pushe.plus:unity-extended:0.4.6")
```

#### Add credentials

In `AndroidManifest` file add this inside the token tag:

```xml
<application
 ...>


    <meta-date android:name="pushe_token" android:value="THE_TOKEN" />
 </application>
```

---

For more information about the documentation, visit [documentation](https://docs.pushe.co/)