using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewDotnetProject.Data;

namespace NewDotnetProject.Data.Config
{
    public class ItemConfig : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseMySqlIdentityColumn();

            builder.Property(n => n.name).IsRequired().HasMaxLength(250);
            builder.Property(n => n.description).IsRequired().HasMaxLength(500);
            builder.Property(n => n.new_price);
             builder.Property(n => n.old_price);
               builder.Property(n => n.gender);
             builder.Property(n => n.category);
              builder.Property(n => n.image_url);
                builder.Property(n => n.stock);

             


            builder.HasData(new List<Item>()
            {
                new Item {
                Id = 1,
                name = "Wireless Headphones",
                description = "High-quality wireless headphones with noise cancellation.",
                new_price = 99.99,
                old_price = 102.99,
                gender = "",
                category = "Electronics",
                image_url = "https://via.placeholder.com/300?text=Wireless+Headphones",
                stock = 50
                },
                 new Item {
                Id = 2,
                name = "Running Shoes - MEN",
                description = "Lightweight running shoes for maximum comfort.",
                new_price = 59.99,
                old_price = 102.99,
                category = "Footwear",
                gender = "men",
                image_url = "https://via.placeholder.com/300?text=Running+Shoes",
                stock = 100
                },

                  new Item {
                Id = 3,
                name = "Smartwatch - WOMEN",
                description = "Track your fitness and notifications with this stylish smartwatch.",
                new_price = 79.99,
                old_price = 102.99,
                category = "Electronics",
                gender = "women",
                image_url = "https://via.placeholder.com/300?text=Smartwatch",
                stock = 30
                },
               
                new Item {
                Id = 4,
                name = "Leather Backpack",
                description = "Durable leather backpack for everyday use.",
                new_price = 92.99,
                old_price = 102.99,
                category = "Accessories",
                gender = "",
                image_url = "https://via.placeholder.com/300?text=Leather+Backpack",
                stock = 20
                },

                 new Item {
                Id = 5,
                name = "Gaming Keyboard - MEN",
                description = "RGB backlit mechanical keyboard for gaming.",
                new_price = 56.99,
                old_price = 102.99,
                category = "Electronics",
                gender = "men",
                image_url = "https://via.placeholder.com/300?text=Gaming+Keyboard",
                stock = 20
                }
            });
        }
    }
}