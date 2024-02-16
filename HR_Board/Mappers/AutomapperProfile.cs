using AutoMapper;
using HR_Board.Data.Entities;
using HR_Board.Data.ModelDTO;

namespace HR_Board.Mappers
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Job, JobResponseDto>();
        }
    }
}
