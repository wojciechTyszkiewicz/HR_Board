using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using HR_Board.Controllers;
using HR_Board.Data;
using HR_Board.Data.Entities;
using HR_Board.Data.Enums;
using HR_Board.Data.ModelDTO;
using HR_Board.Mappers;
using HR_Board.Services.Interfaces;
using HR_Board.Services.JobService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

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

            var createJobCommand = DtoJobConversion.From(jobDto) with { UserId = user.Id };

            Guid createdJobId = await _jobService.CreateAsync(createJobCommand);

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
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateJobRequestDto jobDto)
        {
            var user = await _userManager.GetUserAsync(User);
            var updateJobCommand = DtoJobConversion.From(jobDto) with { UserId = user.Id, JobId = id };

            var success = await _jobService.UpdateAsync(updateJobCommand);

            if(!success.Success)
            {
                switch(success.ResponseStatus)
                {
                    case OperationResponseStatus.NotFound:
                        return NotFound();
                    case OperationResponseStatus.Forbiden:
                        return Forbid();
                }
            }
            return Ok();
        }

        // DELETE: api/Job/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);

            var success = await _jobService.DeleteAsync(id, user.Id);

            if (!success.Success)
            {
                switch (success.ResponseStatus)
                {
                    case OperationResponseStatus.NotFound:
                        return NotFound();
                    case OperationResponseStatus.Forbiden:
                        return Forbid();
                }
            }
            return Ok();
        }

        // PUT: api/Job/5
        [HttpPatch("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateJobStatus(Guid id, [FromBody] JobStatus state)
        {
            var user = await _userManager.GetUserAsync(User);

            var success = await _jobService.UpdateJobStatusAsync(id, user.Id, state);

            if (!success.Success)
            {
                switch (success.ResponseStatus)
                {
                    case OperationResponseStatus.NotFound:
                        return NotFound();
                    case OperationResponseStatus.Forbiden:
                        return Forbid();
                }
            }
            return Ok();
        }


    }
}