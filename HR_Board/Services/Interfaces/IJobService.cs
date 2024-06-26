﻿using HR_Board.Data;
using HR_Board.Data.Entities;
using HR_Board.Data.Enums;
using HR_Board.Data.ModelDTO;
using HR_Board.Services.JobService;
using Microsoft.AspNetCore.Mvc;

namespace HR_Board.Services.Interfaces
{
    public interface IJobService
    {
        public Task<IEnumerable<Job>> GetAllAsync();

        public Task<Job> GetByIdAsync(Guid id);

        public Task<Guid> CreateAsync(CreateJobRequestWithUserId jobFromController);

        public Task<OperationResponse> UpdateAsync(UpdateJobRequestDtoWithJobIdAndUserId jobFromController);

        public Task<OperationResponse> DeleteAsync(Guid id, Guid userId);

        public Task<OperationResponse> UpdateJobStatusAsync(Guid id, Guid userId, JobStatus state);

        Task<IEnumerable<Job>> SearchJobsAsync(string title, string description);

        bool HasAuthotization(Operation operation, Job job, Guid userId);
    };

    public enum Operation
    {
        Create,
        Delete,
        Update
    }
}
