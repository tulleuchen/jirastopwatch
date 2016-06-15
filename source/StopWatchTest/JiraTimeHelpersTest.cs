namespace StopWatchTest
{
    using NUnit.Framework;
    using StopWatch;

    [TestFixture]
    public class JiraTimeHelpersTest
    {
        [Test]
        public void JiraTimeToTimeSpan_InvalidMinutesFails()
        {
            Assert.IsNull(JiraTimeHelpers.JiraTimeToTimeSpan("m"));
            Assert.IsNull(JiraTimeHelpers.JiraTimeToTimeSpan("2 m"));
        }

        [Test]
        public void JiraTimeToTimeSpan_InvalidHoursFails()
        {
            Assert.IsNull(JiraTimeHelpers.JiraTimeToTimeSpan("h"));
            Assert.IsNull(JiraTimeHelpers.JiraTimeToTimeSpan("8 h"));
        }

        [Test]
        public void JiraTimeToTimeSpan_ValidHoursWithInvalidMinutesFails()
        {
            Assert.IsNull(JiraTimeHelpers.JiraTimeToTimeSpan("2h 5"));
            Assert.IsNull(JiraTimeHelpers.JiraTimeToTimeSpan("2h m"));
        }

        [Test]
        public void JiraTimeToTimeSpan_InvalidHoursWithValidMinutesFails()
        {
            Assert.IsNull(JiraTimeHelpers.JiraTimeToTimeSpan("2 5m"));
            Assert.IsNull(JiraTimeHelpers.JiraTimeToTimeSpan("h 5m"));
        }

        [Test]
        public void JiraTimeToTimeSpan_ParsesJiraStyleTimespan()
        {
            Assert.AreEqual(120, JiraTimeHelpers.JiraTimeToTimeSpan("2h").Value.TotalMinutes);
            Assert.AreEqual(125, JiraTimeHelpers.JiraTimeToTimeSpan("2h 5m").Value.TotalMinutes);
            Assert.AreEqual(5, JiraTimeHelpers.JiraTimeToTimeSpan("5m").Value.TotalMinutes);
        }

        [Test]
        public void JiraTimeToTimeSpan_ParsesDecimalHours()
        {
            Assert.AreEqual(150, JiraTimeHelpers.JiraTimeToTimeSpan("2.5h").Value.TotalMinutes);
        }

        [Test]
        public void JiraTimeToTimeSpan_IgnoresDecimalValueForMinutes()
        {
            Assert.AreEqual(600, JiraTimeHelpers.JiraTimeToTimeSpan("10.5m").Value.TotalSeconds);
        }

        [Test]
        public void JiraTimeToTimeSpan_AllowsMinutesBeforeHours()
        {
            Assert.AreEqual(125, JiraTimeHelpers.JiraTimeToTimeSpan("5m 2h").Value.TotalMinutes);
        }

        [Test]
        public void JiraTimeToTimeSpan_AllowsSillyValues()
        {
            Assert.AreEqual(120, JiraTimeHelpers.JiraTimeToTimeSpan("2h 0m").Value.TotalMinutes);
            Assert.AreEqual(5, JiraTimeHelpers.JiraTimeToTimeSpan("0h 5m").Value.TotalMinutes);
        }

        [Test]
        public void JiraTimeToTimeSpan_AllowsMultipleWhitespace()
        {
            Assert.AreEqual(65, JiraTimeHelpers.JiraTimeToTimeSpan("1h      5m").Value.TotalMinutes);
            Assert.AreEqual(125, JiraTimeHelpers.JiraTimeToTimeSpan("    2h   5m    ").Value.TotalMinutes);
        }
    }
}
