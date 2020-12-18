using System;
using Gtk;
using Gdk;

namespace pomodoro_dotnet
{
    public class TrayIcon : StatusIcon {
        public event System.Action OnLeftClick;
        Menu menu;
        string _restIcon;
        string _stopIcon;
        string _workIcon;


        public void OnStateChanged(State state) {

            string icon = string.Empty;
            switch (state) {
                case State.Resting:
                    icon = _restIcon;
                    break;
                case State.Stopped:
                    icon = _stopIcon;
                    break;
                case State.Working:
                    icon = _workIcon;
                    break;                
            }

            this.File = icon;
        }

        public TrayIcon(string workIcon, string stopIcon, string restIcon, Menu menu = null) : base(stopIcon)  {

                _workIcon = workIcon;
                _stopIcon = stopIcon;
                _restIcon = restIcon;
                this.File = _stopIcon;
                this.ButtonPressEvent+=OnButtonPressEvent;
                this.PopupMenu+= OnPopupMenu;
                this.menu = menu;
        }

        private void OnPopupMenu(object o, PopupMenuArgs args) {
            if (menu!=null) {
                menu.ShowAll();
                menu.Popup();
            }
        }

        private void OnButtonPressEvent(object o, ButtonPressEventArgs args)
        {
            var btn = args.Event.Button;
            if (btn!=1) {
                return;
            }
            OnLeftClick.Invoke();
        }
    }
}
