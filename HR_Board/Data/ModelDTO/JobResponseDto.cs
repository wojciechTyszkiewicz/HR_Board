using HR_Board.Data.Enums;

namespace HR_Board.Data.ModelDTO
{
    public class JobResponseDto
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public string Logo { get; set; }

        public string CompanyName { get; set; }

        public JobStatus Status { get; set; }
    }
}

