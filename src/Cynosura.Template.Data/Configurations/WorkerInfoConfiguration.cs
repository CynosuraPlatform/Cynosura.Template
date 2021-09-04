using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cynosura.EF;
using Cynosura.Template.Core.Entities;

namespace Cynosura.Template.Data.Configurations
{
    public class WorkerInfoConfiguration : IEntityTypeConfiguration<WorkerInfo>
    {
        public void Configure(EntityTypeBuilder<WorkerInfo> builder)
        {
            builder.ToTable("WorkerInfos");
        }
    }
}
