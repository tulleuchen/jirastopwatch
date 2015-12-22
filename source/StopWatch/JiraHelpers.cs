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
using System.Globalization;
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
            string s;
            decimal t;
            int minutes = 0;
            Match m;
            
            m = new Regex(@"(\d+(\.|,\d{1,2})?)m", RegexOptions.IgnoreCase).Match(time);
            if (m.Success)
            {
                s = m.Groups[1].Value.Replace(",", ".");
                if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out t))
                    minutes += (int)t;
            }

            m = new Regex(@"(\d+(\.|,\d{1,2})?)h", RegexOptions.IgnoreCase).Match(time);
            if (m.Success)
            {
                s = m.Groups[1].Value.Replace(",", ".");
                if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out t))
                    minutes += (int)(t * 60);
            }

            return new TimeSpan(minutes / 60, minutes % 60, 0);
        }
    }
}

