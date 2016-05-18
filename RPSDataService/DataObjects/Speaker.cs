using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPSDataService.DataObjects
{
    public class Speaker
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Picture { get; set; }
        public string TwitterName { get; set; }

        public virtual ICollection<Session> SessionsTeaching { get; set; }


        public Speaker()
        {
            SessionsTeaching = new HashSet<Session>();

        }
    }
}