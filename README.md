# Unity Plugin for Pushe plus

## Content

* Root folder is the unity project that will be exported as `unitypackage` file.
* `extended` folder contains source code of `unity-extended` native android module which is used using bintray gradle as an android library.
* `CHANGELOG` file

## Usage

### Add the plugin

**Double click** on it when project is open to be imported, or **Right click on Assets** and import it as a `Custom package`.

### Use it

* Add the token in your own `AndroidManifest.xml` (If you don't have it, copy one)

> Since the library will register itself when app is started, no need to do anything extra.

Make a `C# script` and call `Pushe`, `PusheNotification` and `PusheAnalytics` methods as you desire.

> For more information see the `Sample.cs` script (or even add it to a GameObject to see the logs).

### Extra work

**Notification callbacks** are made using `UnitySendMessage` bridge and will need their own object (can be overriden in future versions).

> Make a GameObject called `PusheCallback` (Exactly this name!) and drag it to the scene, and then add `PusheCallback` script to it.

> The callbacks will trigger c# code only when engine is up and running in the foreground. Otherwise it will pause the application and callbacks won't be called.


For further informations, checkout the official documentations.

---

# Develop

Checkout [PLUGIN.md](PLUGIN.md) for more info on how to develop your own and maintain this.