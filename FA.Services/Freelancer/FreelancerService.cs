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
    public class FreelancerService:IFreelancerService
    {
        private readonly IAsyncRepository<Freelancer> _freelancerRepository;

        public FreelancerService(IAsyncRepository<Freelancer> freelancerRepository)
        {
            this._freelancerRepository = freelancerRepository;
        }
        public async Task<List<Freelancer>> GetAllAsync()
        {
            return await _freelancerRepository.Table().ToListAsync();
        }

    }

}
