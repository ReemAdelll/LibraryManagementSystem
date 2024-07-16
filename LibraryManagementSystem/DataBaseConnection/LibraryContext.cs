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

		//fluent api
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			// Fluent API configuration for Author
			modelBuilder.Entity<Author>(entity =>
			{
				entity.HasKey(e => e.Author_Id);
				entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
				entity.Property(e => e.Bio).HasMaxLength(1000).IsRequired(false);
				entity.Property(e => e.Country).HasMaxLength(50);
				entity.HasMany(e => e.Books).WithOne(b => b.Author).HasForeignKey(b => b.Author_Id);
			});

			// Fluent API configuration for Book
			  modelBuilder.Entity<Book>(entity =>
			{
				entity.HasKey(e => e.Book_Id);
				entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
				
				entity.Property(e => e.PublishedYear).IsRequired();
				entity.HasOne(e => e.Author).WithMany(a => a.Books).HasForeignKey(e => e.Author_Id);
			});
		}
	}
}
