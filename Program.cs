using System;
using System.IO;
using System.Reflection;
using System.Threading;
using Gtk;

namespace pomodoro_dotnet
{
    partial class Program
    {
        static string settingsPath = "Resources/Settings.cfg";
#if Windows
        private const String APP_ID = "pomodoro .Net";
#endif
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();
            CancellationToken token = new CancellationToken();
            var settings       = LoadSettings();
            var application    = new App(settings, token);
            var settingsWindow = SettingsWindow.Init(settings);
            var menu = new TrayPopupMenu();
            
            var trayIcon = new TrayIcon("Resources/hammers.png", "Resources/pause.png", "Resources/coffee.png", menu);
       
            IPopup notificator;
#if Linux
            notificator = new GladePopupWindow("Popup.glade");
#elif Windows
            notificator = new ToastPopup(APP_ID);
#endif
            
            menu.closeButton.Activated    += delegate {
                Application.Quit(); 
            };
            menu.settingsButton.Activated += delegate {
                settingsWindow.Show(); 
            };

            trayIcon.OnLeftClick += delegate { application.ToggleState(); };
            application.StateChanged += trayIcon.OnStateChanged;
            application.StateChanged += notificator.OnStateChanged;
            settingsWindow.UpdatedSettings += application.SetSettings;
            settingsWindow.UpdatedSettings += SaveSettings;
            using (menu)
            using (settingsWindow)
            using (trayIcon)
            {
                Application.Run();
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


        public static void ShowMenu()
        {
            var popup = new Menu();
            var exitItem = new MenuItem("Quit"); 
        }
    }
}
