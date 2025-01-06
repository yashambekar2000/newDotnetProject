using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace newDotnetProject.Migrations
{
    /// <inheritdoc />
    public partial class ItemCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    new_price = table.Column<double>(type: "double", nullable: false),
                    old_price = table.Column<double>(type: "double", nullable: false),
                    gender = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    category = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image_url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "category", "description", "gender", "image_url", "name", "new_price", "old_price", "stock" },
                values: new object[,]
                {
                    { 1, "Electronics", "High-quality wireless headphones with noise cancellation.", "", "https://via.placeholder.com/300?text=Wireless+Headphones", "Wireless Headphones", 99.989999999999995, 102.98999999999999, 50 },
                    { 2, "Footwear", "Lightweight running shoes for maximum comfort.", "men", "https://via.placeholder.com/300?text=Running+Shoes", "Running Shoes - MEN", 59.990000000000002, 102.98999999999999, 100 },
                    { 3, "Electronics", "Track your fitness and notifications with this stylish smartwatch.", "women", "https://via.placeholder.com/300?text=Smartwatch", "Smartwatch - WOMEN", 79.989999999999995, 102.98999999999999, 30 },
                    { 4, "Accessories", "Durable leather backpack for everyday use.", "", "https://via.placeholder.com/300?text=Leather+Backpack", "Leather Backpack", 92.989999999999995, 102.98999999999999, 20 },
                    { 5, "Electronics", "RGB backlit mechanical keyboard for gaming.", "men", "https://via.placeholder.com/300?text=Gaming+Keyboard", "Gaming Keyboard - MEN", 56.990000000000002, 102.98999999999999, 20 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
