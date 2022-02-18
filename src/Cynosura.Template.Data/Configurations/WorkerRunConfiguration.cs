using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cynosura.EF;
using Cynosura.Template.Core.Entities;

namespace Cynosura.Template.Data.Configurations
{
    public class WorkerRunConfiguration : IEntityTypeConfiguration<WorkerRun>
    {
        public void Configure(EntityTypeBuilder<WorkerRun> builder)
        {
            builder.ToTable("WorkerRuns");
        }
    }
}
