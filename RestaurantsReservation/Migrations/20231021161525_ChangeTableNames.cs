using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantsReservation.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationsSchedules_AspNetUsers_UserId",
                table: "ReservationsSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationsSchedules_RestaurantTable_RestaurantTableId",
                table: "ReservationsSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationsSchedules_Restaurants_RestaurantId",
                table: "ReservationsSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservationsSchedules",
                table: "ReservationsSchedules");

            migrationBuilder.RenameTable(
                name: "ReservationsSchedules",
                newName: "Reservations");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationsSchedules_UserId",
                table: "Reservations",
                newName: "IX_Reservations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationsSchedules_RestaurantTableId",
                table: "Reservations",
                newName: "IX_Reservations_RestaurantTableId");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationsSchedules_RestaurantId",
                table: "Reservations",
                newName: "IX_Reservations_RestaurantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_UserId",
                table: "Reservations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_RestaurantTable_RestaurantTableId",
                table: "Reservations",
                column: "RestaurantTableId",
                principalTable: "RestaurantTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Restaurants_RestaurantId",
                table: "Reservations",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_UserId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_RestaurantTable_RestaurantTableId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Restaurants_RestaurantId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.RenameTable(
                name: "Reservations",
                newName: "ReservationsSchedules");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_UserId",
                table: "ReservationsSchedules",
                newName: "IX_ReservationsSchedules_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_RestaurantTableId",
                table: "ReservationsSchedules",
                newName: "IX_ReservationsSchedules_RestaurantTableId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_RestaurantId",
                table: "ReservationsSchedules",
                newName: "IX_ReservationsSchedules_RestaurantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservationsSchedules",
                table: "ReservationsSchedules",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationsSchedules_AspNetUsers_UserId",
                table: "ReservationsSchedules",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationsSchedules_RestaurantTable_RestaurantTableId",
                table: "ReservationsSchedules",
                column: "RestaurantTableId",
                principalTable: "RestaurantTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationsSchedules_Restaurants_RestaurantId",
                table: "ReservationsSchedules",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
