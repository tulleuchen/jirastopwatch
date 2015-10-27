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
namespace StopWatch
{
    public class Settings
    {
        #region public members
        public string JiraBaseUrl;
        public bool AlwaysOnTop;
        public int IssueCount;

        public SaveTimerSetting SaveTimerState;

        public string Username;
        public string Password;
        public bool RememberCredentials;
        public bool FirstRun;
        #endregion


        #region public methods
        public Settings()
        {
        }


        public void Load()
        {
            this.JiraBaseUrl = Properties.Settings.Default.JiraBaseUrl ?? "";

            this.AlwaysOnTop = Properties.Settings.Default.AlwaysOnTop;
            this.IssueCount = Properties.Settings.Default.IssueCount;
            this.Username = Properties.Settings.Default.Username;
            if (Properties.Settings.Default.Password != "")
                this.Password = DPAPI.Decrypt(Properties.Settings.Default.Password);
            else
                this.Password = "";
            this.RememberCredentials = Properties.Settings.Default.RememberCredentials;
            this.FirstRun = Properties.Settings.Default.FirstRun;
            this.SaveTimerState = (SaveTimerSetting)Properties.Settings.Default.SaveTimerState;
        }


        public void Save()
        {
            Properties.Settings.Default.JiraBaseUrl = this.JiraBaseUrl;

            Properties.Settings.Default.AlwaysOnTop = this.AlwaysOnTop;
            Properties.Settings.Default.IssueCount = this.IssueCount;
            Properties.Settings.Default.Username = this.Username;
            if (this.Password != "")
                Properties.Settings.Default.Password = DPAPI.Encrypt(this.Password);
            else
                Properties.Settings.Default.Password = "";
            Properties.Settings.Default.RememberCredentials = this.RememberCredentials;
            Properties.Settings.Default.FirstRun = this.FirstRun;
            Properties.Settings.Default.SaveTimerState = (int)this.SaveTimerState;

            Properties.Settings.Default.Save();
        }
        #endregion
    }
}
