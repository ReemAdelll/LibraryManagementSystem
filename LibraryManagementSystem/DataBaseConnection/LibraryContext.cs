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

		//Fluent API
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
        }
	}
}
