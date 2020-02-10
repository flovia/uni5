using System;
using System.Collections.Generic;
using System.Text;

namespace adapter
{
    interface ITimer
    {
        public void Set(string time);
        string Get();
    }

    class Person
    {
        public string GetTime(ITimer timerObj) => timerObj.Get();
        public void SetTime(ITimer timerObj, string time) => timerObj.Set(time);
    }

    class Clock
    {
        private uint hours, minutes;
        private bool cappedFlag = false;
        private void capValues()
        {
            if (cappedFlag) return;
            hours--;
            hours += minutes / 60;
            minutes %= 60;
            hours %= 12;
            hours++;
            cappedFlag = true;
        }
        public void SetHours(uint val)
        {
            hours = val;
            cappedFlag = false;
        }
        public void SetMinutes(uint val)
        {
            minutes = val;
            cappedFlag = false;
        }
        public void SetTime(uint hours, uint minutes)
        {
            SetHours(hours);
            SetMinutes(minutes);
        }
        public uint GetHours()
        {
            capValues();
            return hours;
        }
        public uint GetMinutes()
        {
            capValues();
            return minutes;
        }

        public Clock(uint hours, uint minutes) => SetTime(hours, minutes);
        public Clock() => SetTime(12, 00);
    }

    class ClockToTimerAdapter : ITimer
    {
        private Clock clock;
        public void Set(string time)
        {
            string[] words = time.Split(':');
            if (words.Length != 2) throw new ArgumentException(message: $"Parameter string invalid, got \"{time}\"");
            clock.SetTime(uint.Parse(words[0]), uint.Parse(words[1]));
        }
        public string Get()
        {
            return String.Format("{0:D2}:{1:D2}", clock.GetHours(), clock.GetMinutes()); // todo: check for correct bounding
        }

        public ClockToTimerAdapter(Clock c) => clock = c;
    }
}
