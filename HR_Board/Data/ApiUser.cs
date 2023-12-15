using Microsoft.AspNetCore.Identity;

namespace HR_Board.Data
{
    public class ApiUser : IdentityUser
    {
        public string FirstName {  get; set; }
        public string LastName {  get; set; }
    }
}
