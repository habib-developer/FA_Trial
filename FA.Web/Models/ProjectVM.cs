using FA.Web.Models;
using System;
using System.Collections.Generic;

namespace FA.Web.Models
{
    public class ProjectVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        //public virtual IList<AvailabilityVM> Availabilities { get; set; }
    }
}
