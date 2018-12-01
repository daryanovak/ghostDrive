using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GhostDrive.Domain.Models;

namespace GhostDrive.Persistence.Configurations
{
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.Property(e => e.Id).HasColumnName("FileId");

            builder.Property(e => e.Name).IsRequired();
        }
    }
}
