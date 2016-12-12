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
using System.IO;
using System.Threading;

namespace StopWatch.Logging
{
    public class Logger
    {
        private static readonly Logger _instance = new Logger();

        private Logger() {}

        public static Logger Instance
        {
            get
            {
                return _instance;
            }
        }

        public string LogfilePath { get; set; }

        public bool Enabled { get; set; }


        public void Log(string message, Exception exception = null)
        {
            if (!Enabled)
                return;

            if (string.IsNullOrEmpty(LogfilePath))
                return;

            string line = string.Format("{0} {1}", DateTime.Now, message);
            if (exception != null)
                line += string.Format("\n{0}", exception.StackTrace);

            if (Monitor.TryEnter(syncObj, new TimeSpan(0, 0, 1)))
            {
                try
                {
                    if (LogfileMustBeRotated())
                        RotateLogs();

                    using (StreamWriter sw = GetStreamWriter())
                        sw.WriteLine(line);
                }
                finally {
                    Monitor.Exit(syncObj);
                }
            }
        }


        private StreamWriter GetStreamWriter()
        {
            if (File.Exists(LogfilePath)) 
                return File.AppendText(LogfilePath);

            return File.CreateText(LogfilePath);
        }


        private void RotateLogs()
        {
            for (int i = 5; i >= 0; i--)
            {
                string orgFile = i == 0 ? LogfilePath : string.Format("{0}.{1}", LogfilePath, i);
                if (!File.Exists(orgFile))
                    continue;

                if (i == 5)
                {
                    File.Delete(orgFile);
                    continue;
                }

                string newFile = string.Format("{0}.{1}", LogfilePath, i+1);
                File.Move(orgFile, newFile);
            }
        }


        private bool LogfileMustBeRotated()
        {
            if (!File.Exists(LogfilePath))
                return false;
            return new FileInfo(LogfilePath).Length > 1048576;
        }


        private static object syncObj = new object();
    }
}
