using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
