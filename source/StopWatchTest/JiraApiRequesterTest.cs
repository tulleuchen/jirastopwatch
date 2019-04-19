namespace StopWatchTest
{
    using Moq;
    using NUnit.Framework;
    using RestSharp;
    using StopWatch;
    using System.Linq;
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

        private const string VALID_USERNAME = "myusername";
        private const string VALID_APITOKEN = "myapitoken";

        [SetUp]
        public void Setup()
        {
            clientMock = new Mock<IRestClient>();

            clientFactoryMock = new Mock<IRestClientFactory>();
            clientFactoryMock.Setup(c => c.Create(It.IsAny<bool>())).Returns(clientMock.Object);

            jiraApiRequestFactoryMock = new Mock<IJiraApiRequestFactory>();

            jiraApiRequester = new JiraApiRequester(clientFactoryMock.Object, jiraApiRequestFactoryMock.Object);
        }

        private IRestResponse<TestPocoClass> TestAuth(IRestRequest requestMock)
        {
            var authParam = requestMock.Parameters.FirstOrDefault(p => p.Type == ParameterType.HttpHeader && p.Name == "Authorization");
            const string prefix = "Basic ";
            if (authParam != null)
            {
                if (authParam.Value is string && ((string)authParam.Value).StartsWith(prefix))
                {
                    var base64 = ((string)authParam.Value).Substring(prefix.Length);
                    try
                    {
                        string authString = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(base64));
                        var comps = authString.Split(':');
                        if (comps.Length == 2 && comps[0] == VALID_USERNAME && comps[1] == VALID_APITOKEN)
                        {
                            return new RestResponse<TestPocoClass>()
                            {
                                StatusCode = HttpStatusCode.OK,
                                Data = new TestPocoClass() { foo = "foo", bar = "bar" },
                            };
                        }
                    }
                    catch (System.Exception)
                    { }
                }
            }
            return new RestResponse<TestPocoClass>()
            {
                StatusCode = HttpStatusCode.Unauthorized
            };
        }

        [Test, Description("DoAuthenticatedRequest: with correct credentials return data without error message")]
        public void DoAuthenticatedRequest_WithValidCredentials()
        {
            var requestMock = new RestRequest();

            clientMock.Setup(c => c.Execute<TestPocoClass>(It.IsAny<IRestRequest>())).Returns(() => TestAuth(requestMock));

            jiraApiRequester.SetAuthentication(VALID_USERNAME, VALID_APITOKEN);

            var response = jiraApiRequester.DoAuthenticatedRequest<TestPocoClass>(requestMock);

            Assert.NotNull(response);
            Assert.IsEmpty(jiraApiRequester.ErrorMessage);
        }

        [Test, Description("DoAuthenticatedRequest: with wrong credentials it throws an exception")]
        public void DoAuthenticatedRequest_WithInvalidCredentials()
        {
            var requestMock = new RestRequest();

            clientMock.Setup(c => c.Execute<TestPocoClass>(It.IsAny<IRestRequest>())).Returns(() => TestAuth(requestMock));

            jiraApiRequester.SetAuthentication("invalidUsername", "invalidApiToken");

            Assert.Throws<RequestDeniedException>(() =>
            {
                var response = jiraApiRequester.DoAuthenticatedRequest<TestPocoClass>(requestMock);
            });
        }

    }
}
