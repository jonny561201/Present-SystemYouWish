using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WishSystem.Data.Models;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id).HasName("users_pkey");

        builder.ToTable("users");

        builder.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");
        builder.Property(e => e.Email).HasColumnName("email");
        builder.Property(e => e.FamilyName).HasColumnName("family_name");
        builder.Property(e => e.GivenName).HasColumnName("given_name");
        builder.Property(e => e.MiddleName).HasColumnName("middle_name");
    }
}