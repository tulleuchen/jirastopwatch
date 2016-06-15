/**************************************************************************
Copyright 2015 Carsten Gehling

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
using System.Globalization;

namespace StopWatch
{
    static public class JiraTimeHelpers
    {
        public static string TimeSpanToJiraTime(TimeSpan ts)
        {
            if (ts.Hours > 0)
                return String.Format("{0:%h}h {0:%m}m", ts);

            return String.Format("{0:%m}m", ts);
        }


        public static TimeSpan? JiraTimeToTimeSpan(string time)
        {
            string s;
            decimal t;
            int minutes = 0;
            bool validFormat = true;

            foreach (string timePart in time.Split(' '))
            {
                s = timePart.Trim().ToUpper();

                if (s == "")
                    continue;

                if (!s.Contains("M") && !s.Contains("H"))
                {
                    validFormat = false;
                    break;
                }

                if (!decimal.TryParse(s.Replace("M", "").Replace("H", "").Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out t))
                {
                    validFormat = false;
                    break;
                }

                if (s.Contains("M"))
                    minutes += (int)t;

                if (s.Contains("H"))
                    minutes += (int)(t * 60);
            }

            if (!validFormat)
                return null;

            return new TimeSpan(minutes / 60, minutes % 60, 0);
        }
    }
}

