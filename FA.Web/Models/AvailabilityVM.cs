using FA.Core.Domain;
using FA.Core.Infrastructure;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.Web.Models
{
    public class AvailabilityVM
    {
        public Guid Id { get; set; }
        public Guid FreelancerId { get; set; }
        public virtual FreelancerVM Freelancer { get; set; }
        public Guid? ProjectId { get; set; }
        public virtual ProjectVM Project { get; set; }
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
