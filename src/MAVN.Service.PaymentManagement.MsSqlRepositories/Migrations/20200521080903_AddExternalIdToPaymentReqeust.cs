using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.PaymentManagement.MsSqlRepositories.Migrations
{
    public partial class AddExternalIdToPaymentReqeust : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalPaymentEntityId",
                schema: "payment",
                table: "payment_requests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalPaymentEntityId",
                schema: "payment",
                table: "payment_requests");
        }
    }
}
