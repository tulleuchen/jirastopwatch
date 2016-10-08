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
        #endregion


        #region public methods
        public WorklogForm(string comment, EstimateUpdateMethods estimateUpdateMethod, string estimateUpdateValue)
        {
            InitializeComponent();

            if (!String.IsNullOrEmpty(comment))
            {
                tbComment.Text = String.Format("{0}{0}{1}", Environment.NewLine, comment);
                tbComment.SelectionStart = 0;
            }
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
        #endregion

        #region private fields
        
        /// <summary>
        /// Update method for the estimate
        /// </summary>
        private EstimateUpdateMethods _estimateUpdateMethod = EstimateUpdateMethods.Auto;

        #endregion

        #region private eventhandlers
        private void tbComment_KeyDown(object sender, KeyEventArgs e)
        {
            submit_if_ctrl_enter(e);
        }

        private void tbSetTo_KeyDown(object sender, KeyEventArgs e)
        {
            submit_if_ctrl_enter(e);
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
            if (!ValidateTimeInput(tbSetTo))
            {
                e.Cancel = true;                
            }
        }
        private void tbReduceBy_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ValidateTimeInput(tbReduceBy))
            {
                e.Cancel = true;
            }
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
                        this.tbReduceBy.Enabled = false;
                        break;
                    case "rdEstimateAdjustLeave":
                        this._estimateUpdateMethod = EstimateUpdateMethods.Leave;
                        this.tbSetTo.Enabled = false;
                        this.tbReduceBy.Enabled = false;
                        break;
                    case "rdEstimateAdjustSetTo":
                        this._estimateUpdateMethod = EstimateUpdateMethods.SetTo;
                        this.tbSetTo.Enabled = true;
                        this.tbReduceBy.Enabled = false;
                        break;
                    case "rdEstimateAdjustManualDecrease":
                        this._estimateUpdateMethod = EstimateUpdateMethods.ManualDecrease;
                        this.tbSetTo.Enabled = false;
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
            switch(estimateUpdateMethod) {
                case EstimateUpdateMethods.SetTo: 
                    if (!ValidateTimeInput(tbSetTo))
                    {
                        AllValid = false;
                    }
                    break;
                case EstimateUpdateMethods.ManualDecrease:
                    if (!ValidateTimeInput(tbReduceBy))
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
        private bool ValidateTimeInput(TextBox tb)
        {
            if (tb.Enabled)
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    tb.BackColor = Color.Tomato;
                    tb.Select();
                    return false;
                }
                else
                {
                    TimeSpan? time = JiraTimeHelpers.JiraTimeToTimeSpan(tb.Text);
                    if (time == null)
                    {
                        tb.BackColor = Color.Tomato;
                        tb.Select(0, tb.Text.Length);
                        return false;
                    }
                    else{
                        tb.BackColor = SystemColors.Window;
                        return true;
                    }
                }
            } 
            else 
            {
                return true;
            }            
        }
        #endregion
    }
}
