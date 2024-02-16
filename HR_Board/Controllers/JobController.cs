using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HR_Board.Data;
using HR_Board.Data.Entities;
using HR_Board.Data.Enums;
using HR_Board.Data.ModelDTO;
using HR_Board.Mappers;
using HR_Board.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMapper _mapper;

        public JobController(IJobService jobService, UserManager<ApiUser> userManager, IMapper mapper)
        {
            _jobService = jobService;
            _userManager = userManager;
            _mapper = mapper;

        }

        // GET: api/Job
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<JobResponseDto>>> GetAll()
        {
            var jobs = await _jobService.GetAllAsync();
            var jobsDto = _mapper.Map<IEnumerable<JobResponseDto>>(jobs);

            //dodać paginację wyników
            return Ok(jobsDto);
        }

        // GET: api/Job/5
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<JobResponseDto>> GetById(Guid id)
        {
            var job = await _jobService.GetByIdAsync(id);

            if (job == null)
            {
                return NotFound();
            }
            var jobDto = _mapper.Map<JobResponseDto>(job);

            return Ok(jobDto);
        }

        // POST: api/Job
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Create(CreateJobRequestDto jobDto)
        {
            var user = await _userManager.GetUserAsync(User);
 
            var createJobCommand = DtoJobConversion.From(jobDto) with { UserId = user.Id};

            var succes = await _jobService.CreateAsync(createJobCommand);

            return succes ? Created(): BadRequest(); 
        }


        // PUT: api/Job/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Update( Guid id, [FromBody] UpdateJobRequestDto jobDto)
        {
            var user = await _userManager.GetUserAsync(User);
            var updateJobCommand = DtoJobConversion.From(jobDto) with { UserId = user.Id, JobId = id };

            var success = await _jobService.UpdateAsync(updateJobCommand);


            return success ? Created(): BadRequest(); 
        }

        // DELETE: api/Job/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateJobStatus(Guid id, [FromBody] JobStatus state)
        {
            var success = await _jobService.UpdateJobStatusAsync(id, state);
            return success ? Created() : BadRequest();
        }
    }
}