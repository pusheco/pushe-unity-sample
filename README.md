# [Pushe](https://pushe.co) SDK plugin for Unity.

> **Note**: Works only for projects targeted **Android** platform.

## Run the sample

* Clone this repo: `git clone https://github.com/pusheco/unity-sample.git`
* Open the project with your unity editor.


## Add Pushe to your own project

### Import Pushe to the project

* Download the latest package from the [documents](https://pushe.co/docs/unity/)

* Right click on `Assets` and select `Import package => Custom package` and browse downloaded file <br>

* From `Assets` menu, go to `Play service resolver => Android resolver`, and select `Resolve` or `Force resolve`<br>
> **Note**: Since gradle repositories refuses connections from Iran, you must use proxy in order to get the necessary files from gradle<br>

* For simple initialization, just drag `Pushe.cs` script to your game object in the hierarchy (e.g. Main camera).

### Change AndroidManifest.xml

1. Go to your [Pushe console](http://console.pushe.co/) and add your application with the same application package name selected in `Bundle Identifier`<br>
2. Get the manifest content and add it to your project manifest file.<br>

> **Note:** If your application doesn't have such a manifest, go to `C:\Program Files\Unity\Editor\Data\PlaybackEngines\androidplayer\apk` and copy `AndroidManifest.xml` to your `Assets/Plugins/Android`

### Run
Your project is ready to launch. Select `Build&Run` from `File` menu and test it on your device.


## Usage methods

> **Note**: Since Pushe uses `AndroidJavaObject` and `AndroidJavaClass`, wrap all usages in try/catch block in order to prevent errors.

#### Initialization

 To initialize Pushe, make sure manifest token is set and then call:
 
 ```c#
 Pushe.Initialize(); // Optional arguemnt (showGooglePlayDialog: Boolean)
 ```
 
**Note**: Before using any other Pushe methods, be sure to call `Initialize()` first.
 
#### Topic (Categorize users in specific groups)
 
To add user to a specific group, use topics. To subscribe a user to a topic:
 
```cs
var topicName = "sport";
Pushe.Subscribe(topicName);
```
And to unsubscribe:

```cs
var topicName = "sport";
Pushe.Unsubscribe(topicName);
```

#### Disable / Enable notification

```cs
Pushe.SetNotificationOn(); // To enable (Default settings)

Pushe.SetNotificationOff(); // To disable
``` 

#### Check initialization

To see if Pushe has already been initialized, use this method:

```cs
bool isPusheInitialized = Pushe.IsPusheInitialized();
```

#### Pushe id

A unique id for the device (this id belongs to the device and will not change). To get Pushe id call this method:

```cs
string pusheId = Pushe.GetPusheId();
```

#### Send notification via code

Having a Pushe id of a device, you can send notification to it using these codes.

To send a simple notification with only title and content:

```cs
var pusheId = "somePusheId";
var title = "Notification title";
var content = "Notification content";

Pushe.SendSimpleNotifToUser(pusheId, title, content);
```

> To send notification to the current device running this code use `Pushe.GetPusheId();` as the Pushe id (first argument).

To send an advance notification with other properties:

```cs
var pusheId = "somePusheId";
var notificationJson = "{ \"title\":\"Notification content\"", \"content\":\"Notification content\" }";
Pushe.SendAdvancedNotifToUser(pusheId, notificationJson);
```


## More info and FAQ
For detailed documentations visit https://pushe.co/docs/unity/
[Troubleshooting](https://pushe.co/docs/unity)

#### Contribution

If there are any improvements you can add to make this sample better, feel free to send a pull request.

## Support 
#### Email:
support [at] pushe.co
