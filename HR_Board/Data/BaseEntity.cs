using HR_Board.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HR_Board.Data
{
    public abstract class BaseEntity : IdentityUser<Guid>, IBaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
