using HR_Board.Data.ModelDTO;

namespace HR_Board.Mappers
{
    public static class DtoJobConversion
    {
        public static CreateJobCommand From(CreateJobRequestDto createJobRequestDto)
        {
            return new CreateJobCommand
            {
                Title = createJobRequestDto.Title,
                ShortDescription = createJobRequestDto.ShortDescription,
                LongDescription = createJobRequestDto.LongDescription,
                Logo = createJobRequestDto.Logo,
                CompanyName = createJobRequestDto.CompanyName,
                Status = createJobRequestDto.Status
            };
        }

        public static UpdateJobCommand From(UpdateJobRequestDto updateJobRequestDto)
        {
            return new UpdateJobCommand
            {
                Title = updateJobRequestDto.Title,
                ShortDescription = updateJobRequestDto.ShortDescription,
                LongDescription = updateJobRequestDto.LongDescription,
                Logo = updateJobRequestDto.Logo,
                CompanyName = updateJobRequestDto.CompanyName,
                Status = updateJobRequestDto.Status
            };
        }
    }
}
