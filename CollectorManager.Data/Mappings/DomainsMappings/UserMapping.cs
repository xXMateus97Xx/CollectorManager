using CollectorManager.Data.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollectorManager.Data.Mappings.DomainsMappings;

internal class UserMapping : EntityMapping<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
        builder.Property(x => x.UserName).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Password).IsRequired().HasMaxLength(64);
        builder.Property(x => x.PasswordSalt).IsRequired().HasMaxLength(20);
    }
}
