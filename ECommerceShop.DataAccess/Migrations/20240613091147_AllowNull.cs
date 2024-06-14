using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AllowNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 6, 13, 11, 11, 47, 237, DateTimeKind.Local).AddTicks(6583));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2024, 6, 13, 11, 11, 47, 237, DateTimeKind.Local).AddTicks(6704));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 6, 13, 10, 46, 30, 476, DateTimeKind.Local).AddTicks(8231));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2024, 6, 13, 10, 46, 30, 476, DateTimeKind.Local).AddTicks(8354));
        }
    }
}
