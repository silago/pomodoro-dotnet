#if Linux
using Notify;

namespace pomodoro_dotnet
{
    public class LibnotifyPopup : BasePopupWindow
    {
        protected override void Show(string msg)
        {
            var notification = new Notification("pomodoro", msg);
            notification.Show();
        }
    }
}
#endif