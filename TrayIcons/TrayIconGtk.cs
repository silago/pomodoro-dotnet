#if Linux
using System;
using Gtk;
using Gdk;

namespace pomodoro_dotnet
{
    public class TrayIconGtk : StatusIcon, ITrayIcon {
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
            this.Pixbuf = new Pixbuf(icon);
        }

        public TrayIconGtk(string workIcon, string stopIcon, string restIcon, Menu menu = null) : base(new Pixbuf(stopIcon))  {

                _workIcon = workIcon;
                _stopIcon = stopIcon;
                _restIcon = restIcon;
                this.ButtonPressEvent+=OnButtonPressEvent;
                this.menu = menu;
                
        }

        private void OnButtonPressEvent(object o, ButtonPressEventArgs args)
        {
            var btn = args.Event.Button;
            if (btn==1) {
                OnLeftClick.Invoke();
            } else if (menu!=null) {
                menu.ShowAll();
                menu.Popup(null, null, null, btn, args.Event.Time);                           
            }
        }
    }
}
#endif