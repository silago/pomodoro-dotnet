using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace pomodoro_dotnet
{
    public class GladePopup : MessageDialog { 
        [UI] private Button _button = null;

        public GladePopup(string path) : this(new Builder(path)) { }
        private GladePopup(Builder builder) : base(builder.GetObject("_popup").Handle)
        {
            builder.Autoconnect(this);
            _button.Clicked += OnClick;
        }

        private void OnClick(object sender, EventArgs e)
        {
            this.Hide();
        }

     
    }
}
