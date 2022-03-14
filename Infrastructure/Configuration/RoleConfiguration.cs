using EskhataDigital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EskhataDigital.Infrastructure.Configuration
{
	public class RoleConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.Property(r => r.IsDeleted).HasDefaultValue(false);

			builder.HasMany(u => u.ControllerAccess).WithOne(u => u.Role).OnDelete(DeleteBehavior.Cascade);

		}
	}
}
