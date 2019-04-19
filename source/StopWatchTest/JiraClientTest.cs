namespace StopWatchTest
{
    using Moq;
    using NUnit.Framework;
    using RestSharp;
    using StopWatch;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class JiraClientTest
    {
        private Mock<IJiraApiRequestFactory> jiraApiRequestFactoryMock;
        private Mock<IJiraApiRequester> jiraApiRequesterMock;

        private JiraClient jiraClient;


        [SetUp]
        public void Setup()
        {
            jiraApiRequestFactoryMock = new Mock<IJiraApiRequestFactory>();

            jiraApiRequesterMock = new Mock<IJiraApiRequester>();

            jiraClient = new JiraClient(jiraApiRequestFactoryMock.Object, jiraApiRequesterMock.Object);
        }


        [Test, Description("Authenticate returns true on successful authentication")]
        public void Authenticate_OnSuccess_It_Returns_True()
        {
            var jiraConfig = new JiraConfiguration()
            {
                timeTrackingConfiguration = new TimeTrackingConfiguration()
            };
            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<JiraConfiguration>(It.IsAny<IRestRequest>())).Returns(jiraConfig);
            Assert.That(jiraClient.Authenticate("myuser", "myapitoken"), Is.True);
        }


        [Test, Description("Authenticate returns false on unsuccessful authentication")]
        public void Authenticate_OnFailure_It_Returns_False()
        {
            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<JiraConfiguration>(It.IsAny<IRestRequest>())).Throws<RequestDeniedException>();
            Assert.That(jiraClient.Authenticate("myuser", "myapitoken"), Is.False);
        }


        [Test, Description("ValidateSession: On success it sets SessionValid and returns true")]
        public void ValidateSession_OnSuccess_It_Sets_SessionValid_And_Returns_True()
        {
            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<object>(It.IsAny<IRestRequest>())).Returns(new object());
            Assert.That(jiraClient.ValidateSession(), Is.True);
            Assert.That(jiraClient.SessionValid, Is.True);
        }


        [Test, Description("ValidateSession: On failure it resets SessionValid and returns false")]
        public void ValidateSession_OnFailure_It_Resets_SessionValid_And_Returns_False()
        {
            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<object>(It.IsAny<IRestRequest>())).Throws<RequestDeniedException>();
            Assert.That(jiraClient.ValidateSession(), Is.False);
            Assert.That(jiraClient.SessionValid, Is.False);
        }


        [Test, Description("GetFavoriteFilters: On success it returns a list of type filter")]
        public void GetFavoriteFilters_OnSuccess_It_Returns_List_Of_Filters()
        {
            List<Filter> returnData = new List<Filter>();
            returnData.Add(new Filter { Id = 5, Name = "Foo", Jql = "Project=Foo" });
            returnData.Add(new Filter { Id = 6, Name = "bar", Jql = "Project=Bar" });

            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<List<Filter>>(It.IsAny<IRestRequest>())).Returns(returnData);

            Assert.That(jiraClient.GetFavoriteFilters(), Is.EqualTo(returnData));
        }


        [Test, Description("GetFavoriteFilters: On failure it returns null")]
        public void GetFavoriteFilters_OnFailure_It_Returns_Null()
        {
            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<List<Filter>>(It.IsAny<IRestRequest>())).Throws<RequestDeniedException>();
            Assert.That(jiraClient.GetFavoriteFilters(), Is.Null);
        }


        [Test, Description("GetIssuesByJQL: On success it returns a list of type filter")]
        public void GetIssuesByJQL_OnSuccess_It_Returns_List_Of_Issues()
        {
            SearchResult returnData = new SearchResult
            {
                Issues = new List<Issue>()
            };
            returnData.Issues.Add(new Issue { Key = "FOO-1", Fields = new IssueFields { Summary = "Summary for FOO-1" } });
            returnData.Issues.Add(new Issue { Key = "FOO-2", Fields = new IssueFields { Summary = "Summary for FOO-2" } });

            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<SearchResult>(It.IsAny<IRestRequest>())).Returns(returnData);

            Assert.That(jiraClient.GetIssuesByJQL("testjql"), Is.EqualTo(returnData));
        }


        [Test, Description("GetIssuesByJQL: On failure it returns null")]
        public void GetIssuesByJQL_OnFailure_It_Returns_Null()
        {
            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<List<Filter>>(It.IsAny<IRestRequest>())).Throws<RequestDeniedException>();
            Assert.That(jiraClient.GetIssuesByJQL("testjql"), Is.Null);
        }


        [Test, Description("GetIssueSummary: On success it returns a list of type filter")]
        public void GetIssueSummary_OnSuccess_It_Returns_Issue_Summary()
        {
            Issue returnData = new Issue
            {
                Fields = new IssueFields
                {
                    Summary = "The long dark tea-time of the soul"
                }
            };

            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<Issue>(It.IsAny<IRestRequest>())).Returns(returnData);

            Assert.That(jiraClient.GetIssueSummary("DG-42", false), Is.EqualTo(returnData.Fields.Summary));
        }


        [Test, Description("GetIssueSummary: On failure it returns empty string")]
        public void GetIssueSummary_OnFailure_It_Returns_Empty_String()
        {
            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<Issue>(It.IsAny<IRestRequest>())).Throws<RequestDeniedException>();
            Assert.That(jiraClient.GetIssueSummary("DG-42", false), Is.EqualTo(""));
        }

        [Test, Description("GetIssueTimetracking: On success it returns a timetracking object")]
        public void GetIssueTimetracking_OnSuccess_It_Returns_RemainingTime()
        {
            Issue returnData = new Issue
            {
                Fields = new IssueFields
                {
                    Summary = "The long dark tea-time of the soul",
                    Timetracking = new TimetrackingFields
                    {
                        RemainingEstimate = "1h",
                        RemainingEstimateSeconds = 360
                    }
                }
            };

            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<Issue>(It.IsAny<IRestRequest>())).Returns(returnData);

            Assert.That(jiraClient.GetIssueTimetracking("DG-42"), Is.EqualTo(returnData.Fields.Timetracking));
        }


        [Test, Description("GetIssueTimetracking: On failure it returns null")]
        public void GetIssueTimetracking_OnFailure_It_Returns_Empty_String()
        {
            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<Issue>(It.IsAny<IRestRequest>())).Throws<RequestDeniedException>();
            Assert.That(jiraClient.GetIssueTimetracking("DG-42"), Is.Null);
        }


        [Test, Description("PostWorklog: On success it returns true")]
        public void PostWorklog_OnSuccess_It_Returns_True()
        {
            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<object>(It.IsAny<IRestRequest>())).Returns(new object());

            Assert.That(jiraClient.PostWorklog("DG-42", DateTimeOffset.UtcNow, new TimeSpan(1, 20, 0), "Time is an illusion", EstimateUpdateMethods.Auto, null), Is.True);
        }


        [Test, Description("PostWorklog: On failure it returns false")]
        public void PostWorklog_OnFailure_It_Returns_False()
        {
            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<object>(It.IsAny<IRestRequest>())).Throws<RequestDeniedException>();
            Assert.That(jiraClient.PostWorklog("DG-42", DateTimeOffset.UtcNow, new TimeSpan(2, 10, 0), "Lunchtime doubly so", EstimateUpdateMethods.Auto, null), Is.False);
        }


        [Test, Description("PostComment: On success it returns true")]
        public void PostComment_OnSuccess_It_Returns_True()
        {
            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<object>(It.IsAny<IRestRequest>())).Returns(new object());

            Assert.That(jiraClient.PostComment("DG-42", "Time is an illusion"), Is.True);
        }


        [Test, Description("PostComment: On failure it returns false")]
        public void PostComment_OnFailure_It_Returns_False()
        {
            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<object>(It.IsAny<IRestRequest>())).Throws<RequestDeniedException>();
            Assert.That(jiraClient.PostComment("DG-42", "Lunchtime doubly so"), Is.False);
        }


        [Test, Description("GetAvailableTransitions: On success it returns a list transitions currently available for issue")]
        public void GetAvailableTransitions_OnSuccess_It_Returns_List_Of_Issues()
        {
            AvailableTransitions returnData = new AvailableTransitions
            {
                Expand = "transitions",
                Transitions = new List<Transition>()

            };
            returnData.Transitions.Add(new Transition { Id = 8, Name = "Trans1" });
            returnData.Transitions.Add(new Transition { Id = 9, Name = "Trans2" });

            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<AvailableTransitions>(It.IsAny<IRestRequest>())).Returns(returnData);

            Assert.That(jiraClient.GetAvailableTransitions("KEY-3"), Is.EqualTo(returnData));
        }


        [Test, Description("GetAvailableTransitions: On failure it returns null")]
        public void GetAvailableTransitions_OnFailure_It_Returns_Null()
        {
            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<AvailableTransitions>(It.IsAny<IRestRequest>())).Throws<RequestDeniedException>();
            Assert.That(jiraClient.GetAvailableTransitions("KEY-3"), Is.Null);
        }


        [Test, Description("DoTransition: On success it returns true")]
        public void DoTransition_OnSuccess_It_Returns_True()
        {
            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<object>(It.IsAny<IRestRequest>())).Returns(new object());

            Assert.That(jiraClient.DoTransition("DG-42", 6), Is.True);
        }


        [Test, Description("DoTransition: On failure it returns false")]
        public void DoTransition_OnFailure_It_Returns_False()
        {
            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<object>(It.IsAny<IRestRequest>())).Throws<RequestDeniedException>();
            Assert.That(jiraClient.DoTransition("DG-42", 6), Is.False);
        }


    }
}
