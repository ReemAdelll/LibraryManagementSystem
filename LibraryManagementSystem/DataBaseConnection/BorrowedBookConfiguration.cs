using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DataBaseConnection
{
    public class BorrowedBookConfiguration : IEntityTypeConfiguration<BorrowedBook>
    {
        public void Configure(EntityTypeBuilder<BorrowedBook> builder)
        {
            builder.HasKey(b => b.BorrowedBookId);
            builder.Property(b=>b.BookId).IsRequired();
            builder.Property(b => b.MemberId).IsRequired();
            builder.Property(b => b.BorrowDate).IsRequired();
            builder.Property(b => b.ReturnDate).IsRequired();
        }
    }
}
