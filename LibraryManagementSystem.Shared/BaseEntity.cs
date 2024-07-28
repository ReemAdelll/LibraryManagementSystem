namespace LibraryManagementSystem.Shared
{
    public class BaseEntity<TId>
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}
