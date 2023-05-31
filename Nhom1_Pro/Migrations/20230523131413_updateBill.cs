using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nhom1_Pro.Migrations
{
    public partial class updateBill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Vouchers_IdUser",
                table: "Bills");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_IdVoucher",
                table: "Bills",
                column: "IdVoucher");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Vouchers_IdVoucher",
                table: "Bills",
                column: "IdVoucher",
                principalTable: "Vouchers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Vouchers_IdVoucher",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_IdVoucher",
                table: "Bills");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Vouchers_IdUser",
                table: "Bills",
                column: "IdUser",
                principalTable: "Vouchers",
                principalColumn: "Id");
        }
    }
}
