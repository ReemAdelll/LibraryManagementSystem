namespace LibraryManagementSystem.Shared
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int PublishedYear { get; set; }
    }
}
