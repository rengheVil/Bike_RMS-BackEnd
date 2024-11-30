using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeRentalMS.Migrations
{
    /// <inheritdoc />
    public partial class rentaltableupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "Rentals",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "Rentals");
        }
    }
}
