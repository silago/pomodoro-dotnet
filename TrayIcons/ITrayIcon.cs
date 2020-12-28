using pomodoro_dotnet;

interface ITrayIcon 
{
    event System.Action OnLeftClick;
    void OnStateChanged(State state);
}
