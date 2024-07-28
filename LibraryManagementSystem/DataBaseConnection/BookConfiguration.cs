using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DataBaseConnection
{
    public class BookConfiguration: IEntityTypeConfiguration<Book>
    {
        // Fluent API configuration for Book
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Title).IsRequired().HasMaxLength(200);
            builder.Property(e => e.PublishedYear).IsRequired();
            builder.HasOne(e => e.Author).WithMany(a => a.Books).HasForeignKey(e => e.AuthorId);

            builder.Property(e => e.CreationTime).HasDefaultValue(null);
            builder.Property(e => e.LastUpdateTime).HasDefaultValue(null);

        }
    }
}
