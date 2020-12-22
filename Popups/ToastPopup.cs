#if Windows
using System;
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
            var fields = toastXml.GetElementsByTagName("text")[0].AppendChild(toastXml.CreateTextNode(msg));
            ToastNotification toast = new ToastNotification(toastXml);
            toast.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(30);
            toast.Failed += (o, args) => {
                var message = args.ErrorCode;
                Console.WriteLine(message);
                Console.WriteLine(args.ToString());
            };
            notifier.Show(toast);
        }
    }
}
#endif