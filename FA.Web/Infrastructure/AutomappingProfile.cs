using AutoMapper;
using FA.Core.Domain;
using FA.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FA.Web.Infrastructure
{
    public class AutomappingProfile:Profile
    {
        public AutomappingProfile()
        {
            CreateMap<Freelancer, FreelancerVM>();
            CreateMap<ProjectVM, ProjectVM>();
            CreateMap<Availability, AvailabilityVM>();
        }
    }
}
