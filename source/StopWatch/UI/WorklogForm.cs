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
using System.Drawing;
using System.Windows.Forms;

namespace StopWatch
{
    internal partial class WorklogForm : Form
    {
        #region public members
        public string Comment
        {
            get
            {
                return tbComment.Text;
            }
        }
        public EstimateUpdateMethods estimateUpdateMethod
        {
            get
            {
                return _estimateUpdateMethod;
            }
        }
        public string EstimateValue
        {
            get
            {
                switch(this.estimateUpdateMethod)
                {
                    case EstimateUpdateMethods.SetTo:                       
                        return this.tbSetTo.Text;
                    case EstimateUpdateMethods.ManualDecrease:
                        return this.tbReduceBy.Text;
                    case EstimateUpdateMethods.Auto:
                    case EstimateUpdateMethods.Leave:
                    default:
                        return null;                        
                }
            }
        }
        public DateTimeOffset InitialStartTime
        {
            get
            {
                return this.startDatePicker.Value.Date + this.startTimePicker.Value.TimeOfDay;
            }
        }
        public string RemainingEstimate
        {
            get
            {
                return _RemainingEstimate;
            }
            set
            {
                _RemainingEstimate = value;
                RemainingEstimateUpdated();
            }
        }

        public int RemainingEstimateSeconds
        {
            get
            {
                return _RemainingEstimateSeconds;
            }
            set
            {
                _RemainingEstimateSeconds = value;
                RemainingEstimateUpdated();
            }
        }
        #endregion


        #region public methods
        public WorklogForm(IssueControl issueControl)
        {
            this.TimeElapsed = issueControl.WatchTimer.TimeElapsedNearestMinute;
            DateTimeOffset initialStartTime = issueControl.WatchTimer.GetInitialStartTime();
            if (initialStartTime == null)
            {
                initialStartTime = DateTimeOffset.UtcNow.Subtract(TimeElapsed);
            }
            InitializeComponent();
            if (!String.IsNullOrEmpty(issueControl.Comment))
            {
                tbComment.Text = String.Format("{0}{0}{1}", Environment.NewLine, issueControl.Comment);
                tbComment.SelectionStart = 0;
            }

            // I don't see why I need to do this, but the first time I call LocalDateTime it seems to change time zone on the actual Date4TimeOffset
            // So I don't get the right time.  So I call just once and update both from the same object
            DateTime localInitialStartTime = initialStartTime.LocalDateTime;
            this.startDatePicker.Value = localInitialStartTime;
            this.startTimePicker.Value = localInitialStartTime;

            switch ( issueControl.EstimateUpdateMethod ) {
                case EstimateUpdateMethods.Auto:
                    rdEstimateAdjustAuto.Checked = true;
                    break;
                case EstimateUpdateMethods.Leave:
                    rdEstimateAdjustLeave.Checked = true;
                    break;
                case EstimateUpdateMethods.SetTo:
                    rdEstimateAdjustSetTo.Checked = true;
                    tbSetTo.Text = issueControl.EstimateUpdateValue;
                    break;
                case EstimateUpdateMethods.ManualDecrease:
                    rdEstimateAdjustManualDecrease.Checked = true;
                    tbReduceBy.Text = issueControl.EstimateUpdateValue;
                    break;
            }

            this.Text = "Submit worklog for " + issueControl.IssueKey + ": " + issueControl.Summary;
        }
        #endregion

        #region private fields
        
        /// <summary>
        /// Update method for the estimate
        /// </summary>
        private EstimateUpdateMethods _estimateUpdateMethod = EstimateUpdateMethods.Auto;
        private bool tbSetToInvalid = false;
        private bool tbReduceByInvalid = false;
        private string _RemainingEstimate;
        private int _RemainingEstimateSeconds;
        private TimeSpan TimeElapsed;
        #endregion

        #region private eventhandlers
        private void tbComment_KeyDown(object sender, KeyEventArgs e)
        {
            SubmitIfCtrlEnter(e);
        }

