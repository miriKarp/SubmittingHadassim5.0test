using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagement.Models.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Suppliers_Tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepresentativeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers_Tbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders_Tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders_Tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Tbl_Suppliers_Tbl_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers_Tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products_Tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    MinOrderQuantity = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products_Tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Tbl_Suppliers_Tbl_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers_Tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Tbl_SupplierId",
                table: "Orders_Tbl",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Tbl_SupplierId",
                table: "Products_Tbl",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders_Tbl");

            migrationBuilder.DropTable(
                name: "Products_Tbl");

            migrationBuilder.DropTable(
                name: "Suppliers_Tbl");
        }
    }
}
