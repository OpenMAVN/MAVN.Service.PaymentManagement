using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.PaymentManagement.MsSqlRepositories.Migrations
{
    public partial class AddIndexByExternalId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ExternalPaymentEntityId",
                schema: "payment",
                table: "payment_requests",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_payment_requests_ExternalPaymentEntityId",
                schema: "payment",
                table: "payment_requests",
                column: "ExternalPaymentEntityId",
                unique: true,
                filter: "[ExternalPaymentEntityId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_payment_requests_ExternalPaymentEntityId",
                schema: "payment",
                table: "payment_requests");

            migrationBuilder.AlterColumn<string>(
                name: "ExternalPaymentEntityId",
                schema: "payment",
                table: "payment_requests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
