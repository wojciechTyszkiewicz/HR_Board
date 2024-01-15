namespace HR_Board.Data.Entities
{
    public class Job : BaseEntity
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string Logo { get; set; }
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }
        public string Title { get; set; }
    }
}
