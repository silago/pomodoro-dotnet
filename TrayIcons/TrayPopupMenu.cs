namespace pomodoro_dotnet
{
    interface ITrayPopupMenu 
    {
        event System.Action OnClose;
        event System.Action OnSettings;
    }
}
