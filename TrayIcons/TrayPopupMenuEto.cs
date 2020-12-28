using Eto.Forms;

namespace pomodoro_dotnet
{
    public class TrayPopupMenuEto : ContextMenu, ITrayPopupMenu {
        MenuItem closeButton    = new ButtonMenuItem() { Text = "Quit" } ;
        MenuItem settingsButton = new ButtonMenuItem() {Text = "Settings" };
        public TrayPopupMenuEto() : base() {
            Items.Add(settingsButton);
            Items.Add(closeButton);
            settingsButton.Click += (s,e) => { OnSettings(); };
            closeButton.Click    += (s,e) => { OnClose(); };
        }

        public event System.Action OnClose;
        public event System.Action OnSettings;
    }
}
