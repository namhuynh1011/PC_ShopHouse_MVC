using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PC_ShopHouse.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Tạo bảng Brands
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            // 2. Thêm cột BrandId vào Products (nullable trước để tránh lỗi dữ liệu cũ)
            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Products",
                type: "int",
                nullable: true);

            // 3. Nếu muốn gán dữ liệu mặc định cho các sản phẩm cũ, có thể seed một Brand default và update BrandId
            migrationBuilder.Sql("INSERT INTO Brands (BrandName) VALUES (N'No Brand')");
            migrationBuilder.Sql("UPDATE Products SET BrandId = (SELECT TOP 1 Id FROM Brands WHERE BrandName = N'No Brand')");

            // 4. Đặt lại cột BrandId thành NOT NULL nếu muốn
            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            // 5. Tạo chỉ mục và khóa ngoại
            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BrandId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}