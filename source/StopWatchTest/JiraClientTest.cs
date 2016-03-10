namespace StopWatchTest
{
    using Moq;
    using NUnit.Framework;
    using RestSharp;
    using StopWatch;
    using System.Net;

    [TestFixture]
    public class JiraClientTest
    {
        private Mock<IRestClientFactory> clientFactoryMock;
        private Mock<IRestRequestFactory> requestFactoryMock;
        private Mock<IRestClient> clientMock;
        private Mock<IRestRequest> requestMock;

        private JiraClient jiraClient;

        [SetUp]
        public void Setup()
        {
            clientFactoryMock = new Mock<IRestClientFactory>();
            requestFactoryMock = new Mock<IRestRequestFactory>();
            clientMock = new Mock<IRestClient>();
            requestMock = new Mock<IRestRequest>();
            //responseMock = new Mock<IRestResponse>();

            clientFactoryMock.Setup(c => c.Create(It.IsAny<string>(), It.IsAny<bool>())).Returns(clientMock.Object);
            requestFactoryMock.Setup(r => r.Create(It.IsAny<string>(), It.IsAny<Method>())).Returns(requestMock.Object);

            /*
            clientMock.Setup(c => c.Execute(It.IsAny<IRestRequest>())).Returns(new RestResponse()
            {
                StatusCode = HttpStatusCode.OK
            });
            */



            //jiraClient = new JiraClient(clientFactoryMock.Object, requestFactoryMock.Object);
            //jiraClient.BaseUrl = "http://api.example.com";
        }

        [Test, Description("Authenticate returns true on successful authentication")]
        public void Authenticate_OnSuccess_It_Returns_True()
        {
            clientMock.Setup(c => c.Execute(It.IsAny<IRestRequest>())).Returns(new RestResponse()
            {
                StatusCode = HttpStatusCode.OK
            });
            Assert.That(jiraClient.Authenticate("myuser", "mypassword"), Is.True);
        }

        [Test, Description("Authenticate returns false on unsuccessful authentication")]
        public void Authenticate_OnFailure_It_Returns_False()
        {
            clientMock.Setup(c => c.Execute(It.IsAny<IRestRequest>())).Returns(new RestResponse()
            {
                StatusCode = HttpStatusCode.Unauthorized
            });
            Assert.That(jiraClient.Authenticate("myuser", "mypassword"), Is.False);
        }

        [Test, Description("ValidateSession: On success it sets SessionValid and returns true")]
        public void ValidateSession_OnSuccess_It_Sets_SessionValid_And_Returns_True()
        {
            //clientMock.Setup(c => c.Execute<object>(It.IsAny<IRestRequest>())).Returns(new RestResponse<object>()
            //{
            //    StatusCode = HttpStatusCode.OK
            //});
            clientMock.Setup(c => c.Execute(It.IsAny<IRestRequest>())).Returns(new RestResponse()
            {
                StatusCode = HttpStatusCode.OK
            });
            Assert.That(jiraClient.ValidateSession(), Is.True);
            Assert.That(jiraClient.SessionValid, Is.True);
        }

        [Test, Description("ValidateSession: On failure it resets SessionValid and returns false")]
        public void ValidateSession_OnFailure_It_Resets_SessionValid_And_Returns_False()
        {
            clientMock.Setup(c => c.Execute(It.IsAny<IRestRequest>())).Returns(new RestResponse()
            {
                StatusCode = HttpStatusCode.OK
            });
            //clientMock.Setup(c => c.Execute<object>(It.IsAny<IRestRequest>())).Returns(new RestResponse<object>()
            //{
            //    StatusCode = HttpStatusCode.Unauthorized
            //});
            Assert.That(jiraClient.ValidateSession(), Is.False);
            Assert.That(jiraClient.SessionValid, Is.False);
        }




        [Test, Description("DoAuthenticatedRequest: On OK, it will not try to ReAuthenticate")]
        public void DoAuthenticatedRequest_OnOK_It_Will_Not_Try_To_ReAuhthenticate()
        {
            clientMock.Setup(c => c.Execute<object>(It.IsAny<IRestRequest>())).Returns(new RestResponse<object>()
            {
                StatusCode = HttpStatusCode.OK
            });
            //jiraClient.();
            requestFactoryMock.Verify(m => m.Create("/rest/auth/1/session", Method.POST), Times.Never);
        }

        [Test, Description("DoAuthenticatedRequest: On unauthorized, it tries to ReAuthenticate")]
        public void DoAuthenticatedRequest_OnUnauthorized_It_Tries_To_ReAuhthenticate()
        {
            clientMock.Setup(c => c.Execute<object>(It.IsAny<IRestRequest>())).Returns(new RestResponse<object>()
            {
                StatusCode = HttpStatusCode.Unauthorized
            });
            jiraClient.ValidateSession();
            requestFactoryMock.Verify(m => m.Create("/rest/auth/1/session", Method.POST));
        }
    }
}
