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
        public DateTime? StartDay = null, EndDay = null;
        public bool[] DaysOfWeekEventHappens = new bool[7] { true, true, true, true, true, true, true };

        public bool IsEventHappening(DateTime Now)
        {
            if (DaysOfWeekEventHappens[(int)Now.DayOfWeek])
            {
                if (!StartDay.HasValue)
                    return true;
                if(Now >= StartDay.Value)
                {
                    if (!EndDay.HasValue) return true;
                    if (Now < EndDay.Value) return true;
                }
            }
            return false;
        }

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
