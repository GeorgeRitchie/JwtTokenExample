using EskhataDigital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EskhataDigital.Infrastructure.Configuration
{
	public class ActionAccessConfiguration : IEntityTypeConfiguration<ActionAccess>
	{
		public void Configure(EntityTypeBuilder<ActionAccess> builder)
		{
			builder.Property(u => u.IsDeleted).HasDefaultValue(false);

			builder.HasMany(u => u.ControllerAccess).WithMany(u => u.Actions);
		}
	}
}
