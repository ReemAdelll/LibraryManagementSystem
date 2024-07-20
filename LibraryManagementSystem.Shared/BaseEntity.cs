namespace LibraryManagementSystem.Shared
{
    public class BaseEntity<TId>
    {
        public int Id { get; private set; }
        public DateTime CreationTime { get; private set; }
        public DateTime LastUpdateTime { get; set; }


    }
}
