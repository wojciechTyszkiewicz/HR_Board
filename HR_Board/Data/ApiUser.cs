using HR_Board.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HR_Board.Data
{
    public class ApiUser : IdentityUser<Guid>, IBaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
