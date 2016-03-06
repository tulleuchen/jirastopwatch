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
            Assert.AreEqual(0, JiraTimeHelpers.JiraTimeToTimeSpan("m").TotalMilliseconds);
            Assert.AreEqual(0, JiraTimeHelpers.JiraTimeToTimeSpan("2 m").TotalMilliseconds);
        }

        [Test]
        public void JiraTimeToTimeSpan_InvalidHoursFails()
        {
            Assert.AreEqual(0, JiraTimeHelpers.JiraTimeToTimeSpan("h").TotalMilliseconds);
            Assert.AreEqual(0, JiraTimeHelpers.JiraTimeToTimeSpan("8 h").TotalMilliseconds);
        }

        [Test]
        public void JiraTimeToTimeSpan_ValidHoursWithInvalidMinutesFails()
        {
            Assert.AreEqual(0, JiraTimeHelpers.JiraTimeToTimeSpan("2h 5").TotalMilliseconds);
            Assert.AreEqual(0, JiraTimeHelpers.JiraTimeToTimeSpan("2h m").TotalMilliseconds);
        }

        [Test]
        public void JiraTimeToTimeSpan_InvalidHoursWithValidMinutesFails()
        {
            Assert.AreEqual(0, JiraTimeHelpers.JiraTimeToTimeSpan("2 5m").TotalMilliseconds);
            Assert.AreEqual(0, JiraTimeHelpers.JiraTimeToTimeSpan("h 5m").TotalMilliseconds);
        }

        [Test]
        public void JiraTimeToTimeSpan_ParsesJiraStyleTimespan()
        {
            Assert.AreEqual(120, JiraTimeHelpers.JiraTimeToTimeSpan("2h").TotalMinutes);
            Assert.AreEqual(125, JiraTimeHelpers.JiraTimeToTimeSpan("2h 5m").TotalMinutes);
            Assert.AreEqual(5, JiraTimeHelpers.JiraTimeToTimeSpan("5m").TotalMinutes);
        }

        [Test]
        public void JiraTimeToTimeSpan_ParsesDecimalHours()
        {
            Assert.AreEqual(150, JiraTimeHelpers.JiraTimeToTimeSpan("2.5h").TotalMinutes);
        }

        [Test]
        public void JiraTimeToTimeSpan_IgnoresDecimalValueForMinutes()
        {
            Assert.AreEqual(600, JiraTimeHelpers.JiraTimeToTimeSpan("10.5m").TotalSeconds);
        }

        [Test]
        public void JiraTimeToTimeSpan_AllowsMinutesBeforeHours()
        {
            Assert.AreEqual(125, JiraTimeHelpers.JiraTimeToTimeSpan("5m 2h").TotalMinutes);
        }

        [Test]
        public void JiraTimeToTimeSpan_AllowsSillyValues()
        {
            Assert.AreEqual(120, JiraTimeHelpers.JiraTimeToTimeSpan("2h 0m").TotalMinutes);
            Assert.AreEqual(5, JiraTimeHelpers.JiraTimeToTimeSpan("0h 5m").TotalMinutes);
        }

        [Test]
        public void JiraTimeToTimeSpan_AllowsMultipleWhitespace()
        {
            Assert.AreEqual(65, JiraTimeHelpers.JiraTimeToTimeSpan("1h      5m").TotalMinutes);
            Assert.AreEqual(125, JiraTimeHelpers.JiraTimeToTimeSpan("    2h   5m    ").TotalMinutes);
        }
    }
}
