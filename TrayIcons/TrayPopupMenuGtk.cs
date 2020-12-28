#if Linux
using Gtk;

namespace pomodoro_dotnet
{
    public class TrayPopupMenuGtk : Menu, ITrayPopupMenu {
        public MenuItem closeButton    = new MenuItem("Close");
        public MenuItem settingsButton = new MenuItem("Settings");
        public TrayPopupMenuGtk() : base() {
            Add(closeButton);
            Add(settingsButton);
            closeButton.Activated    += (s,e) => { OnClose(); };
            settingsButton.Activated    += (s,e) => { OnSettings(); };
        }

        public event System.Action OnClose;
        public event System.Action OnSettings;
    }
}
#endif