using FA.Core.Infrastructure;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.Core.Domain
{
    public class Availability:BaseEntity,ITrackable
    {
        public Guid FreelancerId { get; set; }
        public virtual Freelancer Freelancer { get; set; }
        public Guid? ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AvailabilityType Type { get; set; }
        public int HoursPerWeek { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
