using Microsoft.AspNetCore.Identity;

namespace HR_Board.Data
{
    public class ApiUser : BaseEntity
    {
        public string? FirstName {  get; set; }
        public string? LastName {  get; set; }
    }
}
