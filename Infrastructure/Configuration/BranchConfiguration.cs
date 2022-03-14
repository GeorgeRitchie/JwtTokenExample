using EskhataDigital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EskhataDigital.Infrastructure.Configuration
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.Property(b => b.IsDeleted).HasDefaultValue(false);
            builder.HasOne(b => b.City).WithMany(c => c.Branches).HasForeignKey(b => b.CityId);
        }
    }
}
