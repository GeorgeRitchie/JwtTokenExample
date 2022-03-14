using EskhataDigital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EskhataDigital.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(u => u.InvitationReference);
            builder.Property(u => u.InvitationReference).IsRequired();
            builder.HasIndex(u => new { u.Email, u.Phone }).IsUnique();
            builder.Property(u => u.IsDeleted).HasDefaultValue(false);
            builder.HasOne(u => u.Role).WithOne(r => r.User).HasForeignKey<Role>().OnDelete(DeleteBehavior.Cascade);
          
        }
    }
}
