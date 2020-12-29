using System;
using System.Collections.Generic;
using System.Text;

namespace pomodoro_dotnet
{
    class NotificationProxy 
    {
        IPopup popup;
        int type = 2; //not set
        public void OnStateChanged(pomodoro_dotnet.State state)
        {
            if (popup != null) popup.OnStateChanged(state);
                    
        }
        public void SetSettings(Settings settings)
        {
            if (settings.NotificationType==type)
            {
                return;
            }
         //   popup = null;
            switch (settings.NotificationType)
            {
                case (0):
                    popup = new EtoPopup();
                    break;
                case (1):
#if Windows
                    popup = new ToastPopup("pomodoro");
#else
                    popup = new LibnotifyPopup();

#endif
                    break;

            }


        }
    }
}
