using System;
using Eto.Forms;
using Eto.Drawing;
using System.ComponentModel;

namespace pomodoro_dotnet
{
    public class SettingsWindow : Dialog {
        private Label _workLabel = new Label() { 
            Text = "work time", TextAlignment = TextAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center,
        };
        private Label _restLabel = new Label() { Text = "rest time", TextAlignment = TextAlignment.Left,  
            VerticalAlignment = VerticalAlignment.Center,
        };
      
        private TextBox _workTime = new TextBox();
        private TextBox _restTime = new TextBox();
        private Button _saveBtn   = new Button() { Text = "Save" } ;
        private Button _cancelBtn = new Button() { Text = "Cancel" };
        private int notificationId = 0;
        private RadioButtonList rbl = new RadioButtonList { Orientation = RadioButtonListOrientation.Horizontal };
        public event System.Action<Settings> UpdatedSettings;

        private static double FromMsToMin(double i) {
            return i/1000/60;
        }

        private static double FromMinToMs(double i) {
            return i*1000*60;
        }

        public SettingsWindow(Settings settings) 
        {
            
            _workTime.Text = FromMsToMin(settings.WorkTime).ToString();
            _restTime.Text = FromMsToMin(settings.RestTime).ToString();

            _saveBtn.Click += OnSave;
            _cancelBtn.Click += OnCancel;
            var layout = InitLayout();
            Title = " Settings";
            Topmost = true;
            this.Content = layout;          
            //this.AbortButton   = new Button() { Text = "Cancel" };
            //this.DefaultButton = new Button() { Text = "Save" };
            this.Padding = new Eto.Drawing.Padding(10);
            this.Resizable  = false;
            notificationId = settings.NotificationType;

        }

        private Control CreateSpace() {
            return new Splitter(); 
        }

        private Control InitLayout() {
          var layout = new DynamicLayout() { 
              Padding = new Eto.Drawing.Padding(10), 
              Spacing = new Eto.Drawing.Size(5,5),
              DefaultPadding = new Eto.Drawing.Padding(10), 
              DefaultSpacing = new Eto.Drawing.Size(5,5)
          };


            rbl.Items.Add(new ListItem { Text = "Popup", Key = "0" });
            rbl.Items.Add(new ListItem { Text = "Notification", Key = "1" });
            rbl.SelectedKeyChanged += Rbl_SelectedKeyChanged;
            rbl.SelectedKey = notificationId.ToString();
            var splitter = new Splitter();
          layout.BeginVertical (); // create a fields section
          layout.AddRow (null, _restLabel,  _restTime);

          layout.AddRow (null,_workLabel ,  _workTime);
          layout.EndVertical ();
            layout.BeginVertical(); // create a fields section
            layout.AddRow(rbl);
            layout.EndVertical();

            layout.BeginVertical (); // create a fields section
          layout.AddRow(null, _saveBtn, _cancelBtn);
          layout.EndVertical ();


          return layout;
        }

        private void Rbl_SelectedKeyChanged(object sender, EventArgs e)
        {
            notificationId = int.Parse(rbl.SelectedKey);
        }

        private void OnSave(object sender, EventArgs e)
        {
            var restTime = (int)FromMinToMs(int.Parse(_restTime.Text));
            var workTime = (int)FromMinToMs(int.Parse(_workTime.Text));
            UpdatedSettings.Invoke(new Settings {
                RestTime = restTime, WorkTime = workTime, NotificationType = notificationId
            });
            
           this.Hide();

        }
        protected override void OnClosing(CancelEventArgs e)
        {
#if Windows
            e.Cancel = true;
#endif
            Hide();
        }
        public void Hide()
        {
            Visible = false;
        }
        public void Show()
        {
            this.ShowModal();
        }

        private void OnCancel(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
