using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Catalog.Infastructure.Migrations
{
    public partial class CatalogInitialDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SellerInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    SellerName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SellerDescription = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SellerPhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WigwamsInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WigwamsName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Height = table.Column<double>(type: "double precision", nullable: false),
                    Width = table.Column<double>(type: "double precision", nullable: false),
                    Weight = table.Column<double>(type: "double precision", nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    SellerInfoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WigwamsInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WigwamsInfos_SellerInfos_SellerInfoId",
                        column: x => x.SellerInfoId,
                        principalTable: "SellerInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WigwamsInfos_SellerInfoId",
                table: "WigwamsInfos",
                column: "SellerInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WigwamsInfos");

            migrationBuilder.DropTable(
                name: "SellerInfos");
        }
    }
}
