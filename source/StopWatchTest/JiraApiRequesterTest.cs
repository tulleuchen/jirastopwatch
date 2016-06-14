namespace StopWatchTest
{
    using Moq;
    using NUnit.Framework;
    using RestSharp;
    using StopWatch;
    using System.Net;

    internal class TestPocoClass
    {
        public string foo { get; set; }
        public string bar { get; set; }
    }

    [TestFixture]
    public class JiraApiRequesterTest
    {
        private Mock<IRestClient> clientMock;
        private Mock<IRestClientFactory> clientFactoryMock;

        private Mock<IJiraApiRequestFactory> jiraApiRequestFactoryMock;

        private JiraApiRequester jiraApiRequester;

        [SetUp]
        public void Setup()
        {
            clientMock = new Mock<IRestClient>();

            clientFactoryMock = new Mock<IRestClientFactory>();
            clientFactoryMock.Setup(c => c.Create(It.IsAny<bool>())).Returns(clientMock.Object);

            jiraApiRequestFactoryMock = new Mock<IJiraApiRequestFactory>();

            jiraApiRequester = new JiraApiRequester(clientFactoryMock.Object, jiraApiRequestFactoryMock.Object);
        }


        [Test, Description("DoAuthenticatedRequest: On OK, it will not try to ReAuthenticate")]
        public void DoAuthenticatedRequest_OnOK_It_Will_Not_Try_To_ReAuhthenticate()
        {
            clientMock.Setup(c => c.Execute<TestPocoClass>(It.IsAny<IRestRequest>())).Returns(new RestResponse<TestPocoClass>()
            {
                StatusCode = HttpStatusCode.OK
            });

            var response = jiraApiRequester.DoAuthenticatedRequest<TestPocoClass>(new Mock<IRestRequest>().Object);

            jiraApiRequestFactoryMock.Verify(m => m.CreateReAuthenticateRequest(), Times.Never);
        }


        [Test, Description("DoAuthenticatedRequest: On unauthorized, it tries to ReAuthenticate")]
        public void DoAuthenticatedRequest_OnUnauthorized_It_Tries_To_ReAuhthenticate()
        {
            bool authenticated = false;

            clientMock.Setup(c => c.Execute<TestPocoClass>(It.IsAny<IRestRequest>())).Returns(() =>
                new RestResponse<TestPocoClass>()
                {
                    StatusCode = authenticated ?  HttpStatusCode.OK : HttpStatusCode.Unauthorized
                }
            );

            clientMock.Setup(c => c.Execute(It.IsAny<IRestRequest>())).Returns(() => {
                authenticated = true;
                return new RestResponse()
                {
                    StatusCode = HttpStatusCode.OK
                };
            });

            var response = jiraApiRequester.DoAuthenticatedRequest<TestPocoClass>(new Mock<IRestRequest>().Object);

            jiraApiRequestFactoryMock.Verify(m => m.CreateReAuthenticateRequest(), Times.Once);
        }


        [Test, Description("DoAuthenticatedRequest: On BadRequest, it tries to ReAuthenticate")]
        public void DoAuthenticatedRequest_OnBadRequest_It_Tries_To_ReAuhthenticate()
        {
            bool authenticated = false;

            clientMock.Setup(c => c.Execute<TestPocoClass>(It.IsAny<IRestRequest>())).Returns(() =>
                new RestResponse<TestPocoClass>()
                {
                    StatusCode = authenticated ?  HttpStatusCode.OK : HttpStatusCode.BadRequest
                }
            );

            clientMock.Setup(c => c.Execute(It.IsAny<IRestRequest>())).Returns(() => {
                authenticated = true;
                return new RestResponse()
                {
                    StatusCode = HttpStatusCode.OK
                };
            });

            var response = jiraApiRequester.DoAuthenticatedRequest<TestPocoClass>(new Mock<IRestRequest>().Object);

            jiraApiRequestFactoryMock.Verify(m => m.CreateReAuthenticateRequest(), Times.Once);
        }


        [Test, Description("DoAuthenticatedRequest: On unauthorized after ReAuthenticate, it throws an exception")]
        public void DoAuthenticatedRequest_OnUnauthorized_After_ReAuhthenticate_It_Throws_An_Exception()
        {
            clientMock.Setup(c => c.Execute<TestPocoClass>(It.IsAny<IRestRequest>())).Returns(() =>
                new RestResponse<TestPocoClass>()
                {
                    StatusCode = HttpStatusCode.Unauthorized
                }
            );

            clientMock.Setup(c => c.Execute(It.IsAny<IRestRequest>())).Returns(() => {
                return new RestResponse()
                {
                    StatusCode = HttpStatusCode.OK
                };
            });

            Assert.Throws<RequestDeniedException>(() =>
            {
                var response = jiraApiRequester.DoAuthenticatedRequest<TestPocoClass>(new Mock<IRestRequest>().Object);
            });
        }


        [Test, Description("DoAuthenticatedRequest: On ReAuthenticate unauthorized, it throws an exception")]
        public void DoAuthenticatedRequest_On_ReAuthenticate_Unauthorized_It_Throws_An_Exception()
        {
            bool authenticated = false;

            clientMock.Setup(c => c.Execute<TestPocoClass>(It.IsAny<IRestRequest>())).Returns(() =>
                new RestResponse<TestPocoClass>()
                {
                    StatusCode = authenticated ?  HttpStatusCode.OK : HttpStatusCode.Unauthorized
                }
            );

            clientMock.Setup(c => c.Execute(It.IsAny<IRestRequest>())).Returns(() => {
                authenticated = true;
                return new RestResponse()
                {
                    StatusCode = HttpStatusCode.Unauthorized
                };
            });

            Assert.Throws<RequestDeniedException>(() =>
            {
                var response = jiraApiRequester.DoAuthenticatedRequest<TestPocoClass>(new Mock<IRestRequest>().Object);
            });
        }
    }
}
