using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Users",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 4, 23, 1, 18, 52, 629, DateTimeKind.Local).AddTicks(7303));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 4, 23, 1, 18, 52, 629, DateTimeKind.Local).AddTicks(8465));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 4, 23, 1, 18, 52, 629, DateTimeKind.Local).AddTicks(8909));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 4, 23, 1, 18, 52, 629, DateTimeKind.Local).AddTicks(9316));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 4, 23, 1, 18, 52, 629, DateTimeKind.Local).AddTicks(7974));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 23, 1, 18, 52, 629, DateTimeKind.Local).AddTicks(7303),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 23, 1, 18, 52, 629, DateTimeKind.Local).AddTicks(8465),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 23, 1, 18, 52, 629, DateTimeKind.Local).AddTicks(8909),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 23, 1, 18, 52, 629, DateTimeKind.Local).AddTicks(9316),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 23, 1, 18, 52, 629, DateTimeKind.Local).AddTicks(7974),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
