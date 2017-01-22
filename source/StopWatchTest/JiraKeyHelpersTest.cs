namespace StopWatchTest
{
    using NUnit.Framework;
    using StopWatch;


    [TestFixture]
    public class JiraKeyHelpersTest
    {
        [Test]
        public void ParseUrlToKey_ReturnsKeyOnFirstMatch()
        {
            Assert.That(JiraKeyHelpers.ParseUrlToKey(@"KEY-123"), Is.EqualTo("KEY-123"));
            Assert.That(JiraKeyHelpers.ParseUrlToKey(@"http://jira.test.local/browse/KEY-123"), Is.EqualTo("KEY-123"));
            Assert.That(JiraKeyHelpers.ParseUrlToKey(@"browse/KEY-123"), Is.EqualTo("KEY-123"));
            Assert.That(JiraKeyHelpers.ParseUrlToKey(@"http://jira.test.local/browse/KEY-123?foo=bar&key=FOO-555"), Is.EqualTo("KEY-123"));
            Assert.That(JiraKeyHelpers.ParseUrlToKey(@"http://jira.test.local/browse/KEY-123/somefoo/qwe?foo=bar&key=FOO-555"), Is.EqualTo("KEY-123"));
            Assert.That(JiraKeyHelpers.ParseUrlToKey(@"http://jira.test.local/bwse/KEY-123"), Is.EqualTo("KEY-123"));
        }

        [Test]
        public void ParseUrlToKey_ReturnsOriginalTextOnNoMatch()
        {
            Assert.That(JiraKeyHelpers.ParseUrlToKey(@"ABC"), Is.EqualTo("ABC"));
            Assert.That(JiraKeyHelpers.ParseUrlToKey(@"http://jira.test.local/bwse/"), Is.EqualTo("http://jira.test.local/bwse/"));
            Assert.That(JiraKeyHelpers.ParseUrlToKey(@"http://jira.test.local/browse/KEY-"), Is.EqualTo("http://jira.test.local/browse/KEY-"));
        }
    }
}
