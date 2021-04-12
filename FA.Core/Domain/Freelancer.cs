using System;
using System.Collections.Generic;

namespace FA.Core.Domain
{
    public class Freelancer:BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Availability> Availabilities { get; set; }
    }
}
