using FA.Core.Domain;
using FA.Core.Infrastructure;
using FA.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.Services
{
    public class ProjectService:IProjectService
    {
        private readonly IAsyncRepository<Project> _projectRepository;

        public ProjectService(IAsyncRepository<Project> projectRepository)
        {
            this._projectRepository = projectRepository;
        }
        public async Task<List<Project>> GetAllAsync()
        {
            return (await _projectRepository.ListAllAsync()).ToList();
        }
    }

}
