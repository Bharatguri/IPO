using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Subscriptions",
                newName: "SubscriptionMultiple");

            migrationBuilder.RenameColumn(
                name: "Retail",
                table: "Subscriptions",
                newName: "SharesOffered");

            migrationBuilder.RenameColumn(
                name: "QIB",
                table: "Subscriptions",
                newName: "SharesApplied");

            migrationBuilder.RenameColumn(
                name: "NII",
                table: "Subscriptions",
                newName: "Amount");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionDate",
                table: "Subscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionDate",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "SubscriptionMultiple",
                table: "Subscriptions",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "SharesOffered",
                table: "Subscriptions",
                newName: "Retail");

            migrationBuilder.RenameColumn(
                name: "SharesApplied",
                table: "Subscriptions",
                newName: "QIB");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Subscriptions",
                newName: "NII");
        }
    }
}
