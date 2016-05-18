using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPSDataService.DataObjects
{
    public class RoomInfo
    {
        public string Id { get; set; }
        public string RoomId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Theme { get; set; }
        public string ExtraProp4 { get; set; }
    }
}