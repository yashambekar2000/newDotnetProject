using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewDotnetProject.Data;

namespace NewDotnetProject.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseMySqlIdentityColumn();

            builder.Property(n => n.studentName).IsRequired();
            builder.Property(n => n.studentName).HasMaxLength(250);
            builder.Property(n => n.studentAddress).IsRequired(false).HasMaxLength(500);
            builder.Property(n => n.studentEmail).IsRequired().HasMaxLength(250);

            builder.HasData(new List<Student>()
            {
                new Student {
                    Id = 1,
                    studentName = "Venkat",
                    studentAddress="India",
                    studentEmail="venkat@gmail.com",
                    DOB = new DateTime(2022,12,12)
                },
                new Student {
                    Id = 2,
                    studentName = "Nehanth",
                    studentAddress="India",
                    studentEmail="nehanth@gmail.com",
                    DOB = new DateTime(2022,06,12)
                }
            });

            builder.HasOne(n => n.College)
                .WithMany(n => n.Students)
                .HasForeignKey(n => n.CollegeId)
                .HasConstraintName("FK_Students_College");
        }
    }
}