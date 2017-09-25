using System;

namespace MiP.AlternateFacts
{
    public class TimeSpanRandomizer
    {
        private Randomizer _randomizer;

        public TimeSpanRandomizer(Randomizer randomizer)
        {
            _randomizer = randomizer;
        }

        public TimeSpan Days(int min = 0, int max = 365, bool hours = true, bool minutes = true, bool seconds = true, bool milliseconds = true)
        {
            return Randomize(
               min, max,
               0,
               hours ? 23 : 0,
               0,
               minutes ? 59 : 0,
               0,
               seconds ? 59 : 0,
               0,
               milliseconds ? 999 : 0);
        }


        public TimeSpan Hours(int min = 0, int max = 23, bool minutes = true, bool seconds = true, bool milliseconds = true)
        {
            return Randomize(0, 0,
                min, max,
                0,
                minutes ? 59 : 0,
                0,
                seconds ? 59 : 0,
                0,
                milliseconds ? 999 : 0);
        }

        public TimeSpan Minutes(int min = 0, int max = 59, bool seconds = true, bool milliseconds = true)
        {
            return Randomize(0, 0, 0, 0,
                min, max,
                0,
                seconds ? 59 : 0,
                0,
                milliseconds ? 999 : 0);
        }

        public TimeSpan Seconds(int min = 0, int max = 59, bool milliseconds = true)
        {
            return Randomize(0, 0, 0, 0, 0, 0,
                min, max,
                0,
                milliseconds ? 999 : 0);
        }

        public TimeSpan Milliseconds(int min = 0, int max = 999)
        {
            return Randomize(0, 0, 0, 0, 0, 0, 0, 0, min, 999);
        }

        public TimeSpan TimeSpan(TimeSpan min, TimeSpan max)
        {
            return new TimeSpan(_randomizer.Int64(min.Ticks, max.Ticks));
        }

        private TimeSpan Randomize(int minDays, int maxDays, int minHours, int maxHours, int minMinutes, int maxMinutes, int minSeconds, int maxSeconds, int minMillis, int maxMillis)
        {
            return new TimeSpan(
                _randomizer.Int32(minDays, maxDays),
                _randomizer.Int32(minHours, maxHours),
                _randomizer.Int32(minMinutes, maxMinutes),
                _randomizer.Int32(minSeconds, maxSeconds),
                _randomizer.Int32(minMillis, maxMillis)
                );
        }
    }
}