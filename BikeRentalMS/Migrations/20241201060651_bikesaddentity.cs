using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeRentalMS.Migrations
{
    /// <inheritdoc />
    public partial class bikesaddentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Motorbikes",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Motorbikes");
        }
    }
}
