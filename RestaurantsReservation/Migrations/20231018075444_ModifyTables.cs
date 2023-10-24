using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantsReservation.Migrations
{
    /// <inheritdoc />
    public partial class ModifyTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationsSchedules_Tables_TableId",
                table: "ReservationsSchedules");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Tables",
                newName: "TableTypeId");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Restaurants",
                newName: "Region");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "ReservationsSchedules",
                newName: "ReservationPeriod");

            migrationBuilder.RenameColumn(
                name: "TableId",
                table: "ReservationsSchedules",
                newName: "RestaurantTableId");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "ReservationsSchedules",
                newName: "ReservationDate");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationsSchedules_TableId",
                table: "ReservationsSchedules",
                newName: "IX_ReservationsSchedules_RestaurantTableId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tables",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CloseAt",
                table: "Restaurants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Restaurants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Restaurants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Restaurants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OpenAt",
                table: "Restaurants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Restaurants",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "ReservationsSchedules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ReservationsSchedules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RestaurantTableType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantTableType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tables_TableTypeId",
                table: "Tables",
                column: "TableTypeId",
                unique: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationsSchedules_Tables_RestaurantTableId",
                table: "ReservationsSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_RestaurantTableType_TableTypeId",
                table: "Tables");

            migrationBuilder.DropTable(
                name: "RestaurantTableType");

            migrationBuilder.DropIndex(
                name: "IX_Tables_TableTypeId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "CloseAt",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OpenAt",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "ReservationsSchedules");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ReservationsSchedules");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "TableTypeId",
                table: "Tables",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Region",
                table: "Restaurants",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "RestaurantTableId",
                table: "ReservationsSchedules",
                newName: "TableId");

            migrationBuilder.RenameColumn(
                name: "ReservationPeriod",
                table: "ReservationsSchedules",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "ReservationDate",
                table: "ReservationsSchedules",
                newName: "Date");

            migrationBuilder.RenameIndex(
                name: "IX_ReservationsSchedules_RestaurantTableId",
                table: "ReservationsSchedules",
                newName: "IX_ReservationsSchedules_TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationsSchedules_Tables_TableId",
                table: "ReservationsSchedules",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
