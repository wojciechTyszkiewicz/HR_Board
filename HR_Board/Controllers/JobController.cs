using AutoMapper;
using HR_Board.Controllers;
using HR_Board.Data;
using HR_Board.Data.Enums;
using HR_Board.Data.ModelDTO;
using HR_Board.Mappers;
using HR_Board.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class JobController : HrController
    {
        private readonly IJobService _jobService;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IMapper _mapper;

        public JobController(IJobService jobService, UserManager<ApiUser> userManager, IMapper mapper, LinkGenerator linkGenerator) : base("Job", linkGenerator)
        {
            _jobService = jobService;
            _userManager = userManager;
            _mapper = mapper;

        }

        // GET: api/Job
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<JobResponseDto>>> GetAll()
        {
            var jobs = await _jobService.GetAllAsync();
            if (jobs == null)
            {
                return NotFound();
            }
            var jobsDto = _mapper.Map<IEnumerable<JobResponseDto>>(jobs);

            //dodać paginację wyników
            return Ok(jobsDto);
        }

        // GET: api/Job/5
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IResult> Create(CreateJobRequestDto jobDto, LinkGenerator linkGenerator)
        {

            var user = await _userManager.GetUserAsync(User);

            var createJobRequestWithUserId = DtoJobConversion.From(jobDto, user.Id) ;

            Guid createdJobId = await _jobService.CreateAsync(createJobRequestWithUserId);

            if (createdJobId == null)
            {
                return Results.BadRequest();
            }

            return CreatedAtGet(nameof(GetById), createdJobId);
            /*            return CreatedAtAction(nameof(Create), new { Id = createdJobId }, createdJobId);*/
            // w przypadku zmiany GetById na inną ścieżkę nie generował poprawnego URL - dlaczego?
        }


        // PUT: api/Job/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IResult> Update(Guid id, [FromBody] UpdateJobRequestDto jobRequestDto)
        {
            var user = await _userManager.GetUserAsync(User);
            var updateJobRequestWithJobIdAndUserId = DtoJobConversion.From(jobRequestDto) with { UserId = user.Id, JobId = id };

            var response = await _jobService.UpdateAsync(updateJobRequestWithJobIdAndUserId);

            return ReturnStatusCode(response);
        }

        // DELETE: api/Job/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IResult> Delete(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);

            var response = await _jobService.DeleteAsync(id, user.Id);

            return ReturnStatusCode(response);
        }

        // PUT: api/Job/5
        [HttpPatch("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IResult> UpdateJobStatus(Guid id, [FromBody] JobStatus state)
        {
            var user = await _userManager.GetUserAsync(User);

            var response = await _jobService.UpdateJobStatusAsync(id, user.Id, state);

            return ReturnStatusCode(response);
        }


    }
}