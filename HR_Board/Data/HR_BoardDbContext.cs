using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HR_Board.Data
{
    public class HR_BoardDbContext : IdentityDbContext<ApiUser>
    {
        public HR_BoardDbContext(DbContextOptions<HR_BoardDbContext> options) : base(options) 
        { 
        }
    }
}