        private void tbSetTo_KeyUp(object sender, KeyEventArgs e)
        {
            if (tbSetToInvalid)
            {
                ValidateTimeInput(tbSetTo, false);
            }
        }
        private void tbSetTo_KeyDown(object sender, KeyEventArgs e)
        {
            SubmitIfEnter(e);
        }
        private void tbReduceBy_KeyUp(object sender, KeyEventArgs e)
        {
            if (tbReduceByInvalid)
            {
                ValidateTimeInput(tbReduceBy, false);
            }
        }
        private void tbReduceBy_KeyDown(object sender, KeyEventArgs e)
        {
            SubmitIfEnter(e);
        }
        private void rdEstimateAdjustAuto_KeyDown(object sender, KeyEventArgs e)
        {
            SubmitIfEnter(e);
        }
        private void rdEstimateAdjustLeave_KeyDown(object sender, KeyEventArgs e)
        {
            SubmitIfEnter(e);
        }
        private void rdEstimateAdjustSetTo_KeyDown(object sender, KeyEventArgs e)
        {
            SubmitIfEnter(e);
        }
        private void rdEstimateAdjustManualDecrease_KeyDown(object sender, KeyEventArgs e)
        {
            SubmitIfEnter(e);
        }
        private void tbSetTo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Valdiate but do not set the cancel event
            // The reason for this is that setting the cancel event means you can't leave the field,
            // even to choose a different estimate adjustment option.
            // So valiate (so the colour updates) but do not cancel
            ValidateTimeInput(tbSetTo, false);
        }
        private void tbReduceBy_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Valdiate but do not set the cancel event
            // The reason for this is that setting the cancel event means you can't leave the field,
            // even to choose a different estimate adjustment option.
            // So valiate (so the colour updates) but do not cancel
            ValidateTimeInput(tbReduceBy, false);
        }

        private void SubmitIfCtrlEnter(KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Enter))
                SubmitForm();
        }

        private void SubmitIfEnter(KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Enter))
                SubmitForm();
        }

        private void SubmitForm()
        {
            DialogResult = DialogResult.OK;
            PostTimeAndClose();
            if (DialogResult == DialogResult.OK)
                Close();
        }

        private void estimateUpdateMethod_changed(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;
            if (button != null && button.Checked)
            {
                switch (button.Name)
                {
                    case "rdEstimateAdjustAuto":
                        this._estimateUpdateMethod = EstimateUpdateMethods.Auto;
                        this.tbSetTo.Enabled = false;
                        this.tbSetTo.BackColor = SystemColors.Window;
                        this.tbReduceBy.Enabled = false;
                        this.tbReduceBy.BackColor = SystemColors.Window;
                        break;
                    case "rdEstimateAdjustLeave":
                        this._estimateUpdateMethod = EstimateUpdateMethods.Leave;
                        this.tbSetTo.Enabled = false;
                        this.tbSetTo.BackColor = SystemColors.Window;
                        this.tbReduceBy.Enabled = false;
                        this.tbReduceBy.BackColor = SystemColors.Window;
                        break;
                    case "rdEstimateAdjustSetTo":
                        this._estimateUpdateMethod = EstimateUpdateMethods.SetTo;
                        this.tbSetTo.Enabled = true;
                        this.tbReduceBy.Enabled = false;
                        this.tbReduceBy.BackColor = SystemColors.Window;
                        break;
                    case "rdEstimateAdjustManualDecrease":
                        this._estimateUpdateMethod = EstimateUpdateMethods.ManualDecrease;
                        this.tbSetTo.Enabled = false;
                        this.tbSetTo.BackColor = SystemColors.Window;
                        this.tbReduceBy.Enabled = true;                        
                        break;
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            PostTimeAndClose();
        }

        private void PostTimeAndClose()
        {
            if (!ValidateAllInputs())
            {
                DialogResult = DialogResult.None;
                return;
            }            
        }
        #endregion       

        #region private utility methods

        private void RemainingEstimateUpdated()
        {
            if (string.IsNullOrWhiteSpace(RemainingEstimate))
            {
                rdEstimateAdjustLeave.Text = "&Leave Unchanged";
            }
            else
            {
                rdEstimateAdjustLeave.Text = string.Format("&Leave As {0}", _RemainingEstimate);
            }

            if( TimeElapsed != null && RemainingEstimateSeconds > 0){
                rdEstimateAdjustAuto.Text = string.Format("Adjust &Automatically (to {0})", calculatedAdjustedRemainingEstimate());
            }else {
                rdEstimateAdjustAuto.Text = "Adjust &Automatically";
            }
        }

        private string calculatedAdjustedRemainingEstimate()
        {
            
            int AdjustedRemainingSeconds = RemainingEstimateSeconds - (int)Math.Floor(TimeElapsed.TotalSeconds);
            if (AdjustedRemainingSeconds > 0)
            {
                TimeSpan AdjustedRemaining = new TimeSpan(0, 0, AdjustedRemainingSeconds);
                return JiraTimeHelpers.TimeSpanToJiraTime(AdjustedRemaining);
            }
            else
            {
                return "0m";
            }
        }

        private void foo(string text)
        {

        }

        /// <summary>
        /// Validates the required inputs.  Returns
        /// </summary>
        /// <returns></returns>
        private bool ValidateAllInputs()
        {
            Boolean AllValid = true;
            tbSetToInvalid = false;
            tbReduceByInvalid = false;
            switch(estimateUpdateMethod) {
                case EstimateUpdateMethods.SetTo: 
                    if (!ValidateTimeInput(tbSetTo, true))
                    {
                        AllValid = false;                        
                    }
                    break;
                case EstimateUpdateMethods.ManualDecrease:
                    if (!ValidateTimeInput(tbReduceBy, true))
                    {
                        AllValid = false;                        
                    }
                    break;
            }

            return AllValid;
        }
        /// <summary>
        /// Checks if the time entered in the submitted textbox is valid
        /// Marks it as invalid if it is not
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        private bool ValidateTimeInput(TextBox tb, bool FocusIfInvalid)
        {
            bool fieldIsValid;
            if (tb.Enabled)
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    tb.BackColor = Color.Tomato;
                    if (FocusIfInvalid)
                    {
                        tb.Select();
                    }
                    fieldIsValid = false;
                }
                else
                {
                    TimeSpan? time = JiraTimeHelpers.JiraTimeToTimeSpan(tb.Text);
                    if (time == null)
                    {
                        tb.BackColor = Color.Tomato;
                        if (FocusIfInvalid)
                        {
                            tb.Select(0, tb.Text.Length);
                        }
                        fieldIsValid = false;
                    }
                    else{
                        tb.BackColor = SystemColors.Window;
                        fieldIsValid = true;
                    }
                }
            } 
            else 
            {
                fieldIsValid = true;
            }

            switch(tb.Name)
            {
                case "tbSetTo":
                    tbSetToInvalid = !fieldIsValid;
                    break;
                case "tbReduceBy":
                    tbReduceByInvalid = !fieldIsValid;
                    break;
            }
            return fieldIsValid;
        }
        #endregion
    }
}
