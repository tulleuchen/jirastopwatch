namespace StopWatchTest
{
    using Moq;
    using NUnit.Framework;
    using RestSharp;
    using StopWatch;
    using System;

    [TestFixture]
    public class JiraApiRequestFactoryTest
    {
        private Mock<IRestRequest> requestMock;
        private Mock<IRestRequestFactory> requestFactoryMock;

        private JiraApiRequestFactory jiraApiRequestFactory;

        [SetUp]
        public void Setup()
        {
            requestMock = new Mock<IRestRequest>();

            requestFactoryMock = new Mock<IRestRequestFactory>();
            requestFactoryMock.Setup(m => m.Create(It.IsAny<string>(), It.IsAny<Method>())).Returns(requestMock.Object);

            jiraApiRequestFactory = new JiraApiRequestFactory(requestFactoryMock.Object);
        }



        [Test]
        public void CreateValidateSessionRequest_CreatesValidRequest()
        {
            var request = jiraApiRequestFactory.CreateValidateSessionRequest();
            requestFactoryMock.Verify(m => m.Create("/rest/auth/1/session", Method.GET));
        }


        [Test]
        public void CreateGetFavoriteFiltersRequest_CreatesValidRequest()
        {
            var request = jiraApiRequestFactory.CreateGetFavoriteFiltersRequest();
            requestFactoryMock.Verify(m => m.Create("/rest/api/2/filter/favourite", Method.GET));
        }
        

        [Test]
        public void CreateGetIssuesByJQLRequest_CreatesValidRequest()
        {
            string jql = "status%3Dopen";
            var request = jiraApiRequestFactory.CreateGetIssuesByJQLRequest(jql);
            requestFactoryMock.Verify(m => m.Create(String.Format("/rest/api/2/search?jql={0}", jql), Method.GET));
        }


        [Test]
        public void CreateGetIssueSummaryRequest_CreatesValidRequest()
        {
            string key = "FOO-42";
            var request = jiraApiRequestFactory.CreateGetIssueSummaryRequest(key);
            requestFactoryMock.Verify(m => m.Create(String.Format("/rest/api/2/issue/{0}", key), Method.GET));
        }


        [Test]
        public void CreateGetIssueSummaryRequest_RemoveLeadingAndTrailingSpacesFromIssueKey()
        {
            string key = "   FOO-42   ";
            var request = jiraApiRequestFactory.CreateGetIssueSummaryRequest(key);
            requestFactoryMock.Verify(m => m.Create(String.Format("/rest/api/2/issue/{0}", key.Trim()), Method.GET));
        }


        [Test]
        public void CreatePostWorklogRequest_CreatesValidRequest()
        {
            string key = "FOO-42";
            TimeSpan time = new TimeSpan(1, 2, 0);
            string comment = "Sorry for the inconvenience...";
            var request = jiraApiRequestFactory.CreatePostWorklogRequest(key, time, comment);

            requestFactoryMock.Verify(m => m.Create(String.Format("/rest/api/2/issue/{0}/worklog", key), Method.POST));

            requestMock.VerifySet(m => m.RequestFormat = DataFormat.Json);

            requestMock.Verify(m => m.AddBody(It.Is<object>(o =>
                o.GetHashCode() == (new {
                    timeSpent = JiraTimeHelpers.TimeSpanToJiraTime(time),
                    comment = comment
                }).GetHashCode()
            )));
        }


        [Test]
        public void CreatePostWorklogRequest_RemoveLeadingAndTrailingSpacesFromIssueKey()
        {
            string key = "   FOO-42   ";
            TimeSpan time = new TimeSpan(1, 2, 0);
            string comment = "Sorry for the inconvenience...";
            var request = jiraApiRequestFactory.CreatePostWorklogRequest(key, time, comment);

            requestFactoryMock.Verify(m => m.Create(String.Format("/rest/api/2/issue/{0}/worklog", key.Trim()), Method.POST));
        }


        [Test]
        public void CreatePostCommentRequest_CreatesValidRequest()
        {
            string key = "FOO-42";
            string comment = "Sorry for the inconvenience...";
            var request = jiraApiRequestFactory.CreatePostCommentRequest(key, comment);

            requestFactoryMock.Verify(m => m.Create(String.Format("/rest/api/2/issue/{0}/comment", key), Method.POST));

            requestMock.VerifySet(m => m.RequestFormat = DataFormat.Json);

            requestMock.Verify(m => m.AddBody(It.Is<object>(o =>
                o.GetHashCode() == (new {
                    body = comment
                }).GetHashCode()
            )));
        }


        [Test]
        public void CreatePostCommentRequest_RemoveLeadingAndTrailingSpacesFromIssueKey()
        {
            string key = "   FOO-42   ";
            string comment = "Sorry for the inconvenience...";
            var request = jiraApiRequestFactory.CreatePostCommentRequest(key, comment);

            requestFactoryMock.Verify(m => m.Create(String.Format("/rest/api/2/issue/{0}/comment", key.Trim()), Method.POST));
        }


        [Test]
        public void CreateAuthenticateRequest_CreatesValidRequest()
        {
            string username = "Marvin";
            string password = "IThinkItMakesMeHappy";

            var request = jiraApiRequestFactory.CreateAuthenticateRequest(username, password);

            requestFactoryMock.Verify(m => m.Create("/rest/auth/1/session", Method.POST));

            requestMock.VerifySet(m => m.RequestFormat = DataFormat.Json);

            requestMock.Verify(m => m.AddBody(It.Is<object>(o =>
                o.GetHashCode() == (new {
                    username = username,
                    password = password
                }).GetHashCode()
            )));
        }


        [Test]
        public void CreateReAuthenticateRequest_IfAuthenticateHasNotBeenCalled_ThrowsException()
        {
            Assert.Throws<AuthenticateNotYetCalledException>(() => jiraApiRequestFactory.CreateReAuthenticateRequest());
        }


        [Test]
        public void CreateReAuthenticateRequest_IfAuthenticateHasBeenCalled_CreatesValidRequest()
        {
            string username = "Marvin";
            string password = "IThinkItMakesMeHappy";

            jiraApiRequestFactory.CreateAuthenticateRequest(username, password);
            var request = jiraApiRequestFactory.CreateReAuthenticateRequest();

            requestFactoryMock.Verify(m => m.Create("/rest/auth/1/session", Method.POST));

            requestMock.VerifySet(m => m.RequestFormat = DataFormat.Json);

            requestMock.Verify(m => m.AddBody(It.Is<object>(o =>
                o.GetHashCode() == (new {
                    username = username,
                    password = password
                }).GetHashCode()
            )));
        }
    }
}
