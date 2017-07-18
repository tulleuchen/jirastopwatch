using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StopWatch
{
    public partial class CreateIssueForm : Form
    {
        private Object _searchLock = new Object();
        private JiraClient _jiraClient;

        //private AutoCompleteResultItem[] _projects;
        private CreateIssueMeta _createIssueMeta;
        private List<string> _images;

        public CreateIssueForm(JiraClient jiraClient)
        {
            _images = new List<string>();

            _jiraClient = jiraClient;

            InitializeComponent();

            Task.Factory.StartNew(
                () =>
                {
                    var createIssueMeta = _jiraClient.GetCreateIssueMeta();
///rest/api/2/issue/createmeta?projectKeys=QA&issuetypeNames=Bug&expand=projects.issuetypes.fields
                    this.InvokeIfRequired(
                        () => _createIssueMeta = createIssueMeta
                    );
                }
            );
        }

        private void tbSummary_TextChanged(object sender, EventArgs e)
        {
            RetrieveAndDisplayRelatedIssues(tbSummary.Text);
        }

        private void RetrieveAndDisplayRelatedIssues(string text)
        {
            Task.Factory.StartNew(
                () =>
                {
                    if (!Monitor.TryEnter(_searchLock))
                        return;

                    try
                    {
                        string jql = GetJQL(text);
                        if (jql != "")
                        {
                            var result = _jiraClient.GetIssuesByJQL(jql, 10);

                            this.InvokeIfRequired(
                                () =>
                                {
                                    lvRelatedIssues.Items.Clear();
                                    foreach (var issue in result.Issues)
                                    {
                                        lvRelatedIssues.Items.Add($"{issue.Key} . {issue.Fields.Summary}");
                                    }
                                }
                            );
                        }
                    }
                    finally
                    {
                        Monitor.Exit(_searchLock);
                    }
                }
            );
        }

        private string GetJQL(string text)
        {
            text = string.Join(" ", text.Split(null)
                .Where(t => t.Length > 3)
                .Select(t => $"{t}*"));

            if (text == "")
                return "";

            return $"text ~ \"{text}\"";
        }

        private void lvRelatedIssues_DoubleClick(object sender, EventArgs e)
        {
            if (lvRelatedIssues.SelectedItems.Count == 0)
                return;

            string item = lvRelatedIssues.SelectedItems[0].Text;
            string key = item.Split(null)[0];

            string url = Settings.Instance.JiraBaseUrl;
            if (!url.EndsWith("/"))
                url += "/";
            url += "browse/";
            url += key.Trim();
            System.Diagnostics.Process.Start(url);
        }

        private void tbDescription_OnPaste(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                string filepath = GetTempFileName("screenshot-", "png");
                SaveClipboardImageToFile(filepath);
                _images.Add(filepath);

                tbDescription.Text += $" !{Path.GetFileName(filepath)}|thumbnail! ";
                tbDescription.SelectionStart = tbDescription.Text.Length;
                tbDescription.SelectionLength = 0;
            }
        }

        private void SaveClipboardImageToFile(string filepath)
        {
            Clipboard.GetImage().Save(filepath, ImageFormat.Png);
        }

        public static string GetTempFileName(string prefix, string extension)
        {
            int attempt = 0;
            while (true)
            {
                string fileName = $"{prefix}-{Path.GetRandomFileName()}";
                fileName = Path.ChangeExtension(fileName, extension);
                fileName = Path.Combine(Path.GetTempPath(), fileName);

                try
                {
                    using (new FileStream(fileName, FileMode.CreateNew)) { }
                    return fileName;
                }
                catch (IOException ex)
                {
                    if (++attempt == 10)
                        throw new IOException("No unique temporary file name is available.", ex);
                }
            }
        }

        private void actbSearchProject_OnAutoComplete(object sender, AutoCompleteEventArgs e)
        {
            if (_createIssueMeta?.Projects == null)
                return;

            var items = _createIssueMeta.Projects.Select(x => new AutoCompleteResultItem(x.Id.ToString(), $"{x.Key} - {x.Name}")).ToArray();

            if (string.IsNullOrEmpty(e.SearchPattern))
            {
                e.Results = items.OrderBy(x => x.Text).ToArray();
                return;
            }

            e.Results = items
                .Select(x => new { Item = x, Score = x.Text.PercentMatchTo(e.SearchPattern) })
                .Where(x => x.Score > 0.01)
                .OrderByDescending(x => x.Score)
                .Select(x => x.Item).ToArray();
        }

        private void btnCreateIssue_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                DialogResult = DialogResult.None;
                return;
            }

            CreateIssue();
        }

        private bool ValidateForm()
        {
            bool valid = true;

            if (cbIssueType.SelectedIndex == -1)
            {
                cbIssueType.BackColor = Color.LightPink;
                valid = false;
            }
            else
                cbIssueType.BackColor = SystemColors.Window;

            if (actbSearchProject.SelectedValue == null)
            {
                actbSearchProject.BackColor = Color.LightPink;
                valid = false;
            }
            else
                actbSearchProject.BackColor = SystemColors.Window;

            if (actbAssignee.SelectedValue == null)
            {
                actbAssignee.BackColor = Color.LightPink;
                valid = false;
            }
            else
                actbAssignee.BackColor = SystemColors.Window;

            if (string.IsNullOrWhiteSpace(tbSummary.Text))
            {
                tbSummary.BackColor = Color.LightPink;
                valid = false;
            }
            else
                tbSummary.BackColor = SystemColors.Window;

            if (string.IsNullOrWhiteSpace(tbDescription.Text))
            {
                tbDescription.BackColor = Color.LightPink;
                valid = false;
            }
            else
                tbDescription.BackColor = SystemColors.Window;

            return valid;
        }

        private void CreateIssue()
        {
            int projectId = Convert.ToInt32(actbSearchProject.SelectedValue);
            var issueType = cbIssueType.Items[cbIssueType.SelectedIndex] as IssueType;
            string assignee = Convert.ToString(actbAssignee.SelectedValue);
            string issueKey = _jiraClient.CreateIssue(projectId, issueType.Id, tbSummary.Text, tbDescription.Text, assignee);
        }

        private void actbAssignee_OnAutoComplete(object sender, AutoCompleteEventArgs e)
        {
            var users = _jiraClient.FindUsers(e.SearchPattern);
            e.Results = users.Select(x => new AutoCompleteResultItem(x.Name, x.DisplayName)).ToArray();
        }

        private void btnAssignToMe_Click(object sender, EventArgs e)
        {
            var user = _jiraClient.GetMyself();
            actbAssignee.SelectedValue = user.Name;
            actbAssignee.Text = user.DisplayName;
        }

        private void actbSearchProject_SelectedValueChanged(object sender, EventArgs e)
        {
            cbIssueType.Items.Clear();

            int projectId = Convert.ToInt32(actbSearchProject.SelectedValue);
            if (projectId == 0)
                return;

            var issueTypes = _createIssueMeta.Projects
                .Where(p => p.Id == projectId)
                .First()
                .IssueTypes
                .Where(x => !x.SubTask);

            foreach (var it in issueTypes)
                cbIssueType.Items.Add(it);
        }
    }
}
