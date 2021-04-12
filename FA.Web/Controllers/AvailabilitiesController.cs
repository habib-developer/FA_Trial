using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FA.Core.Domain;
using FA.Data;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using FA.Core.Services;
using FA.Web.Models;
using System.Security.Claims;
using Syncfusion.EJ2.Base;
using System.IO;
using Newtonsoft.Json;

namespace FA.Web.Controllers
{
    [Authorize]
    public class AvailabilitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAvailabilityService _availabilityService;
        private readonly IFreelancerService _freelancerService;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public AvailabilitiesController(ApplicationDbContext context,
            IAvailabilityService availabilityService,
            IFreelancerService freelancerService,
            IProjectService projectService,
            IMapper mapper)
        {
            _context = context;
            this._availabilityService = availabilityService;
            this._freelancerService = freelancerService;
            this._projectService = projectService;
            this._mapper = mapper;
        }

        // GET: Availabilities
        public async Task<IActionResult> Index(Guid freelancerId)
        {
            List<Freelancer> freelancers = await _freelancerService.GetAllAsync();
            if (freelancerId == default)
            {
                freelancerId = freelancers.FirstOrDefault().Id;
            }
            List<ProjectVM> projects = _mapper.Map<List<ProjectVM>>(await _projectService.GetAllAsync());
            List<Availability> availabilities = await _availabilityService.GetAvailabilitiesByFreelancerIdAsync(freelancerId);
            var availabilitiesVM = PrepareAvailabilityVM(availabilities);
            ViewBag.Freelancers = new SelectList(freelancers,"Id","Name",freelancerId);
            ViewBag.FreelancerId = freelancerId;
            ViewBag.Projects = projects;
            ViewBag.Availabilities = availabilitiesVM;
            //
            return View();
        }
        public async Task<IActionResult> List(Guid freelancerId,[FromBody] DataManagerRequest dm)
        {

            List<Freelancer> freelancers = await _freelancerService.GetAllAsync();
            List<Availability> availabilities = await _availabilityService.GetAvailabilitiesByFreelancerIdAsync(freelancerId);
            List<AvailabilityVM> availabilitiesVM = PrepareAvailabilityVM(availabilities);
            return Json(new { result = availabilitiesVM, count = availabilitiesVM.Count });
        }
        public async Task<IActionResult> Calender(Guid freelancerId)
        {
            List<Availability> availabilities = await _availabilityService.GetAvailabilitiesByFreelancerIdAsync(freelancerId);
            var availabilitiesVM = PrepareAvailabilityVM(availabilities);
            return Json(generateEvents(availabilitiesVM));
        }
        private List<EventsData> generateEvents(List<AvailabilityVM> availabilities)
        {
            List<EventsData> dateCollections = new List<EventsData>();
            foreach (var item in availabilities)
            {
                dateCollections.Add(new EventsData()
                {
                    Id = item.Id,
                    StartTime = item.StartDate,
                    EndTime = item.EndDate,
                    IsAllDay = true,
                    Subject = item.Type.ToString(),
                    CategoryColor = item.Type == AvailabilityType.Available ? "#28a745" : (item.Type==AvailabilityType.Booked? "#007bff" : "#dc3545 "),
                });
            }
            return dateCollections;
        }
        public async Task<IActionResult> Update()
        {
            string jsonData = await new StreamReader(Request.Body).ReadToEndAsync();
            var entity = _mapper.Map<Availability>(JsonConvert.DeserializeObject<InserUpdateRequest>(jsonData).value);
            var availability = await _availabilityService.GetAvailabilityById(entity.Id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            availability.HoursPerWeek = entity.HoursPerWeek;
            availability.ProjectId = entity.ProjectId;
            availability.Type = entity.Type;
            availability.StartDate = entity.StartDate;
            availability.EndDate = entity.EndDate;
            availability.UpdatedBy = userId;
            availability.UpdatedOn = DateTime.UtcNow;
            await _availabilityService.UpdateAsync(availability);
            var data = PrepareAvailabilityVM(await _availabilityService.GetAvailabilitiesByFreelancerIdAsync(availability.FreelancerId));
            return Json(new { result = data, count = data.Count });
        }
        [HttpPost]
        public async Task<IActionResult> Insert(Guid freelancerId)
        {
               
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string jsonData = await new StreamReader(Request.Body).ReadToEndAsync();
            var entity = _mapper.Map<Availability>(JsonConvert.DeserializeObject<InserUpdateRequest>(jsonData).value);
            entity.FreelancerId = freelancerId;
            entity.CreatedBy = entity.UpdatedBy = userId;
            entity.CreatedOn = entity.UpdatedOn = DateTime.UtcNow;
            await _availabilityService.AddAsync(entity);
            var data = PrepareAvailabilityVM(await _availabilityService.GetAvailabilitiesByFreelancerIdAsync(freelancerId));
            return Json(new { result = data, count = data.Count });
        }
        
        public async Task<IActionResult> Delete([FromBody]DeleteRequest e)
        {
            Availability entity= await _availabilityService.GetAvailabilityById(e.Key);
            var freelancerId = entity.FreelancerId;
            await _availabilityService.DeleteAsync(entity);
            var data = PrepareAvailabilityVM(await _availabilityService.GetAvailabilitiesByFreelancerIdAsync(freelancerId));
            return Json(new { result = data, count = data.Count });
        }
        private List<AvailabilityVM> PrepareAvailabilityVM(List<Availability> data)
        {
            List<AvailabilityVM> availabilitiesVM = data.Select(e => new AvailabilityVM()
            {
                Id = e.Id,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                HoursPerWeek = e.HoursPerWeek,
                HoursPerDay = (int)(e.HoursPerWeek / 5),
                CreatedOn = e.CreatedOn,
                UpdatedOn = e.UpdatedOn,
                Project = _mapper.Map<ProjectVM>(e.Project),
                Type = e.Type,
                TypeString = e.Type.ToString(),
                TotalHours = ((e.EndDate - e.StartDate).TotalDays * (e.HoursPerWeek / 5)),
                Freelancer = _mapper.Map<FreelancerVM>(e.Freelancer),
                FreelancerId = e.FreelancerId,
                ProjectId = e.ProjectId,
                UpdatedBy = e.UpdatedBy
            }).ToList();
            return availabilitiesVM;
        }
    }
    public class InserUpdateRequest
    {
        public string action { get; set; }
        public AvailabilityVM value { get; set; }
    }
    public class DeleteRequest
    {
        public string Action { get; set; }
        public Guid Key { get; set; }
        public string KeyColumn { get; set; }
    }
    
}
