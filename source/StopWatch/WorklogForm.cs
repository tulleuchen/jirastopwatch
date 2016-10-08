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
        public WorklogForm(string comment)
        {
            InitializeComponent();

            if (!String.IsNullOrEmpty(comment))
            {
                tbComment.Text = String.Format("{0}{0}{1}", Environment.NewLine, comment);
                tbComment.SelectionStart = 0;
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

        private void submit_if_ctrl_enter(KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Enter))
            {
                DialogResult = DialogResult.OK;
                Close();
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

        #endregion       
    }
}
