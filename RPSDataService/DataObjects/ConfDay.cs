using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPSDataService.DataObjects
{
    public class ConfDay
    {
        public const int SESSION_LENGTH = 60;

        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public List<ConfTimeSlot> TimeSlots { get; set; }

        public ConfDay(string title, DateTime start, DateTime end)
        {
            this.Title = title;
            this.Start = start;
            this.End = end;
            this.TimeSlots = new List<ConfTimeSlot>();

            var sessionStartTimes = GetTimeRange(Start, End, SESSION_LENGTH);
            foreach (var sessionStartTime in sessionStartTimes)
            {
                var isBreak = false;
                var sessionTitle = string.Empty;
                if (sessionStartTime.Hour == 8)
                {
                    isBreak = true;
                    sessionTitle = "Registration";
                }
                else if (sessionStartTime.Hour == 12)
                {
                    isBreak = true;
                    sessionTitle = "Lunch";
                }

                var cTimeSlot = new ConfTimeSlot { Start = sessionStartTime, End = sessionStartTime.AddMinutes(SESSION_LENGTH), Title = sessionTitle, IsBreak = isBreak };
                TimeSlots.Add(cTimeSlot);
            }

        }

        private IEnumerable<DateTime> GetTimeRange(DateTime startTime, DateTime endTime, int minutesBetween)
        {
            int periods = (int)(endTime - startTime).TotalMinutes / minutesBetween;
            return Enumerable.Range(0, periods + 1)
                             .Select(p => startTime.AddMinutes(minutesBetween * p));
        }
    }
}