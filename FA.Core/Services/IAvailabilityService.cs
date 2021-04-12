using FA.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.Core.Services
{
    public interface IAvailabilityService
    {
        Task<List<Availability>> GetAvailabilitiesByFreelancerIdAsync(Guid freelancerId);
        Task UpdateAsync(Availability value);
        Task AddAsync(Availability value);
        Task DeleteAsync(Availability value);
        Task<Availability> GetAvailabilityById(Guid key);
    }
}
