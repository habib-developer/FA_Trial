using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FA.Web.Models
{
    public class EventsData
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsAllDay { get; set; }
        public string CategoryColor { get; set; }
        public int TaskId { get; set; }
    }
}
