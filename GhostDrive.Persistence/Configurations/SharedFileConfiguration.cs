using GhostDrive.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GhostDrive.Persistence.Configurations
{
    public class SharedFileConfiguration : IEntityTypeConfiguration<SharedFile>
    {
        public void Configure(EntityTypeBuilder<SharedFile> builder)
        {
            builder.HasKey(t => new {t.FileId, t.UserId});

            builder.ToTable("ShareFiles");

            builder.HasOne(sharedFile => sharedFile.User)
                .WithMany(user => user.SharedFiles)
                .HasForeignKey(sharedFile => sharedFile.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sharedFile => sharedFile.File)
                .WithMany(file => file.SharedFiles)
                .HasForeignKey(sharedFile => sharedFile.FileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
