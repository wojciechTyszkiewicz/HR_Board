using HR_Board.Data.ModelDTO;

namespace HR_Board.Mappers
{
    public static class DtoJobConversion
    {
        public static CreateJobRequestWithUserId From(CreateJobRequestDto createJobRequestDto, Guid userId)
        {
            return new CreateJobRequestWithUserId
            {
                Title = createJobRequestDto.Title,
                ShortDescription = createJobRequestDto.ShortDescription,
                LongDescription = createJobRequestDto.LongDescription,
                Logo = createJobRequestDto.Logo,
                CompanyName = createJobRequestDto.CompanyName,
                Status = createJobRequestDto.Status,
                UserId = userId
            };
        }

        public static UpdateJobRequestDtoWithJobIdAndUserId From(UpdateJobRequestDto updateJobRequestDto)
        {
            return new UpdateJobRequestDtoWithJobIdAndUserId
            {
                JobId = updateJobRequestDto.Id,
                Title = updateJobRequestDto.Title,
                ShortDescription = updateJobRequestDto.ShortDescription,
                LongDescription = updateJobRequestDto.LongDescription,
                Logo = updateJobRequestDto.Logo,
                CompanyName = updateJobRequestDto.CompanyName,
                Status = updateJobRequestDto.Status,
                UserId = updateJobRequestDto.UserId
            };
        }
    }
}
