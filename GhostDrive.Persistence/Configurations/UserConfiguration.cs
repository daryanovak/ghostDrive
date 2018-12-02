using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GhostDrive.Domain.Models;

namespace GhostDrive.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Id).HasColumnName("UserId");

            builder.Property(e => e.Login).IsRequired();
            builder.Property(e => e.Password).IsRequired();
            builder.Property(e => e.PasswordSalt).IsRequired();

            builder.HasMany(e => e.Files)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);
        }
    }
}
