using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace pomodoro_dotnet
{
    public class SettingsWindow : Window {
        // them autoconnects searching the field name in the glade file
        [UI] private Entry _workTime = null;
        [UI] private Entry _restTime = null;
        [UI] private Button _saveBtn = null;
        [UI] private Button _cancelBtn = null;

        public event System.Action<Settings> UpdatedSettings;

        private static double FromMsToMin(double i) {
            return i/1000/60;
        }

        private static double FromMinToMs(double i) {
            return i*1000*60;
        }

        public static SettingsWindow Init(Settings settings, string path = "Settings.glade") {
            var window = new SettingsWindow(path);
            window._workTime.Text = FromMsToMin(settings.WorkTime).ToString();
            window._restTime.Text = FromMsToMin(settings.RestTime).ToString();
            return window;
        }

        private SettingsWindow(string path) : this(new Builder(path)) { }
        private SettingsWindow(Builder builder) : base(builder.GetObject("Settings").Handle)
        {
            builder.Autoconnect(this);
            _saveBtn.Clicked += OnSave;
            _cancelBtn.Clicked += OnCancel;
        }

        private void OnSave(object sender, EventArgs e)
        {
            var restTime = (int)FromMinToMs(int.Parse(_restTime.Text));
            var workTime = (int)FromMinToMs(int.Parse(_workTime.Text));
            UpdatedSettings.Invoke(new Settings {
                RestTime = restTime, WorkTime = workTime
                    });
           this.Hide();
        }

        private void OnCancel(object sender, EventArgs e)
        {
           this.Hide();
        }
    }
}
