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
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace StopWatch
{
    public enum SaveTimerSetting
    {
        NoSave,
        SavePause,
        SaveRunActive
    }

    public enum PauseAndResumeSetting
    {
        NoPause,
        Pause,
        PauseAndResume
    }

    public enum WorklogCommentSetting
    {
        WorklogOnly,
        CommentOnly,
        WorklogAndComment
    }

    internal sealed class Settings
    {
        public static readonly Settings Instance = new Settings();

        #region public members
        public string JiraBaseUrl { get; set; }
        public bool AlwaysOnTop { get; set; }
        public bool MinimizeToTray { get; set; }
        public int IssueCount { get; set; }
        public bool AllowMultipleTimers { get; set; }
        public bool IncludeProjectName { get; set; }

        public SaveTimerSetting SaveTimerState { get; set; }
        public PauseAndResumeSetting PauseOnSessionLock { get; set; }
        public WorklogCommentSetting PostWorklogComment { get; set; }

        public string Username { get; set; }
        public string ApiToken { get; set; }
        public bool FirstRun { get; set; }

        public int CurrentFilter { get; set; }

        public List<PersistedIssue> PersistedIssues { get; private set; }

        public string StartTransitions { get; set; }

        public bool LoggingEnabled { get; set; }

        public bool CheckForUpdate { get; set; }
        #endregion


        #region public methods
        public bool Load()
        {
            try
            {
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            }
            catch (ConfigurationErrorsException ex)
            {
                string filename = ex.Filename;
                if (File.Exists(filename))
                    File.Delete(filename);

                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.Reload();
                Properties.Settings.Default.UpgradeRequired = false;
                Properties.Settings.Default.FirstRun = true;
                Properties.Settings.Default.Save();

                ReadSettings();
                return false;
            }

            // Check for upgrade because of application version change
            if (Properties.Settings.Default.UpgradeRequired)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpgradeRequired = false;
                Properties.Settings.Default.Save();
            }

            ReadSettings();

            return true;
        }

        private void ReadSettings()
        {
            this.JiraBaseUrl = Properties.Settings.Default.JiraBaseUrl ?? "";

            this.AlwaysOnTop = Properties.Settings.Default.AlwaysOnTop;
            this.IncludeProjectName = Properties.Settings.Default.IncludeProjectName;
            this.MinimizeToTray = Properties.Settings.Default.MinimizeToTray;
            this.IssueCount = Properties.Settings.Default.IssueCount;
            this.Username = Properties.Settings.Default.Username;
            if (Properties.Settings.Default.ApiToken != "")
                this.ApiToken = DPAPI.Decrypt(Properties.Settings.Default.ApiToken);
            else
                this.ApiToken = "";
            this.FirstRun = Properties.Settings.Default.FirstRun;
            this.SaveTimerState = (SaveTimerSetting)Properties.Settings.Default.SaveTimerState;
            this.PauseOnSessionLock = (PauseAndResumeSetting)Properties.Settings.Default.PauseOnSessionLock;
            this.PostWorklogComment = (WorklogCommentSetting)Properties.Settings.Default.PostWorklogComment;

            this.CurrentFilter = Properties.Settings.Default.CurrentFilter;

            this.PersistedIssues = ReadIssues(Properties.Settings.Default.PersistedIssues);

            this.AllowMultipleTimers = Properties.Settings.Default.AllowMultipleTimers;

            this.StartTransitions = Properties.Settings.Default.StartTransitions;

            this.LoggingEnabled = Properties.Settings.Default.LoggingEnabled;

            CheckForUpdate = Properties.Settings.Default.CheckForUpdate;
        }


        public void Save()
        {
            lock (_writeLock)
            {
                Properties.Settings.Default.JiraBaseUrl = this.JiraBaseUrl;

                Properties.Settings.Default.AlwaysOnTop = this.AlwaysOnTop;
                Properties.Settings.Default.MinimizeToTray = this.MinimizeToTray;
                Properties.Settings.Default.IssueCount = this.IssueCount;
                Properties.Settings.Default.IncludeProjectName = this.IncludeProjectName;

                Properties.Settings.Default.Username = this.Username;
                if (this.ApiToken != "")
                    Properties.Settings.Default.ApiToken = DPAPI.Encrypt(this.ApiToken);
                else
                    Properties.Settings.Default.ApiToken = "";

                Properties.Settings.Default.FirstRun = this.FirstRun;
                Properties.Settings.Default.SaveTimerState = (int)this.SaveTimerState;
                Properties.Settings.Default.PauseOnSessionLock = (int)this.PauseOnSessionLock;
                Properties.Settings.Default.PostWorklogComment = (int)this.PostWorklogComment;

                Properties.Settings.Default.CurrentFilter = this.CurrentFilter;

                Properties.Settings.Default.PersistedIssues = WriteIssues(this.PersistedIssues);

                Properties.Settings.Default.AllowMultipleTimers = this.AllowMultipleTimers;

                Properties.Settings.Default.StartTransitions = this.StartTransitions;

                Properties.Settings.Default.LoggingEnabled = this.LoggingEnabled;

                Properties.Settings.Default.CheckForUpdate = CheckForUpdate;

                Properties.Settings.Default.Save();
            }
        }

        public List<PersistedIssue> ReadIssues(string data)
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.PersistedIssues))
                return new List<PersistedIssue>();

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(data)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (List<PersistedIssue>)bf.Deserialize(ms);
            }

        }


        public string WriteIssues(List<PersistedIssue> issues)
        {
            string s;

            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, issues);
                ms.Position = 0;
                byte[] buffer = new byte[(int)ms.Length];
                ms.Read(buffer, 0, buffer.Length);
                s = Convert.ToBase64String(buffer);
            }

            return s;
        }
        #endregion


        #region private methods
        private Settings()
        {
            this.PersistedIssues = new List<PersistedIssue>();
        }
        #endregion


        private Object _writeLock = new Object(); 
    }
}
