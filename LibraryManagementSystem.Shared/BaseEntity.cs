namespace LibraryManagementSystem.Shared
{
    public class BaseEntity<TId>
    {
        public int Id { get; set; }
        public DateTime Creation_Time { get; set; }
        public DateTime LastUpdate_Time { get; set; }


    }
}
