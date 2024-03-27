namespace HR_Board.Data.Entities.Interfaces
{
    public interface IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        bool IsDeleted { get; set; }
    }
}