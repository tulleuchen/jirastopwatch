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
using System.Windows.Forms;
using System.Drawing;

namespace StopWatch
{
    public partial class WorklogForm : Form
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
                if (this.AllowManualEstimateAdjustments) 
                {
                    return _estimateUpdateMethod;
                }
                else 
                {
                    return EstimateUpdateMethods.Auto;
                }
            }
        }
        public string EstimateValue
        {
            get
            {
                if (this.AllowManualEstimateAdjustments)
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
                else
                {
                    return null;
                }
            }
        }
        #endregion


        #region public methods
        public WorklogForm(bool AllowManualEstimateAdjustments, string comment, EstimateUpdateMethods estimateUpdateMethod, string estimateUpdateValue)
        {
            this.AllowManualEstimateAdjustments = AllowManualEstimateAdjustments;
            InitializeComponent();

            if (!String.IsNullOrEmpty(comment))
            {
                tbComment.Text = String.Format("{0}{0}{1}", Environment.NewLine, comment);
                tbComment.SelectionStart = 0;
            }
            if (this.AllowManualEstimateAdjustments)
            {
                this.gbRemainingEstimate.Visible = true;
                switch( estimateUpdateMethod ) {
                    case EstimateUpdateMethods.Auto:
                        rdEstimateAdjustAuto.Checked = true;
                        break;
                    case EstimateUpdateMethods.Leave:
                        rdEstimateAdjustLeave.Checked = true;
                        break;
                    case EstimateUpdateMethods.SetTo:
                        rdEstimateAdjustSetTo.Checked = true;
                        tbSetTo.Text = estimateUpdateValue;
                        break;
                    case EstimateUpdateMethods.ManualDecrease:
                        rdEstimateAdjustManualDecrease.Checked = true;
                        tbReduceBy.Text = estimateUpdateValue;
                        break;
                }
            }
            else
            {
                this.lblInfo.Location = new Point(
                    this.lblInfo.Location.X,
                    this.lblInfo.Location.Y - this.gbRemainingEstimate.Height
                );
                this.btnSave.Location = new Point(
                    this.btnSave.Location.X,
                    this.btnSave.Location.Y - this.gbRemainingEstimate.Height
                );
                this.btnOk.Location = new Point(
                    this.btnOk.Location.X,
                    this.btnOk.Location.Y - this.gbRemainingEstimate.Height
                );
                this.btnCancel.Location = new Point(
                    this.btnCancel.Location.X,
                    this.btnCancel.Location.Y - this.gbRemainingEstimate.Height
                );
                this.Height -= this.gbRemainingEstimate.Height;

            }
        }
        #endregion

        #region private fields
        
        /// <summary>
        /// Update method for the estimate
        /// </summary>
        private EstimateUpdateMethods _estimateUpdateMethod = EstimateUpdateMethods.Auto;
        private bool tbSetToInvalid = false;
        private bool tbReduceByInvalid = false;
        private bool AllowManualEstimateAdjustments;

        #endregion

        #region private eventhandlers
        private void tbComment_KeyDown(object sender, KeyEventArgs e)
        {
            submit_if_ctrl_enter(e);
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
            submit_if_ctrl_enter(e);
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
            submit_if_ctrl_enter(e);
        }
        private void rdEstimateAdjustAuto_KeyDown(object sender, KeyEventArgs e)
        {
            submit_if_ctrl_enter(e);
        }
        private void rdEstimateAdjustLeave_KeyDown(object sender, KeyEventArgs e)
        {
            submit_if_ctrl_enter(e);
        }
        private void rdEstimateAdjustSetTo_KeyDown(object sender, KeyEventArgs e)
        {
            submit_if_ctrl_enter(e);
        }
        private void rdEstimateAdjustManualDecrease_KeyDown(object sender, KeyEventArgs e)
        {
            submit_if_ctrl_enter(e);
        }
        private void tbSetTo_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Valdiate but do not set the cancel event
            // The reason for this is thats etting the cancel event means you can't leave the field,
            // even to choose a different estimate adjustment option.
            // So valiate (so the colour updates) but do not cancel
            ValidateTimeInput(tbSetTo, false);
        }
        private void tbReduceBy_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Valdiate but do not set the cancel event
            // The reason for this is thats etting the cancel event means you can't leave the field,
            // even to choose a different estimate adjustment option.
            // So valiate (so the colour updates) but do not cancel
            ValidateTimeInput(tbReduceBy, false);
        }

        private void submit_if_ctrl_enter(KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Enter))
            {
                DialogResult = DialogResult.OK;
                post_time_and_close();
                if (DialogResult == DialogResult.OK)
                {
                    Close();
                }
            }
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
            post_time_and_close();
        }

        private void post_time_and_close()
        {
            if (!ValidateAllInputs())
            {
                DialogResult = DialogResult.None;
                return;
            }            
        }
        #endregion       

        #region private utility methods
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
