using HR_Board.Data.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HR_Board.Data.Entities
{
    public class ApiUser : IdentityUser<Guid>, IBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public List<Meeting> Meetings { get; set; }
    }
}
