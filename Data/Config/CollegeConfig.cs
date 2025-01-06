using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NewDotnetProject.Data;

namespace NewDotnetProject.Data.Config
{
    public class CollegeConfig : IEntityTypeConfiguration<College>
    {
        public void Configure(EntityTypeBuilder<College> builder)
        {
            builder.ToTable("Colleges");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseMySqlIdentityColumn();

            builder.Property(n => n.collegeName).IsRequired().HasMaxLength(200);
            builder.Property(n => n.collegeAddress).HasMaxLength(500).IsRequired(false);

            builder.HasData(new List<College>()
            {
                new College {
                    Id = 1,
                    collegeName = "ECE",
                    collegeAddress="ECE Department",
                },
                new College {
                    Id = 2,
                    collegeName = "CSE",
                    collegeAddress="CSE Department",
                }
            });
        }
    }
}