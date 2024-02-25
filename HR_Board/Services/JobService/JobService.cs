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

namespace HR_Board.Services.JobService
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

        public async Task<Guid> CreateAsync(CreateJobCommand jobFromController)
        {

            var job = new Job
            {
                Title = jobFromController.Title,
                ShortDescription = jobFromController.ShortDescription,
                LongDescription = jobFromController.LongDescription,
                Logo = jobFromController.Logo,
                CompanyName = jobFromController.CompanyName,
                Status = JobStatus.Open,
                CreatedBy = jobFromController.UserId
            };

            await _context.Jobs.AddAsync(job);
            var r = await _context.SaveChangesAsync();
            return job.Id;
        }

        public async Task<OperationResponse> UpdateAsync(UpdateJobCommand jobFromController)
        {

            if (!await _context.Jobs.Where(job => job.Id == jobFromController.JobId).AnyAsync())
            {
                return new OperationResponse(false, OperationResponseStatus.NotFound);
            }
            if (!await _context.Jobs.Where(job => job.Id == jobFromController.JobId && job.CreatedBy == jobFromController.UserId).AnyAsync())
            {
                return new OperationResponse(false, OperationResponseStatus.Forbiden);
            }
            var jobToUpdate = await GetByIdAsync(jobFromController.JobId);

            jobToUpdate.Title = jobFromController.Title;
            jobToUpdate.ShortDescription = jobFromController.ShortDescription;
            jobToUpdate.LongDescription = jobFromController.LongDescription;
            jobToUpdate.Logo = jobFromController.Logo;
            jobToUpdate.CompanyName = jobFromController.CompanyName;
            jobToUpdate.Status = jobFromController.Status;

            var r = await _context.SaveChangesAsync();
            return new OperationResponse(r == 1, OperationResponseStatus.Success);
        }

        public async Task<OperationResponse> DeleteAsync(Guid id, Guid userId)
        {
            if (!await _context.Jobs.Where(job => job.Id == id).AnyAsync())
            {
                return new OperationResponse(false, OperationResponseStatus.NotFound);
            }
            if (!await _context.Jobs.Where(job => job.Id == id && job.CreatedBy == userId).AnyAsync())
            {
                return new OperationResponse(false, OperationResponseStatus.Forbiden);
            }

            /*jobToDelete.IsDeleted = true;*/
            _context.Jobs.Remove(new Job { Id = id });
            var r = await _context.SaveChangesAsync();

            return new OperationResponse(r == 1, OperationResponseStatus.Success);
        }

        public async Task<OperationResponse> UpdateJobStatusAsync(Guid id, Guid userId, JobStatus state)
        {
            if (!await _context.Jobs.Where(job => job.Id == id).AnyAsync())
            {
                return new OperationResponse(false, OperationResponseStatus.NotFound);
            }
            if (!await _context.Jobs.Where(job => job.Id == id && job.CreatedBy == userId).AnyAsync())
            {
                return new OperationResponse(false, OperationResponseStatus.Forbiden);
            }

            var jobToUpdateStatus = await GetByIdAsync(id);
            jobToUpdateStatus.Status = state;
/*            _context.Entry(jobToUpdateStatus).State = EntityState.Modified;*/
            var r = await _context.SaveChangesAsync();

            return new OperationResponse(r == 1, OperationResponseStatus.Success);
        }


        //Todo: move to authentication class service ?

        public bool HasAuthotization(Operation operation, Job job, Guid userId)
        {
            if (job.CreatedBy == userId)
            {
                return true;
            }
            return false;
        }
    }
}
