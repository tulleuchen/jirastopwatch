/**************************************************************************
Copyright 2016 Carsten Gehling

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
**************************************************************************/
using System;

namespace StopWatch
{
    internal class TimerState
    {
        public bool Running { get; set; }

        public TimeSpan TotalTime { get; set; }

        public DateTime StartTime { get; set; }
    }


    internal class WatchTimer
    {
        #region public members
        public TimeSpan TimeElapsed
        {
            get
            {
                if (!Running)
                    return totalTime;

                return totalTime + (DateTime.Now - startTime);
            }

            set
            {
                TimerState state = GetState();
                state.TotalTime = value;
                state.StartTime = DateTime.Now;
                SetState(state);
            }
        }

        public bool Running { get; private set; }
        #endregion


        #region public methods
        public WatchTimer()
        {
            Reset();
        }


        public void Start()
        {
            if (Running)
                return;

            startTime = DateTime.Now;
            Running = true;
        }


        public void Pause()
        {
            if (!Running)
                return;

            totalTime += (DateTime.Now - startTime);
            Running = false;
        }


        public void Reset()
        {
            totalTime = new TimeSpan();
            Running = false;
        }


        public TimerState GetState()
        {
            return new TimerState
            {
                Running = this.Running,
                TotalTime = this.totalTime,
                StartTime = this.startTime
            };
        }


        public void SetState(TimerState state)
        {
            this.Running = state.Running;
            this.totalTime = state.TotalTime;
            this.startTime = state.StartTime;
        }
        #endregion


        #region private members
        private DateTime startTime;

        private TimeSpan totalTime;
        #endregion
    }
}
