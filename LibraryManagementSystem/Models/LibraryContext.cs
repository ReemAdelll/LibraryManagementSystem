using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Models
{
	public class LibraryContext:DbContext
	{
        public LibraryContext()
        {
            
        }
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
		{
		}
		public DbSet<Author> authors {  get; set; }
		public DbSet<Book> books { get; set; }

		//fluent api
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Author>()
				.HasMany(a => a.Books)
				.WithOne(b => b.Author)
				.HasForeignKey(b => b.Author_Id);
		}
	}
}
