namespace StopWatchTest
{
    using NUnit.Framework;
    using StopWatch;

    [TestFixture]
    public class JiraHelpersTest
    {
        [Test]
        public void JiraTimeToTimeSpan_InvalidMinutesFails()
        {
            Assert.AreEqual(0, JiraHelpers.JiraTimeToTimeSpan("m").TotalMilliseconds);
            Assert.AreEqual(0, JiraHelpers.JiraTimeToTimeSpan("2 m").TotalMilliseconds);
        }

        [Test]
        public void JiraTimeToTimeSpan_InvalidHoursFails()
        {
            Assert.AreEqual(0, JiraHelpers.JiraTimeToTimeSpan("h").TotalMilliseconds);
            Assert.AreEqual(0, JiraHelpers.JiraTimeToTimeSpan("8 h").TotalMilliseconds);
        }

        [Test]
        public void JiraTimeToTimeSpan_ValidHoursWithInvalidMinutesFails()
        {
            Assert.AreEqual(0, JiraHelpers.JiraTimeToTimeSpan("2h 5").TotalMilliseconds);
            Assert.AreEqual(0, JiraHelpers.JiraTimeToTimeSpan("2h m").TotalMilliseconds);
        }

        [Test]
        public void JiraTimeToTimeSpan_InvalidHoursWithValidMinutesFails()
        {
            Assert.AreEqual(0, JiraHelpers.JiraTimeToTimeSpan("2 5m").TotalMilliseconds);
            Assert.AreEqual(0, JiraHelpers.JiraTimeToTimeSpan("h 5m").TotalMilliseconds);
        }

        [Test]
        public void JiraTimeToTimeSpan_ParsesJiraStyleTimespan()
        {
            Assert.AreEqual(120, JiraHelpers.JiraTimeToTimeSpan("2h").TotalMinutes);
            Assert.AreEqual(125, JiraHelpers.JiraTimeToTimeSpan("2h 5m").TotalMinutes);
            Assert.AreEqual(5, JiraHelpers.JiraTimeToTimeSpan("5m").TotalMinutes);
        }

        [Test]
        public void JiraTimeToTimeSpan_ParsesDecimalHours()
        {
            Assert.AreEqual(150, JiraHelpers.JiraTimeToTimeSpan("2.5h").TotalMinutes);
        }

        [Test]
        public void JiraTimeToTimeSpan_IgnoresDecimalValueForMinutes()
        {
            Assert.AreEqual(600, JiraHelpers.JiraTimeToTimeSpan("10.5m").TotalSeconds);
        }

        [Test]
        public void JiraTimeToTimeSpan_AllowsMinutesBeforeHours()
        {
            Assert.AreEqual(125, JiraHelpers.JiraTimeToTimeSpan("5m 2h").TotalMinutes);
        }

        [Test]
        public void JiraTimeToTimeSpan_AllowsSillyValues()
        {
            Assert.AreEqual(120, JiraHelpers.JiraTimeToTimeSpan("2h 0m").TotalMinutes);
            Assert.AreEqual(5, JiraHelpers.JiraTimeToTimeSpan("0h 5m").TotalMinutes);
        }

        [Test]
        public void JiraTimeToTimeSpan_AllowsMultipleWhitespace()
        {
            Assert.AreEqual(65, JiraHelpers.JiraTimeToTimeSpan("1h      5m").TotalMinutes);
            Assert.AreEqual(125, JiraHelpers.JiraTimeToTimeSpan("    2h   5m    ").TotalMinutes);
        }
    }
}
