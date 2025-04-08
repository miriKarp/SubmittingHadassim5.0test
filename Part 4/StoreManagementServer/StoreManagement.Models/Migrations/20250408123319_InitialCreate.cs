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
                name: "Manager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products_Tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products_Tbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers_Tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepresentativeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Status = table.Column<int>(type: "int", nullable: false),
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
                name: "SupplierProduct_Tbl",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerItem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinimumQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierProduct_Tbl", x => new { x.SupplierId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_SupplierProduct_Tbl_Products_Tbl_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products_Tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierProduct_Tbl_Suppliers_Tbl_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers_Tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdersProduct_Tbl",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersProduct_Tbl", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrdersProduct_Tbl_Orders_Tbl_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders_Tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersProduct_Tbl_Products_Tbl_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products_Tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Tbl_SupplierId",
                table: "Orders_Tbl",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersProduct_Tbl_ProductId",
                table: "OrdersProduct_Tbl",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierProduct_Tbl_ProductId",
                table: "SupplierProduct_Tbl",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "OrdersProduct_Tbl");

            migrationBuilder.DropTable(
                name: "SupplierProduct_Tbl");

            migrationBuilder.DropTable(
                name: "Orders_Tbl");

            migrationBuilder.DropTable(
                name: "Products_Tbl");

            migrationBuilder.DropTable(
                name: "Suppliers_Tbl");
        }
    }
}
