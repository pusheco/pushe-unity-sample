### 1.2 `(Pushe version: 1.6.3)`

* Moved initialize code out of `uiThread` block in order to prevent lags on network problems.
* New API `RemoveNotificationCHannel`.
* Move `Initialize` to an API function.
* Removed `PushePrefab` object. From now on, drag the `Pushe.cs` script to your own hierarchy, or make your own.
* You can just call `Pushe.Initialize()` anywhere. Just import the script in your own and use it.

### 1.0 `(Pushe version: 1.6.3)`

* New build tool `Unity jar resolver` to get the library dependencies.
* Migrate to Pushe version **1.6.3**.
* Code cleanup.

### 0.x `(Pushe version: 1.4.0)`

* First plugin release to support Unity engine.