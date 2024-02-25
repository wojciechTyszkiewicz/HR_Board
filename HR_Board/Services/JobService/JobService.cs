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


            await _context.AddAsync(job);
            var r = await _context.SaveChangesAsync();
            return job.Id;

            /*            return CreatedAtAction(nameof(GetById), new { id = job.Id }, job);*/
        }

        public async Task<OperationResponse> UpdateAsync(UpdateJobCommand jobFromController)
        {
            var jobToUpdate = await GetByIdAsync(jobFromController.JobId);

            if (jobToUpdate == null)
            {
                return new OperationResponse(false, OperationResponseStatus.NotFound);
            }
            if (!HasAuthotization(Operation.Update, jobToUpdate, jobFromController.UserId))
            {
                return new OperationResponse(false, OperationResponseStatus.Forbiden);
            }


            jobToUpdate.Title = jobFromController.Title;
            jobToUpdate.ShortDescription = jobFromController.ShortDescription;
            jobToUpdate.LongDescription = jobFromController.LongDescription;
            jobToUpdate.Logo = jobFromController.Logo;
            jobToUpdate.CompanyName = jobFromController.CompanyName;
            jobToUpdate.Status = jobFromController.Status;

            _context.Entry(jobToUpdate).State = EntityState.Modified;
            var r = await _context.SaveChangesAsync();

            return new OperationResponse(r == 1, OperationResponseStatus.Success);

        }

        public async Task<OperationResponse> DeleteAsync(Guid id, Guid userId)
        {
            var jobToDelete = await GetByIdAsync(id);
            if (jobToDelete == null)
            {
                return new OperationResponse(false, OperationResponseStatus.NotFound);
            }
            if (!HasAuthotization(Operation.Delete, jobToDelete, userId))
            {
                return new OperationResponse(false, OperationResponseStatus.Forbiden);
            }

            jobToDelete.IsDeleted = true;

            _context.Entry(jobToDelete).State = EntityState.Deleted;
            var r = await _context.SaveChangesAsync();

            return new OperationResponse(r == 1, OperationResponseStatus.Success);
        }

        public async Task<OperationResponse> UpdateJobStatusAsync(Guid id, Guid userId, JobStatus state)
        {
            var jobToUpdateStatus = await GetByIdAsync(id);

            if (jobToUpdateStatus == null)
            {
                return new OperationResponse(false, OperationResponseStatus.NotFound);
            }
            if (!HasAuthotization(Operation.Delete, jobToUpdateStatus, userId))
            {
                return new OperationResponse(false, OperationResponseStatus.Forbiden);
            }

            jobToUpdateStatus.Status = state;
            _context.Entry(jobToUpdateStatus).State = EntityState.Modified;
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
