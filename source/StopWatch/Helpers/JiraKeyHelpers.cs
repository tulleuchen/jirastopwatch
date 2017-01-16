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
using System.Text.RegularExpressions;

namespace StopWatch
{
    static public class JiraKeyHelpers
    {
        /// <summary>
        /// If the text supplied is a Jira ssue uRL (by pattern) for the current Jira installatyion
        /// then return only the issue kery.  Otherwise the text is retyurned unchanged
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ParseUrlToKey(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";

            // If text does not contain a key at all, just return it
            if (!new Regex(@"[A-Z0-9_]+-\d+", RegexOptions.Compiled).IsMatch(text))
                return text;

            text = text.Trim();

            // If text only consist of a key - return it
            if (new Regex(@"^[A-Z0-9_]+-\d+$", RegexOptions.Compiled).IsMatch(text))
                return text;

            if (!text.Contains(@"browse/"))
                return "";

            // Remove querystring parameters
            if (text.IndexOf('?') >= 0)
                text = text.Substring(0, text.IndexOf('?'));

            // Remove initial browse path part
            text = text.Substring(text.IndexOf(@"browse/")).Substring(7);

            // Remove trailing path parts
            if (text.IndexOf('/') >= 0)
                text = text.Substring(0, text.IndexOf('/'));

            return text;
        }
    }
}

