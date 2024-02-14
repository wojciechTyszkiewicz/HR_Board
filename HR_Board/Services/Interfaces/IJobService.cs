using HR_Board.Data;
using HR_Board.Data.Entities;
using HR_Board.Data.Enums;
using HR_Board.Data.ModelDTO;
using Microsoft.AspNetCore.Mvc;

namespace HR_Board.Services.Interfaces
{
    public interface IJobService
    {
        public Task<IEnumerable<Job>> GetAllAsync();

        public Task<Job> GetByIdAsync(Guid id);

        public Task<bool> CreateAsync(CreateJobCommand jobFromController);

        public Task<bool> UpdateAsync(UpdateJobCommand jobFromController);

        public Task<bool> DeleteAsync(Guid id);

        public Task<bool> UpdateJobStatusAsync(Guid id, JobStatus state);
    };
}
