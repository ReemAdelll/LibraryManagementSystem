using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LibraryManagementSystem.Models
{
    public class BaseEntity<TId>
    {
        public int Id { get; set; }
        public DateTime Creation_Time { get; set; }
        public DateTime lastUpdate_Time { get; set; }


    }
}
