using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace StopWatch
{
    public class AutoCompleteResultItem
    {
        public object Value { get; private set; }
        public string Text { get; private set; }

        public AutoCompleteResultItem(object value, string text)
        {
            Value = value;
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }

    public class AutoCompleteEventArgs : EventArgs
    {
        public string SearchPattern { get; private set; }
        public AutoCompleteResultItem[] Results { get; set; }

        public AutoCompleteEventArgs(string searchPatttern)
        {
            SearchPattern = searchPatttern;
        }
    }

    public class AutoCompleteTextBox : TextBox
    {
        public event EventHandler<AutoCompleteEventArgs> OnAutoComplete;

        public event EventHandler SelectedValueChanged;

        public object SelectedValue;

        private ListBox _listBox;

        public AutoCompleteTextBox()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            _listBox = new ListBox();
            HideListBox();

            KeyDown += this_KeyDown;
            KeyUp += this_KeyUp;
            KeyPress += this_KeyPress;
        }


        private void ShowListBox()
        {
            if (_listBox.Visible)
                return;

            if (!Parent.Controls.Contains(_listBox))
                Parent.Controls.Add(_listBox);
            _listBox.Left = Left;
            _listBox.Top = Top + Height;
            _listBox.Font = new Font(Font, FontStyle.Regular);
            _listBox.Visible = true;
            _listBox.BringToFront();
        }

        private void HideListBox()
        {
            _listBox.Visible = false;
        }

        private void this_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 0x1b || e.KeyChar == 0x0d || e.KeyChar == 0x09)
            {
                e.Handled = true;
                return;
            }

            UpdateListBox();
        }

        private void this_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.Down)
            {
                UpdateListBox();
                e.Handled = true;
                return;
            }

            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                return;
            }
        }

        private void this_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (IsAutoCompleting())
                {
                    HideListBox();
                    e.Handled = true;
                }
                return;
            }

            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                if (IsAutoCompleting())
                {
                    SelectItem();
                    e.Handled = true;
                }
                return;
            }

            if (e.KeyCode == Keys.Down)
            {
                if (IsAutoCompleting() && _listBox.SelectedIndex < _listBox.Items.Count - 1)
                    _listBox.SelectedIndex++;
                e.Handled = true;
                return;
            }

            if (e.KeyCode == Keys.Up)
            {
                if (IsAutoCompleting() && _listBox.SelectedIndex > 0)
                    _listBox.SelectedIndex--;
                e.Handled = true;
                return;
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down)
                return true;

            if (keyData == Keys.Tab)
                return IsAutoCompleting();

            return base.IsInputKey(keyData);
        }

        private void UpdateListBox()
        {
            AutoCompleteEventArgs evt = new AutoCompleteEventArgs(Text);
            OnAutoComplete?.Invoke(this, evt);

            if (evt.Results == null || evt.Results.Count() == 0)
            {
                HideListBox();
                return;
            }

            ShowListBox();
            _listBox.Items.Clear();
            foreach (var s in evt.Results)
                _listBox.Items.Add(s);
            _listBox.SelectedIndex = 0;
            _listBox.Height = 0;
            _listBox.Width = 0;
            Focus();
            using (Graphics graphics = _listBox.CreateGraphics())
            {
                for (int i = 0; i < _listBox.Items.Count; i++)
                {
                    // Max height at 6 rows
                    if (i < 6)
                        _listBox.Height += _listBox.GetItemHeight(i);
                    int itemWidth = (int)graphics.MeasureString(_listBox.Items[i].ToString() + "_", _listBox.Font).Width;
                    // Max width is Textbox width
                    if (itemWidth > Width)
                        itemWidth = Width;
                    _listBox.Width = (_listBox.Width < itemWidth) ? itemWidth : _listBox.Width;
                }
            }
        }

        private void SelectItem()
        {
            var item = _listBox.SelectedItem as AutoCompleteResultItem;
            if (item == null)
                return;

            SelectedValue = item.Value;
            Text = item.Text;
            SelectionStart = Text.Length;
            SelectionLength = 0;
            HideListBox();

            SelectedValueChanged?.Invoke(this, new EventArgs());
        }

        private bool IsAutoCompleting()
        {
            return _listBox.Visible;
        }
    }
}
