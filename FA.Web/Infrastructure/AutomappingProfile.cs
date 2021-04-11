using AutoMapper;
using AutoMapper.EquivalencyExpression;
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
            CreateMap<FreelancerVM, Freelancer>();
            CreateMap<Project, ProjectVM>();
            CreateMap<ProjectVM, Project>();
            CreateMap<Availability, AvailabilityVM>().EqualityComparison((odto,o)=> odto.Id==o.Id);
            CreateMap<AvailabilityVM, Availability>().EqualityComparison((odto,o)=> odto.Id==o.Id);
        }
    }
}
