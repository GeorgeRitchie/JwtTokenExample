using EskhataDigital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EskhataDigital.Infrastructure.Configuration
{
	public class UserAuthenticationDataConfiguration : IEntityTypeConfiguration<UserAuthenticationData>
	{
		public void Configure(EntityTypeBuilder<UserAuthenticationData> builder)
		{
			builder.Property(u=>u.IsDeleted).HasDefaultValue(false);
			builder.HasOne(u => u.User).WithOne(u => u.UserAuthenticationData).HasForeignKey<UserAuthenticationData>(u => u.UserId).OnDelete(DeleteBehavior.Cascade);
			builder.Property(u => u.Login).IsRequired();
			builder.Property(u=>u.Password).IsRequired();
			builder.Property(u=>u.RefreshToken).HasDefaultValue(null);

		}
	}
}
