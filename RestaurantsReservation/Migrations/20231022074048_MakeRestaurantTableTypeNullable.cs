using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantsReservation.Migrations
{
    /// <inheritdoc />
    public partial class MakeRestaurantTableTypeNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantTable_RestaurantTableType_TableTypeId",
                table: "RestaurantTable");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantTable_TableTypeId",
                table: "RestaurantTable");

            migrationBuilder.AlterColumn<int>(
                name: "TableTypeId",
                table: "RestaurantTable",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantTable_TableTypeId",
                table: "RestaurantTable",
                column: "TableTypeId",
                unique: true,
                filter: "[TableTypeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantTable_RestaurantTableType_TableTypeId",
                table: "RestaurantTable",
                column: "TableTypeId",
                principalTable: "RestaurantTableType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantTable_RestaurantTableType_TableTypeId",
                table: "RestaurantTable");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantTable_TableTypeId",
                table: "RestaurantTable");

            migrationBuilder.AlterColumn<int>(
                name: "TableTypeId",
                table: "RestaurantTable",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantTable_TableTypeId",
                table: "RestaurantTable",
                column: "TableTypeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantTable_RestaurantTableType_TableTypeId",
                table: "RestaurantTable",
                column: "TableTypeId",
                principalTable: "RestaurantTableType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
