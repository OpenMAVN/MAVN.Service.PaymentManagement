using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.PaymentManagement.MsSqlRepositories.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "payment");

            migrationBuilder.CreateTable(
                name: "PaymentRequests",
                schema: "payment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PartnerId = table.Column<Guid>(nullable: false),
                    PaymentId = table.Column<string>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Currency = table.Column<string>(nullable: false),
                    PaymentStatus = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRequests", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_Currency",
                schema: "payment",
                table: "PaymentRequests",
                column: "Currency");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_CustomerId",
                schema: "payment",
                table: "PaymentRequests",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_PartnerId",
                schema: "payment",
                table: "PaymentRequests",
                column: "PartnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentRequests",
                schema: "payment");
        }
    }
}
