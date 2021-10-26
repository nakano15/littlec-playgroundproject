using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Applications.AgendaAppClasses
{
    public class Event
    {
        public string EventName = "";
        public string Description = "";
        public EventTimeStruct? StartTime = null,
            EndTime = null;
        public EventDayStruct? StartDay = null, EndDay = null;
        public bool[] DaysOfWeekEventHappens = new bool[7] { true, true, true, true, true, true, true };

        public struct EventTimeStruct
        {
            public byte Hour, Minute;

            public EventTimeStruct(byte Hour, byte Minute)
            {
                this.Hour = Hour;
                this.Minute = Minute;
            }
        }

        public struct EventDayStruct
        {
            public byte Day, Month, Year;

            public EventDayStruct(byte Day, byte Month, byte Year)
            {
                this.Day = Day;
                this.Month = Month;
                this.Year = Year;
            }
        }
    }
}
