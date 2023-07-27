using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace ServiceShop.Api.ShoppingCart.Migrations
{
    /// <inheritdoc />
    public partial class InitialMySqlMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CartSession",
                columns: table => new
                {
                    CartSessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartSession", x => x.CartSessionId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CartSessionDetail",
                columns: table => new
                {
                    CartSessionDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    SelectedProduct = table.Column<string>(type: "longtext", nullable: false),
                    CartSessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartSessionDetail", x => x.CartSessionDetailId);
                    table.ForeignKey(
                        name: "FK_CartSessionDetail_CartSession_CartSessionId",
                        column: x => x.CartSessionId,
                        principalTable: "CartSession",
                        principalColumn: "CartSessionId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CartSessionDetail_CartSessionId",
                table: "CartSessionDetail",
                column: "CartSessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartSessionDetail");

            migrationBuilder.DropTable(
                name: "CartSession");
        }
    }
}
