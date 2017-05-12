using System;
using System.Linq;
using System.Windows.Forms;
namespace StopWatch
{
    public partial class CreateIssueForm : Form
    {
        public CreateIssueForm(JiraClient jiraClient)
        {
            InitializeComponent();

            actbSearchProject.Values = jiraClient.GetProjects().Select(x => $"{x.Key} - {x.Name}").ToArray();
        }
    }
}
