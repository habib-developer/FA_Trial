using FA.Core.Domain;
using FA.Core.Infrastructure;
using FA.Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IAsyncRepository<Availability> _availabilityRepo;

        public AvailabilityService(IAsyncRepository<Availability> availabilityRepo)
        {
            this._availabilityRepo = availabilityRepo;
        }
        public async Task<List<Availability>> GetAvailabilitiesByFreelancerIdAsync(Guid freelancerId)
        {
            return await _availabilityRepo.Table().Include(a => a.Freelancer).Include(a => a.Project).Where(e=>e.FreelancerId==freelancerId).ToListAsync();
        }
        public async Task UpdateAsync(Availability value)
        {
            await _availabilityRepo.UpdateAsync(value);
        }
        public async Task AddAsync(Availability value)
        {
            await _availabilityRepo.AddAsync(value);
        }
        public async Task DeleteAsync(Availability value)
        {
            await _availabilityRepo.DeleteAsync(value);
        }
        public async Task<Availability> GetAvailabilityById(Guid key)
        {
            return await _availabilityRepo.GetByIdAsync(key);
        }
    }

}
