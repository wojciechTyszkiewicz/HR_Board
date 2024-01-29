using HR_Board.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HR_Board.Data.Entities
{
    public class Job : BaseEntity
    {
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(15, MinimumLength = 5)]
        public string ShortDescription { get; set; }

        [StringLength(250, MinimumLength = 5)]
        public string LongDescription { get; set; }

        [Url]
        public string Logo { get; set; }

        [StringLength(50, MinimumLength = 5)]
        public string CompanyName { get; set; }

        public JobStatus Status { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
