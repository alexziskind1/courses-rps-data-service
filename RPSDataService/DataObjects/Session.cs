using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPSDataService.DataObjects
{
    public class Session
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DescriptionShort { get; set; }
        public string Room { get; set; }
        public string RoomInfoId { get; set; }
        public string CalendarEventId { get; set; }
        public bool IsBreak { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public virtual RoomInfo RoomInfo { get; set; }
        public virtual ICollection<Speaker> Speakers { get; set; }

        public Session()
        {
            Speakers = new HashSet<Speaker>();
        }
    }
}