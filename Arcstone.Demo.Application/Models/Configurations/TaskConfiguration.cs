using Arcstone.Demo.Application.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arcstone.Demo.Application.Models.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<TaskModel>
    {
        public void Configure(EntityTypeBuilder<TaskModel> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.StartTime).IsRequired();

            builder.Property(x => x.EndTime).IsRequired();

            builder.HasOne(x => x.Project)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.ProjectId).IsRequired();

            builder.Property(x => x.Active).HasDefaultValue(true);

            builder.Property(x => x.Date).IsRequired();
        }
    }
}