using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DataBaseConnection
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(e=>e.Id);
            builder.Property(e => e.GenreName).IsRequired().HasMaxLength(50);

            builder.Property(e => e.CreationTime).HasDefaultValue(null);
            builder.Property(e => e.LastUpdateTime).HasDefaultValue(null);
        }
    }
}
