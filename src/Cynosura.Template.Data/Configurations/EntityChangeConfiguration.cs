using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cynosura.EF;
using Cynosura.Template.Core.Entities;

namespace Cynosura.Template.Data.Configurations
{
    public class EntityChangeConfiguration : IEntityTypeConfiguration<EntityChange>
    {
        public void Configure(EntityTypeBuilder<EntityChange> builder)
        {
            builder.ToTable("EntityChanges");
        }
    }
}
