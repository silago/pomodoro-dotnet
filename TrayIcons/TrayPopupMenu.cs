using Eto.Forms;
using Eto.Drawing;
using System.Collections.Generic;

namespace pomodoro_dotnet
{
    public class TrayPopupMenu : ContextMenu {
        public MenuItem closeButton    = new ButtonMenuItem() { Text = "Quit" } ;
        public MenuItem settingsButton = new ButtonMenuItem() {Text = "Settings" };
        public TrayPopupMenu() : base() {
            Items.Add(settingsButton);
            Items.Add(closeButton);
        }
    }
}
