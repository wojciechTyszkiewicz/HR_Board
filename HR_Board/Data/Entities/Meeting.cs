using System.ComponentModel.DataAnnotations;

namespace HR_Board.Data.Entities
{
    public class Meeting : BaseEntity
    {
        public DateTime MeetingDate { get; set; }
        public MeetingType Type { get; set; }
        public int MyProperty { get; set; }
        public string MeetingAddress { get; set; }

        public string MeetingUrl { get; set; }
        public string Candidate { get; set; }
        public Guid JobId { get; set; }

    }


}



