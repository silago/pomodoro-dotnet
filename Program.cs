using System;
using System.IO;
using System.Reflection;
using System.Threading;
using Eto.Forms;


namespace pomodoro_dotnet
{
    partial class Program
    {
        static string settingsPath = "Resources"+Path.DirectorySeparatorChar+"Settings.cfg";
#if Windows
        private const String APP_ID = "pomodoro .Net";
#endif
        [STAThread]
        public static void Main(string[] args)
        {
            var app = new Application();
            //Application.Init();
            CancellationToken token = new CancellationToken();
            var settings       = LoadSettings();
            var application    = new App(settings, token);
            var settingsWindow = new SettingsWindow(settings);
            var menu = new TrayPopupMenu();
            var s = Path.DirectorySeparatorChar;
            ITrayIcon trayIcon;
       
            IPopup notificator;
#if Linux
            trayIcon = new TrayIconGtk("Resources/hammers.png", "Resources/pause.png", "Resources/coffee.png", menu);
            //notificator = new GladePopupWindow("Popup.glade");
            notificator = new LibnotifyPopup();
#elif Windows
            trayIcon = new TrayIconEto("Resources/hammers.png", "Resources/pause.png", "Resources/coffee.png", menu);
            notificator = new ToastPopup(APP_ID);
#endif
            
            menu.closeButton.Click    += delegate {
                app.Quit(); 
            };
            menu.settingsButton.Click += delegate {
                settingsWindow.Show(); 
            };

            trayIcon.OnLeftClick += delegate { application.ToggleState(); };
            application.StateChanged += trayIcon.OnStateChanged;
            application.StateChanged += notificator.OnStateChanged;
            settingsWindow.UpdatedSettings += application.SetSettings;
            settingsWindow.UpdatedSettings += SaveSettings;
            //using (menu)
            using (settingsWindow)
            {
                app.Run();
            }
        }

        public static string GetPath() {
            string folder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string path = folder + Path.DirectorySeparatorChar + settingsPath;
            return path;
        }

        public static Settings LoadSettings() {
            var path = GetPath();
            var content = File.ReadAllText(path);
            var data = content.Split(',');
            return new Settings() {WorkTime = int.Parse(data[0]), RestTime = int.Parse(data[1])};
        }

        public static void SaveSettings(Settings settings) {
            var path = GetPath();
            var data = settings.WorkTime + "," + settings.RestTime;
            File.WriteAllText(path, data);
        }
    }
}
