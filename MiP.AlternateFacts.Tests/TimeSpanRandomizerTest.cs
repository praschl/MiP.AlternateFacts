using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MiP.AlternateFacts.Tests
{
    [TestClass]
    public class TimeSpanRandomizerTest : RandomTest
    {
        private TimeSpanRandomizer _randomizer = new TimeSpanRandomizer(new Randomizer());

        [TestMethod]
        public void Days()
        {
            TimeSpan min = new TimeSpan();
            TimeSpan max = new TimeSpan(366, 0, 0, 0, 0);

            ReturnsChecked(() => _randomizer.Days(0, 365, true, true, true, true), ts => ts >= min && ts < max);
        }

        [TestMethod]
        public void TimeSpan()
        {
            TimeSpan min = System.TimeSpan.FromDays(1);
            TimeSpan max = System.TimeSpan.FromDays(3);

            ReturnsChecked(() => _randomizer.TimeSpan(min, max), ts => ts >= min && ts <= max);
        }
    }
}