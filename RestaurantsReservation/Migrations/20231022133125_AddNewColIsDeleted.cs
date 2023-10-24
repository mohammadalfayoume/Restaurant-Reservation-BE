using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantsReservation.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColIsDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantTable_RestaurantTableType_TableTypeId",
                table: "RestaurantTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RestaurantTableType",
                table: "RestaurantTableType");

            migrationBuilder.RenameTable(
                name: "RestaurantTableType",
                newName: "RestaurantTableTypes");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RestaurantTableTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RestaurantTableTypes",
                table: "RestaurantTableTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantTable_RestaurantTableTypes_TableTypeId",
                table: "RestaurantTable",
                column: "TableTypeId",
                principalTable: "RestaurantTableTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantTable_RestaurantTableTypes_TableTypeId",
                table: "RestaurantTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RestaurantTableTypes",
                table: "RestaurantTableTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RestaurantTableTypes");

            migrationBuilder.RenameTable(
                name: "RestaurantTableTypes",
                newName: "RestaurantTableType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RestaurantTableType",
                table: "RestaurantTableType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantTable_RestaurantTableType_TableTypeId",
                table: "RestaurantTable",
                column: "TableTypeId",
                principalTable: "RestaurantTableType",
                principalColumn: "Id");
        }
    }
}
