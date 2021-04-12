using System;
using System.Collections.Generic;

namespace FA.Core.Domain
{
    public class Project:BaseEntity
    {
        public string Title { get; set; }
        public virtual ICollection<Availability> Availabilities { get; set; }
    }
}
