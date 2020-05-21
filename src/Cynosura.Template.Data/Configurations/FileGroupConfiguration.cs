using Cynosura.EF;
using Cynosura.Template.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cynosura.Template.Data.Configurations
{
    public class FileGroupConfiguration : IEntityTypeConfiguration<FileGroup>
    {
        public void Configure(EntityTypeBuilder<FileGroup> builder)
        {
            builder.ToTable("FileGroups");
        }
    }
}
