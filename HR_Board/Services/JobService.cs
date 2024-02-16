using HR_Board.Data;
using HR_Board.Data.Entities;
using HR_Board.Data.Enums;
using HR_Board.Data.ModelDTO;
using HR_Board.Mappers;
using HR_Board.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace HR_Board.Services
{
    public class JobService : IJobService
    {
        private readonly AppDbContext _context;

        public JobService(AppDbContext context, UserManager<ApiUser> userManager)
        {
            _context = context;
        }

        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            return await _context.Jobs.ToListAsync();

        }

        public async Task<Job> GetByIdAsync(Guid id)
        {
            var job = await _context.Jobs.FindAsync(id);
            return job;
        }

        public async Task<bool> CreateAsync(CreateJobCommand jobFromController)
        {

            var job = new Job
            {
                Title = jobFromController.Title,
                ShortDescription = jobFromController.ShortDescription,
                LongDescription = jobFromController.LongDescription,
                Logo = jobFromController.Logo,
                CompanyName = jobFromController.CompanyName,
                Status = Data.Enums.JobStatus.Open,
                CreatedBy = jobFromController.UserId
            };

            await _context.AddAsync(job);
            var r = await _context.SaveChangesAsync();
            return r == 1;

/*            return CreatedAtAction(nameof(GetById), new { id = job.Id }, job);*/
        }

        public async Task<bool> UpdateAsync(UpdateJobCommand jobFromController)
        {
            var updatedJob = await GetByIdAsync(jobFromController.JobId);

            if(updatedJob == null)
            {
                return false;
            }

            updatedJob.Title = jobFromController.Title;
            updatedJob.ShortDescription = jobFromController.ShortDescription;
            updatedJob.LongDescription = jobFromController.LongDescription;
            updatedJob.Logo = jobFromController.Logo;
            updatedJob.CompanyName = jobFromController.CompanyName;
            updatedJob.Status = jobFromController.Status;

            _context.Entry(updatedJob).State = EntityState.Modified;
            var r = await _context.SaveChangesAsync();

            return r ==1;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var job = await GetByIdAsync(id);
            if (job == null)
            {
                return false;
            }

            job.IsDeleted = true;

            _context.Entry(job).State = EntityState.Deleted;
            var r = await _context.SaveChangesAsync();

            return r == 1;
        }

        public async Task<bool> UpdateJobStatusAsync(Guid id, JobStatus state)
        {
            var job = await GetByIdAsync(id);
            if (job == null)
            {
                return false;
            }

            job.Status = state;
            _context.Entry(job).State = EntityState.Modified;
            var r = await _context.SaveChangesAsync();

            return r == 1;
        }

    }
}
