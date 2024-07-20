namespace LibraryManagementSystem.Shared
{
    public class BaseEntity<TId>
    {
        public int Id { get; private set; }
        public DateTime Creation_Time { get; private set; }
        public DateTime LastUpdate_Time { get; set; }


    }
}
