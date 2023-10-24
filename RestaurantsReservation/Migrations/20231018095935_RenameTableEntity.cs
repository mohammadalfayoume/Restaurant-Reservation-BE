using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantsReservation.Migrations
{
    /// <inheritdoc />
    public partial class RenameTableEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationsSchedules_Tables_RestaurantTableId",
                table: "ReservationsSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_RestaurantTableType_TableTypeId",
                table: "Tables");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Restaurants_RestaurantId",
                table: "Tables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tables",
                table: "Tables");

            migrationBuilder.RenameTable(
                name: "Tables",
                newName: "RestaurantTable");

            migrationBuilder.RenameIndex(
                name: "IX_Tables_TableTypeId",
                table: "RestaurantTable",
                newName: "IX_RestaurantTable_TableTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Tables_RestaurantId",
                table: "RestaurantTable",
                newName: "IX_RestaurantTable_RestaurantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RestaurantTable",
                table: "RestaurantTable",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationsSchedules_RestaurantTable_RestaurantTableId",
                table: "ReservationsSchedules",
                column: "RestaurantTableId",
                principalTable: "RestaurantTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantTable_RestaurantTableType_TableTypeId",
                table: "RestaurantTable",
                column: "TableTypeId",
                principalTable: "RestaurantTableType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantTable_Restaurants_RestaurantId",
                table: "RestaurantTable",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationsSchedules_RestaurantTable_RestaurantTableId",
                table: "ReservationsSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantTable_RestaurantTableType_TableTypeId",
                table: "RestaurantTable");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantTable_Restaurants_RestaurantId",
                table: "RestaurantTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RestaurantTable",
                table: "RestaurantTable");

            migrationBuilder.RenameTable(
                name: "RestaurantTable",
                newName: "Tables");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantTable_TableTypeId",
                table: "Tables",
                newName: "IX_Tables_TableTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantTable_RestaurantId",
                table: "Tables",
                newName: "IX_Tables_RestaurantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tables",
                table: "Tables",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationsSchedules_Tables_RestaurantTableId",
                table: "ReservationsSchedules",
                column: "RestaurantTableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_RestaurantTableType_TableTypeId",
                table: "Tables",
                column: "TableTypeId",
                principalTable: "RestaurantTableType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Restaurants_RestaurantId",
                table: "Tables",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id");
        }
    }
}
