using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace StopWatch
{
    public class AutoCompleteTextBox : TextBox
    {
        public string[] Values { get; set; }

        private ListBox _listBox;
        private string _formerValue = string.Empty;

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
        }

        private void ShowListBox()
        {
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

        private void this_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateListBox();
        }

        private void this_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Tab:
                case Keys.Enter:
                {
                    if (_listBox.Visible)
                    {
                        SelectItem((string)_listBox.SelectedItem);
                        HideListBox();
                        _formerValue = Text;
                        e.Handled = true;
                    }
                    break;
                }
                case Keys.Down:
                {
                    if ((_listBox.Visible) && (_listBox.SelectedIndex < _listBox.Items.Count - 1))
                        _listBox.SelectedIndex++;
                    e.Handled = true;

                    break;
                }
                case Keys.Up:
                {
                    if ((_listBox.Visible) && (_listBox.SelectedIndex > 0))
                        _listBox.SelectedIndex--;
                    e.Handled = true;

                    break;
                }
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Tab:
                    return true;
                default:
                    return base.IsInputKey(keyData);
            }
        }

        private void UpdateListBox()
        {
            if (Text == _formerValue)
                return;

            if (Values == null || Values.Length == 0)
                return;

            _formerValue = Text;

            string[] matches;
            if (Text.Length == 0)
            {
                matches = Values.OrderBy(x => x).ToArray();
            }
            else
            {
                var m = Values.Select(x => new { Value = x, Score = x.PercentMatchTo(Text) }).ToArray();

                matches = Values
                    .Select(x => new { Value = x, Score = x.PercentMatchTo(Text) })
                    .Where(x => x.Score > 0.01)
                    .OrderByDescending(x => x.Score)
                    .Select(x => x.Value).ToArray();
            }

            if (matches.Length == 0)
            {
                HideListBox();
                return;
            }

            ShowListBox();
            _listBox.Items.Clear();
            Array.ForEach(matches, x => _listBox.Items.Add(x));
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
                    int itemWidth = (int)graphics.MeasureString(((string)_listBox.Items[i]) + "_", _listBox.Font).Width;
                    // Max width is Textbox width
                    if (itemWidth > Width)
                        itemWidth = Width;
                    _listBox.Width = (_listBox.Width < itemWidth) ? itemWidth : _listBox.Width;
                }
            }
        }


        private void SelectItem(string text)
        {
            Text = text;
            if (!string.IsNullOrEmpty(text))
            {
                SelectionStart = text.Length;
                SelectionLength = 0;
            }
        }
    }
}
