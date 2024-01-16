using HR_Board.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HR_Board.Data.Entities
{
    public class Meeting : BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime MeetingDate { get; set; }
        public MeetingType MeetingType { get; set; } = MeetingType.online;
        public string MeetingAddress { get; set; }
        public string MeetingUrl { get; set; }
        public ApiUser Candidate { get; set; }
        public Guid CandidateId { get; set; }
        public Job Job { get; set; }
        public Guid JobId { get; set; }

    }


}



