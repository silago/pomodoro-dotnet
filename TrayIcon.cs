using System;
using Eto.Forms;
using Eto.Drawing;

namespace pomodoro_dotnet
{
    public class TrayIcon : TrayIndicator {
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

        public TrayIcon(string workIcon, string stopIcon, string restIcon, ContextMenu menu = null)  : base() { 
            Console.WriteLine("XX");

                _workIcon = new Bitmap(workIcon);
                _stopIcon = new Bitmap(stopIcon);
                _restIcon = new Bitmap(restIcon);
                this.Image = _stopIcon;
                this.Activated += onActivatedMenu;
                
                
                //this.ButtonPressEvent+=OnButtonPressEvent;
                this.Menu = menu;
                menu.Opening += OnOpening;
                this.Show();
        }

        private void OnOpening(object sender, EventArgs e)
        {
            Console.WriteLine("On OPening");
        }

        protected override void OnActivated(EventArgs e) {
            Console.WriteLine("On Activated");
        }

        private void onActivatedMenu(object sender, EventArgs e)
        {
            
            Console.WriteLine("On Activated");
            //OnLeftClick();
        }
        /*

private void OnActivated(EventArgs e)
{
   //var btn = args.Event.Button;
   //if (btn==1) {
       OnLeftClick.Invoke();
   //} else if (menu!=null) {
   //    menu.ShowAll();
   //    menu.Popup(null, null, null, btn, args.Event.Time);                           
   //}
}
*/
    }
}
