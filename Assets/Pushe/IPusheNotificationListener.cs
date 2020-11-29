namespace Pushe
{
    /// <summary>
    /// In order to use Pushe notification callback, you must implement an instance of this interface.
    /// Once you've done it, pass it to `PusheNotification.SetNotificationListener`.
    ///
    /// * NOTE: These callback only work when app (Unity engine) is up and running.
    /// </summary>
    public interface IPusheNotificationListener
    {
        /// <summary>
        /// Gets invoked when a notification has been received.
        /// </summary>
        /// <param name="notificationData">Will have all data received from notification</param>
        void OnNotification(NotificationData notificationData);

        /// <summary>
        /// If a notification contains custom data, this function will be called holding a data as json.
        /// </summary>
        /// <param name="json">is the json from notification</param>
        void OnCustomContentReceived(string json);

        /// <summary>
        /// If a notification gets clicked while app is open this function will invoked having notification data.
        /// </summary>
        /// <param name="notificationData">Will have the data of that notification</param>
        void OnNotificationClick(NotificationData notificationData);

        /// <summary>
        /// If a notification gets swiped out while app is open, this function will be invoked having notification data.
        /// </summary>
        /// <param name="notificationData">Will have the data of that notification</param>
        void OnNotificationDismiss(NotificationData notificationData);

        /// <summary>
        /// If a button from a notification was clicked, this function will ba called having button data and all notification data
        /// </summary>
        /// <param name="notificationButtonData">Will have data of the clicked button</param>
        /// <param name="notificationData">Will contain the notification data</param>
        void OnButtonClick(NotificationButtonData notificationButtonData, NotificationData notificationData);
    }
}