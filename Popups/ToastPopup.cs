#if Windows
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace pomodoro_dotnet
{
    public class ToastPopup : BasePopupWindow
    {
        ToastNotifier notifier;
        public ToastPopup(string id)
        {
            notifier = ToastNotificationManager.CreateToastNotifier(id);
        }
        protected override void Show(string msg)
        {
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
            toastXml.GetElementsByTagName("text")[0].AppendChild(toastXml.CreateTextNode(msg));
            ToastNotification toast = new ToastNotification(toastXml);
            notifier.Show(toast);
        }

    }
}
#endif