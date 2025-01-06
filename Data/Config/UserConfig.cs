using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewDotnetProject.Data;

namespace NewDotnetProject.Data.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseMySqlIdentityColumn();

            builder.Property(n => n.name).IsRequired();
            builder.Property(n => n.name).HasMaxLength(250);
            builder.Property(n => n.password).IsRequired().HasMaxLength(500);
            builder.Property(n => n.email).IsRequired().HasMaxLength(250);

            builder.HasData(new List<User>()
            {
                new User {
                    Id = 1,
                    name = "yash",
                    password="yasha12345",
                    email="yash@gmail.com",
                }
            });
        }
    }
}