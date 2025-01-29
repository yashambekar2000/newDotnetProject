﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NewDotnetProject.Data;

#nullable disable

namespace newDotnetProject.Migrations
{
    [DbContext(typeof(CollegeDBContext))]
    partial class CollegeDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("NewDotnetProject.Data.College", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("collegeAddress")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("collegeName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Colleges", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            collegeAddress = "ECE Department",
                            collegeName = "ECE"
                        },
                        new
                        {
                            Id = 2,
                            collegeAddress = "CSE Department",
                            collegeName = "CSE"
                        });
                });

            modelBuilder.Entity("NewDotnetProject.Data.GetHomeImages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("image_url")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("HomeImages");
                });

            modelBuilder.Entity("NewDotnetProject.Data.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("image_url")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<double>("new_price")
                        .HasColumnType("double");

                    b.Property<double>("old_price")
                        .HasColumnType("double");

                    b.Property<int>("stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Items", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            category = "Electronics",
                            description = "High-quality wireless headphones with noise cancellation.",
                            gender = "",
                            image_url = "https://via.placeholder.com/300?text=Wireless+Headphones",
                            name = "Wireless Headphones",
                            new_price = 99.989999999999995,
                            old_price = 102.98999999999999,
                            stock = 50
                        },
                        new
                        {
                            Id = 2,
                            category = "Footwear",
                            description = "Lightweight running shoes for maximum comfort.",
                            gender = "men",
                            image_url = "https://via.placeholder.com/300?text=Running+Shoes",
                            name = "Running Shoes - MEN",
                            new_price = 59.990000000000002,
                            old_price = 102.98999999999999,
                            stock = 100
                        },
                        new
                        {
                            Id = 3,
                            category = "Electronics",
                            description = "Track your fitness and notifications with this stylish smartwatch.",
                            gender = "women",
                            image_url = "https://via.placeholder.com/300?text=Smartwatch",
                            name = "Smartwatch - WOMEN",
                            new_price = 79.989999999999995,
                            old_price = 102.98999999999999,
                            stock = 30
                        },
                        new
                        {
                            Id = 4,
                            category = "Accessories",
                            description = "Durable leather backpack for everyday use.",
                            gender = "",
                            image_url = "https://via.placeholder.com/300?text=Leather+Backpack",
                            name = "Leather Backpack",
                            new_price = 92.989999999999995,
                            old_price = 102.98999999999999,
                            stock = 20
                        },
                        new
                        {
                            Id = 5,
                            category = "Electronics",
                            description = "RGB backlit mechanical keyboard for gaming.",
                            gender = "men",
                            image_url = "https://via.placeholder.com/300?text=Gaming+Keyboard",
                            name = "Gaming Keyboard - MEN",
                            new_price = 56.990000000000002,
                            old_price = 102.98999999999999,
                            stock = 20
                        });
                });

            modelBuilder.Entity("NewDotnetProject.Data.SavedItem", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("SavedItems", (string)null);
                });

            modelBuilder.Entity("NewDotnetProject.Data.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CollegeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("studentAddress")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("studentEmail")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("studentName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("CollegeId");

                    b.ToTable("Students", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DOB = new DateTime(2022, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            studentAddress = "India",
                            studentEmail = "venkat@gmail.com",
                            studentName = "Venkat"
                        },
                        new
                        {
                            Id = 2,
                            DOB = new DateTime(2022, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            studentAddress = "India",
                            studentEmail = "nehanth@gmail.com",
                            studentName = "Nehanth"
                        });
                });

            modelBuilder.Entity("NewDotnetProject.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            email = "yash@gmail.com",
                            name = "yash",
                            password = "yasha12345"
                        });
                });

            modelBuilder.Entity("NewDotnetProject.Data.SavedItem", b =>
                {
                    b.HasOne("NewDotnetProject.Data.Item", "Item")
                        .WithMany("SavedItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NewDotnetProject.Data.User", "User")
                        .WithMany("SavedItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NewDotnetProject.Data.Student", b =>
                {
                    b.HasOne("NewDotnetProject.Data.College", "College")
                        .WithMany("Students")
                        .HasForeignKey("CollegeId")
                        .HasConstraintName("FK_Students_College");

                    b.Navigation("College");
                });

            modelBuilder.Entity("NewDotnetProject.Data.College", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("NewDotnetProject.Data.Item", b =>
                {
                    b.Navigation("SavedItems");
                });

            modelBuilder.Entity("NewDotnetProject.Data.User", b =>
                {
                    b.Navigation("SavedItems");
                });
#pragma warning restore 612, 618
        }
    }
}
