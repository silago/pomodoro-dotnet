namespace pomodoro_dotnet
{
    public class GladePopupWindow : BasePopupWindow
    {
        GladePopup gladePopup;
        public GladePopupWindow(string path)
        {
            gladePopup = new GladePopup(path);
        }

        protected override void Show(string text) {
            gladePopup.Text = text;
            gladePopup.Show();
        }
        
    }
}
