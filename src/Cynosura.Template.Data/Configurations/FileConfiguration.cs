﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cynosura.EF;
using Cynosura.Template.Core.Entities;

namespace Cynosura.Template.Data.Configurations
{
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.ToTable("Files");

            builder
                 .HasOne(e => e.Group)
                 .WithMany()
                 .HasForeignKey(e => e.GroupId)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
