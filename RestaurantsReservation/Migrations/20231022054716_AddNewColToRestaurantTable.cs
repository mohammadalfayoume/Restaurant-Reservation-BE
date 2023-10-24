using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantsReservation.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColToRestaurantTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "RestaurantTable",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "RestaurantTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "RestaurantTable",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "RestaurantTable",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "RestaurantTable");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RestaurantTable");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "RestaurantTable");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "RestaurantTable");
        }
    }
}
