using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.PaymentManagement.MsSqlRepositories.Migrations
{
    public partial class MakeIndexByExternalIdNonUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_payment_requests_ExternalPaymentEntityId",
                schema: "payment",
                table: "payment_requests");

            migrationBuilder.CreateIndex(
                name: "IX_payment_requests_ExternalPaymentEntityId",
                schema: "payment",
                table: "payment_requests",
                column: "ExternalPaymentEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_payment_requests_ExternalPaymentEntityId",
                schema: "payment",
                table: "payment_requests");

            migrationBuilder.CreateIndex(
                name: "IX_payment_requests_ExternalPaymentEntityId",
                schema: "payment",
                table: "payment_requests",
                column: "ExternalPaymentEntityId",
                unique: true,
                filter: "[ExternalPaymentEntityId] IS NOT NULL");
        }
    }
}
