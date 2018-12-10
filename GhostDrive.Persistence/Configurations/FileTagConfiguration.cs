using GhostDrive.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostDrive.Persistence.Configurations
{
    public class FileTagConfiguration : IEntityTypeConfiguration<FileTag>
    {
        public void Configure(EntityTypeBuilder<FileTag> builder)
        {
            builder.HasKey(t => new { t.FileId, t.TagId });

            builder.HasOne(fileTag => fileTag.Tag)
                .WithMany(tag => tag.Files)
                .HasForeignKey(fileTag => fileTag.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(fileTag => fileTag.File)
                .WithMany(file => file.Tags)
                .HasForeignKey(fileTag => fileTag.FileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
