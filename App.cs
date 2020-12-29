using System;
using System.Threading;
using System.Threading.Tasks;

namespace pomodoro_dotnet
{
    partial class Program
    {
        public class App {
            State _state;
            State state {
                get { return _state; }
                set {
                    _state = value;
                    StateChanged?.Invoke(value);                    
                    switch (value)
                    {
                        case (State.Resting):
                            endTime = DateTime.Now.AddMilliseconds(RestTime);
                            break;
                        case (State.Working):
                            endTime = DateTime.Now.AddMilliseconds(WorkTime);
                            break;
                        case (State.Stopped):
                            endTime = DateTime.MaxValue;
                            break;
                    }
                }
            }
            public event System.Action<State> StateChanged;
                
            int WorkTime {get; set;}
            int RestTime {get; set;}
            DateTime endTime = DateTime.MaxValue;

            public App(Settings settings, CancellationToken token) {
                SetSettings(settings);
                state = State.Stopped;
                Loop(token);
            }

            public void SetSettings(Settings settings) {
                WorkTime = settings.WorkTime;
                RestTime = settings.RestTime;
            }


            public void ToggleState() {
                switch (state) {
                    case (State.Resting):
                    case (State.Working):
                        state = State.Stopped;
                        endTime = DateTime.MaxValue;
                        break;
                    case (State.Stopped):
                        state = State.Working;
                        endTime = DateTime.Now.AddMilliseconds(WorkTime); 
                        break;
                }
            }

            async void Loop(CancellationToken token) {
                int second = 1000;
                for (;;) {
                    while (DateTime.Now < endTime ) {
                        if (token.IsCancellationRequested) return;
                        //Console.WriteLine("Delay is state "+ state);
                        await Task.Delay(second);
                    }


                    switch (state) {
                        case State.Working:
                            //endTime = DateTime.Now.AddMilliseconds(RestTime);
                            state = State.Resting;
                            break;
                        case State.Resting:
                            //endTime = DateTime.Now.AddMilliseconds(WorkTime);
                            state = State.Working;
                            break;
                    }
                    Console.WriteLine("state " + state);
                }
            }
        }
    }
}
