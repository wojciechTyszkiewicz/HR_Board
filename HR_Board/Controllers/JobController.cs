using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Board.Data;
using HR_Board.Data.Entities;
using HR_Board.Data.Enums;
using HR_Board.Data.ModelDTO;
using HR_Board.Mappers;
using HR_Board.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly UserManager<ApiUser> _userManager;

        public JobController(IJobService jobService, UserManager<ApiUser> userManager)
        {
            _jobService = jobService;
            _userManager = userManager;

        }

        // GET: api/Job
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetAll()
        {
            var jobs = await _jobService.GetAllAsync();
            //dodać paginację wyników
            return Ok(jobs);
        }

        // GET: api/Job/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetById(Guid id)
        {
            var job = await _jobService.GetByIdAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return Ok(job);
        }

        // POST: api/Job
        [HttpPost]
        public async Task<IActionResult> Create(CreateJobRequestDto jobDto)
        {
            var user = await _userManager.GetUserAsync(User);
 
            var createJobCommand = DtoJobConversion.From(jobDto) with { UserId = user.Id};

            var succes = await _jobService.CreateAsync(createJobCommand);

            return succes ? Created(): BadRequest(); 
        }


        // PUT: api/Job/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateJobRequestDto jobDto)
        {
            var user = await _userManager.GetUserAsync(User);
            var updateJobCommand = DtoJobConversion.From(jobDto) with { UserId = user.Id, JobId = id };

            var success = await _jobService.UpdateAsync(updateJobCommand);


            return success ? Created(): BadRequest(); 
        }

        // DELETE: api/Job/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _jobService.DeleteAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // PUT: api/Job/5
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateJobStatus(Guid id, JobStatus state)
        {
            var success = await _jobService.UpdateJobStatusAsync(id, state);
            return success ? Created(): BadRequest();
        }
    }
}