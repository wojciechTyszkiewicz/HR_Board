using HR_Board.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HR_Board.Data.ModelDTO
{
    public class UpdateJobRequestDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        [Url]
        public string Logo { get; set; }

        public string CompanyName { get; set; }

        public JobStatus Status { get; set; }
        public Guid UserId { get; set; }
    }
}
