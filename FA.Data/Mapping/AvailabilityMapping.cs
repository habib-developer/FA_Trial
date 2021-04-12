using FA.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FA.Data.Mapping
{
    public class AvailabilityMapping : IEntityTypeConfiguration<Availability>
    {
        public void Configure(EntityTypeBuilder<Availability> builder)
        {
            builder.ToTable("Availability");
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Freelancer)
                .WithMany(e => e.Availabilities)
                .HasForeignKey(e => e.FreelancerId);
            builder.HasOne(e => e.Project)
                .WithMany(e => e.Availabilities)
                .HasForeignKey(e => e.ProjectId);
        }
    }
}
