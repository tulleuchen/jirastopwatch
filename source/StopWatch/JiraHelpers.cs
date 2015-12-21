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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StopWatch
{
    static public class JiraHelpers
    {
        public static string TimeSpanToJiraTime(TimeSpan ts)
        {
            if (ts.Hours > 0)
                return String.Format("{0:%h}h {0:%m}m", ts);

            return String.Format("{0:%m}m", ts);
        }

        public static TimeSpan JiraTimeToTimeSpan(string time)
        {
            int hours = 0;
            int minutes = 0;

            var matchHours = new Regex(@"(\d+)h", RegexOptions.IgnoreCase).Match(time);
            if (matchHours.Success)
                int.TryParse(matchHours.Groups[1].Value, out hours);

            var matchMinutes = new Regex(@"(\d+)m", RegexOptions.IgnoreCase).Match(time);
            if (matchMinutes.Success)
                int.TryParse(matchMinutes.Groups[1].Value, out minutes);

            return new TimeSpan(hours, minutes, 0);
        }
    }
}
