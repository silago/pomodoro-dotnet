using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace pomodoro_dotnet
{
    public class PopupWindow : MessageDialog {

        string _restMsg = "it's time to make a break";
        string _workMsg = "back to work";
        string _stopMsg = "pomodoro has been stopped";

        public void OnStateChanged(pomodoro_dotnet.State state) {
            string msg = string.Empty;
            string icon = string.Empty;
            switch (state) {
                case pomodoro_dotnet.State.Resting:
                    msg = _restMsg;
                    break;
                case pomodoro_dotnet.State.Stopped:
                    msg = _stopMsg;
                    break;
                case pomodoro_dotnet.State.Working:
                    msg = _workMsg;
                    break;                
            }

            this.Text = msg;
            this.Show();
        }

        // them autoconnects searching the field name in the glade file
        //[UI] private MessageDialog _popup = null;
        [UI] private Button _button = null;


        public static PopupWindow Init(string path = "Popup.glade") {
            var window = new PopupWindow(path);
            return window;
        }

        private PopupWindow(string path) : this(new Builder(path)) { }
        private PopupWindow(Builder builder) : base(builder.GetObject("_popup").Handle)
        {
            builder.Autoconnect(this);
            _button.Clicked += OnClick;
        }

        private void OnClick(object sender, EventArgs e)
        {
           this.Hide();
        }
    }
}
