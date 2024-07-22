using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.DataBaseConnection
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(a => a.MemberId);
            builder.Property(a => a.FirstName).IsRequired().HasMaxLength(10);
            builder.Property(a => a.LastName).IsRequired().HasMaxLength(10);
            builder.Property(a => a.PhoneNumber).IsRequired().HasMaxLength(11);
            builder.Property(a => a.Email).IsRequired();
        }
    }
}
