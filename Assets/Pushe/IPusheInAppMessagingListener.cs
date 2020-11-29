namespace Pushe
{
    public interface IPusheInAppMessagingListener
    {
        void OnInAppMessageReceived(InAppMessage inAppMessage);

        void OnInAppMessageTriggered(InAppMessage inAppMessage);

        void OnInAppMessageClicked(InAppMessage inAppMessage);

        void OnInAppMessageDismissed(InAppMessage inAppMessage);

        void OnInAppMessageButtonClicked(InAppMessage inAppMessage, int index);
    }
}