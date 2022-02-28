using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cynosura.EF;
using Cynosura.Template.Core.Entities;

namespace Cynosura.Template.Data.Configurations
{
    public class WorkerScheduleTaskConfiguration : IEntityTypeConfiguration<WorkerScheduleTask>
    {
        public void Configure(EntityTypeBuilder<WorkerScheduleTask> builder)
        {
            builder.ToTable("WorkerScheduleTasks");
        }
    }
}
