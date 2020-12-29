using System;
using System.Collections.Generic;
using System.Text;
using Eto.Forms;
using Eto.Drawing;
using System.ComponentModel;

#if Windows
namespace pomodoro_dotnet
{
    public class EtoPopup : BasePopupWindow
    {
        Dialog dialog;
        Label label = new Label() { Text = "", HorizontalAlign = HorizontalAlign.Center };

        public EtoPopup()
        {
            dialog = CreateDialogue();
        }
        private Dialog CreateDialogue()
        {
            Dialog item = new Dialog();
            //item.Locations =
            var layout = new DynamicLayout()
            {
                Padding = new Eto.Drawing.Padding(10),
                Spacing = new Eto.Drawing.Size(5, 5),
                DefaultPadding = new Eto.Drawing.Padding(10),
                DefaultSpacing = new Eto.Drawing.Size(5, 5)
            };
            Button okBtn = new Button() { Text = "Ok" };
            okBtn.Click += OnClose;
            var splitter = new Splitter();
            layout.BeginVertical(); // create a fields section
            layout.AddRow(label);
            layout.AddSeparateRow();
            layout.AddRow(okBtn);
            layout.EndVertical();
            item.Content = layout;
            item.Closing += OnClosing;
            return item;
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        void Hide()
        {
            dialog.Visible = false;

        }
        void OnClose(object sender, EventArgs e)
        {
            Hide();
        }
        protected override void Show(string msg)
        {
            label.Text = msg;
            if (dialog.Visible == false)
            {

                dialog.ShowModal();
            }


        }
    }
}
#endif

