using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DataBaseConnection
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        // Fluent API configuration for Author
        public void Configure(EntityTypeBuilder<Author> builder) 
        {
           builder.HasKey(e => e.AuthorId);
           builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Bio).HasMaxLength(1000).IsRequired(false);
            builder.Property(e => e.Country).HasMaxLength(50);
            builder.HasMany(e => e.Books).WithOne(b => b.Author).HasForeignKey(b => b.AuthorId);

            builder.Property(e => e.CreationTime).HasDefaultValue(null);
            builder.Property(e => e.LastUpdateTime).HasDefaultValue(null);
        }
    }
}
