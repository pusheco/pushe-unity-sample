# Change log

> The plugin version follows the `unity-extended` gradle version for simplicity.
> Refer to https://dl.bintray.com/pushe/plugin for more info about versioning.

## 0.4.6

* Updated native plugin to `0.4.6` which uses `2.0.4` of Android native SDK
* Fix bug with `CreateNotificationChannel`
* Big fixes and improvements

## 0.4.4

* Moved `mainTemplate.gradle` in `Pushe/setup` folder to avoid force replacement of this file when already exists.
* Added `AndroidManifest.xml` for more help in `Pushe/setup` folder.
* Updated native plugin to `0.4.4`
* Fix `PusheNotification.GetSubscribedTags()` returning an empty dict. The function will return `json` in `string` format.


## 0.4.3 - First release

* Added `unity-extended` module using `mainTemplate.gradle`
* All APIs are translated to C# scripts for better development usage

> **TODO**: Check what heppens when added to a project with existing `mainTemplate`.