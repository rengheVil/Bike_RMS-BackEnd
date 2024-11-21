using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeRentalMS.Migrations
{
    /// <inheritdoc />
    public partial class Tablesupdated3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "UpdateStatusRequests");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRequests_MotorbikeId",
                table: "RentalRequests",
                column: "MotorbikeId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRequests_UserId",
                table: "RentalRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistorys_MotorbikeId",
                table: "OrderHistorys",
                column: "MotorbikeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistorys_UserId",
                table: "OrderHistorys",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistorys_Motorbikes_MotorbikeId",
                table: "OrderHistorys",
                column: "MotorbikeId",
                principalTable: "Motorbikes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistorys_Users_UserId",
                table: "OrderHistorys",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalRequests_Motorbikes_MotorbikeId",
                table: "RentalRequests",
                column: "MotorbikeId",
                principalTable: "Motorbikes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalRequests_Users_UserId",
                table: "RentalRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistorys_Motorbikes_MotorbikeId",
                table: "OrderHistorys");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistorys_Users_UserId",
                table: "OrderHistorys");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalRequests_Motorbikes_MotorbikeId",
                table: "RentalRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalRequests_Users_UserId",
                table: "RentalRequests");

            migrationBuilder.DropIndex(
                name: "IX_RentalRequests_MotorbikeId",
                table: "RentalRequests");

            migrationBuilder.DropIndex(
                name: "IX_RentalRequests_UserId",
                table: "RentalRequests");

            migrationBuilder.DropIndex(
                name: "IX_OrderHistorys_MotorbikeId",
                table: "OrderHistorys");

            migrationBuilder.DropIndex(
                name: "IX_OrderHistorys_UserId",
                table: "OrderHistorys");

            migrationBuilder.AddColumn<string>(
                name: "RequestId",
                table: "UpdateStatusRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
