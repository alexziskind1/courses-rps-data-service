using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPSDataService.DataObjects
{
    public class ConfTimeSlot
    {
        public string Title { get; set; }
        public bool IsBreak { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }


    }
}