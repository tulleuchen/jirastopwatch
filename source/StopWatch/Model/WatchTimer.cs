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

        public DateTime SessionStartTime { get; set; }
        public DateTimeOffset? InitialStartTime { get; set; }
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

                return totalTime + (DateTime.Now - sessionStartTime);
            }

            set
            {
                TimerState state = GetState();
                state.TotalTime = value;
                state.SessionStartTime = DateTime.Now;
                state.InitialStartTime = initialStartTime;
                SetState(state);
            }
        }

        public TimeSpan TimeElapsedNearestMinute
        {
            get
            {
                return TimeSpan.FromMinutes(Math.Ceiling(TimeElapsed.TotalMinutes));
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
            sessionStartTime = DateTime.Now;
            if( initialStartTime == null)
            {
                initialStartTime = DateTimeOffset.UtcNow;
            }
            Running = true;
        }


        public void Pause()
        {
            if (!Running)
                return;

            totalTime += (DateTime.Now - sessionStartTime);
            Running = false;
        }


        public void Reset()
        {
            totalTime = new TimeSpan();
            Running = false;
            initialStartTime = null;
        }


        public TimerState GetState()
        {
            if (Running)
            {
                var now = DateTime.Now;
                return new TimerState
                {
                    Running = Running,
                    TotalTime = totalTime + (now - sessionStartTime),
                    SessionStartTime = now,
                    InitialStartTime = initialStartTime
                };
            }

            return new TimerState
            {
                Running = Running,
                TotalTime = totalTime,
                SessionStartTime = sessionStartTime,
                InitialStartTime = initialStartTime
            };
        }


        public void SetState(TimerState state)
        {
            this.Running = state.Running;
            this.totalTime = state.TotalTime;
            this.sessionStartTime = state.SessionStartTime;
            this.initialStartTime = state.InitialStartTime;
        }
        
        /// <summary>
        /// Returns the initial start time to sue when logging
        /// We check both the estimated time (i.e. now minus the elapsed time) and the 
        /// recorded start time (when you first pressed play on this issue) and return the earliest
        /// This should mean that if you start and pause and restart the timer you get the actual time you first started
        /// but if you manually edit the time spent (e.g. forgot to start the timer) then you get the estimated one
        /// </summary>
        /// <returns></returns>
        public DateTimeOffset GetInitialStartTime()
        {
            var estimatedStartTime = DateTimeOffset.UtcNow.Subtract(TimeElapsed);
            if(initialStartTime == null || estimatedStartTime < initialStartTime)
            {
                return estimatedStartTime;
            } else
            {
                return (DateTimeOffset)initialStartTime;
            }
        }
        #endregion


        #region private members
        private DateTimeOffset? initialStartTime;

        private DateTime sessionStartTime;

        private TimeSpan totalTime;
        #endregion
    }
}
