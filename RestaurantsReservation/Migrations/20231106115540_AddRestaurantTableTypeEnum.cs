using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantsReservation.Migrations
{
    /// <inheritdoc />
    public partial class AddRestaurantTableTypeEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantTable_RestaurantTableTypes_TableTypeId",
                table: "RestaurantTable");

            migrationBuilder.DropTable(
                name: "RestaurantTableTypes");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantTable_TableTypeId",
                table: "RestaurantTable");

            migrationBuilder.DropColumn(
                name: "TableTypeId",
                table: "RestaurantTable");

            migrationBuilder.AddColumn<string>(
                name: "RestaurantTableType",
                table: "RestaurantTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RestaurantTableType",
                table: "RestaurantTable");

            migrationBuilder.AddColumn<int>(
                name: "TableTypeId",
                table: "RestaurantTable",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RestaurantTableTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TableType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantTableTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantTable_TableTypeId",
                table: "RestaurantTable",
                column: "TableTypeId",
                unique: true,
                filter: "[TableTypeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantTable_RestaurantTableTypes_TableTypeId",
                table: "RestaurantTable",
                column: "TableTypeId",
                principalTable: "RestaurantTableTypes",
                principalColumn: "Id");
        }
    }
}
