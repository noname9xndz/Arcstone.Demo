using Arcstone.Demo.Application.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arcstone.Demo.Application.Models.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<ProjectModel>
    {
        public void Configure(EntityTypeBuilder<ProjectModel> builder)
        {
            builder.ToTable("Projects");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.ClientName).IsRequired();

            builder.Property(x => x.Active).HasDefaultValue(true);

            builder.Ignore(x => x.TotalTime);

            builder.Ignore(x => x.TaskGroupByDateModels);

            builder.Ignore(x => x.TotalTimeWithCondition);
        }
    }
}