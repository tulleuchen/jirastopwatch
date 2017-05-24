using System;
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

        public CreateIssueForm(JiraClient jiraClient)
        {
            _jiraClient = jiraClient;

            InitializeComponent();

            actbSearchProject.Values = _jiraClient.GetProjects().Select(x => $"{x.Key} - {x.Name}").ToArray();
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
    }
}
