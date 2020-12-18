using Gtk;

namespace pomodoro_dotnet
{
    public class TrayPopupMenu : Menu {
        public MenuItem closeButton    = new MenuItem("Close");
        public MenuItem settingsButton = new MenuItem("Settings");
        public TrayPopupMenu() : base() {
            Add(closeButton);
            Add(settingsButton);
        }
    }
}
