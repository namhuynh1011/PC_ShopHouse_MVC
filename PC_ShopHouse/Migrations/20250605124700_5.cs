using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PC_ShopHouse.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GPUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Memory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemoryType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenmoryBus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusInterface = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoreClock = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoostClock = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TDP = table.Column<int>(type: "int", nullable: false),
                    OutputPorts = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GPUs_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GPUs_ProductId",
                table: "GPUs",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GPUs");
        }
    }
}
