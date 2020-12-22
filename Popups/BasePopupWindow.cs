namespace pomodoro_dotnet
{
    public abstract class BasePopupWindow : IPopup
    {
        string _restMsg = "it's time to make a break";
        string _workMsg = "back to work";
        string _stopMsg = "pomodoro has been stopped";
        public void OnStateChanged(pomodoro_dotnet.State state)
        {
            string msg = string.Empty;
            switch (state)
            {
                case pomodoro_dotnet.State.Resting:
                    msg = _restMsg;
                    break;
                case pomodoro_dotnet.State.Stopped:
                    msg = _stopMsg;
                    break;
                case pomodoro_dotnet.State.Working:
                    msg = _workMsg;
                    break;
            }
            Show(msg);
        }
        protected abstract void Show(string msg);
    }
}
