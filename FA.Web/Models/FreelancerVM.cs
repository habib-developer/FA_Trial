using System;
using System.Collections.Generic;

namespace FA.Web.Models
{
    public class FreelancerVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //public virtual ICollection<AvailabilityVM> Availabilities { get; set; }
    }
}
