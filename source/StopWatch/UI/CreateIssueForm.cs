using System;
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

        private void tbDescription_OnPaste(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                string filepath = GetTempFileName("png");
                SaveClipboardImageToFile(filepath);
                PostAttachmentToJira(filepath);
                string filename = Path.GetFileName(filepath);
                tbDescription.Text += @"!{filename}|thumbnail! ";
            }
        }

        private void PostAttachmentToJira(string filepath)
        {
            throw new NotImplementedException();
        }

        private void SaveClipboardImageToFile(string filepath)
        {
            Clipboard.GetImage().Save(filepath, ImageFormat.Png);
        }

        public static string GetTempFileName(string extension)
        {
            int attempt = 0;
            while (true)
            {
                string fileName = Path.GetRandomFileName();
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
    }
}
