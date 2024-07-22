using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DataBaseConnection
{
	public class LibraryContext : DbContext
	{
		public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
		{
		}
		public DbSet<Author> Authors { get; set; }
		
		public DbSet<Book> Books { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Member> Members { get; set; }
		public DbSet<BorrowedBook> BorrowedBooks { get; set; }

        //Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
			modelBuilder.ApplyConfiguration(new  GenreConfiguration());
			modelBuilder.ApplyConfiguration(new  MemberConfiguration());
			modelBuilder.ApplyConfiguration(new  BorrowedBookConfiguration());
        }
	}
}
