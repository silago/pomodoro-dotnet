using System;
using Eto.Forms;
using Eto.Drawing;
using System.IO;

namespace pomodoro_dotnet
{
    public class TrayIconEto : TrayIndicator, ITrayIcon {
        public event System.Action OnLeftClick;
        Menu menu;
        Bitmap _restIcon;
        Bitmap _stopIcon;
        Bitmap _workIcon;

        public void OnStateChanged(State state) {
            string icon = string.Empty;
            switch (state) {
                case State.Resting:
                    Image = _restIcon;
                    break;
                case State.Stopped:
                    Image = _stopIcon;
                    break;
                case State.Working:
                    Image = _workIcon;
                    break;                
            }
        }
#if Windows

        private static Bitmap ToEtoImage(String str)
        {
            Bitmap btm;

            using (FileStream stream = new FileStream(str, FileMode.Open, FileAccess.Read))
            {
                btm = new Bitmap(stream);
            
            }
            return btm;
        }
#endif
        public TrayIconEto(string workIcon, string stopIcon, string restIcon, ContextMenu menu = null)  : base() { 
#if Windows
            _workIcon = ToEtoImage(workIcon);
            _stopIcon = ToEtoImage(stopIcon);
            _restIcon = ToEtoImage(restIcon);

#else
_workIcon = Bitmap.FromResource(workIcon);
                _stopIcon = new Bitmap(stopIcon);
                _restIcon = new Bitmap(restIcon);
#endif
            this.Image = _stopIcon;
            this.Activated += onActivatedMenu;
            this.Menu = menu;
             this.Show();
        }
        private void onActivatedMenu(object sender, EventArgs e)
        {          
           OnLeftClick();
        }
    }
}
