namespace StopWatchTest
{
    using Moq;
    using NUnit.Framework;
    using RestSharp;
    using StopWatch;
    using System.Collections.Generic;
    using System.Net;

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
            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<object>(It.IsAny<IRestRequest>())).Returns(new object());
            Assert.That(jiraClient.Authenticate("myuser", "mypassword"), Is.True);
        }


        [Test, Description("Authenticate returns false on unsuccessful authentication")]
        public void Authenticate_OnFailure_It_Returns_False()
        {
            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<object>(It.IsAny<IRestRequest>())).Throws<RequestDeniedException>();
            Assert.That(jiraClient.Authenticate("myuser", "mypassword"), Is.False);
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

            //var response = new Mock<IRestResponse<List<Filter>>>();
            //response.SetupGet<List<Filter>>(m => m.Data).Returns(returnData);

            jiraApiRequesterMock.Setup(m => m.DoAuthenticatedRequest<List<Filter>>(It.IsAny<IRestRequest>())).Returns(returnData);

            Assert.That(jiraClient.GetFavoriteFilters(), Is.EqualTo(returnData));
        }
    }
}
