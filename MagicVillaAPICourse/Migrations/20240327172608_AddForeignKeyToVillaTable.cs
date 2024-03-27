using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVillaAPICourse.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaID",
                table: "VillasNumber",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 27, 17, 26, 8, 64, DateTimeKind.Local).AddTicks(5524));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 27, 17, 26, 8, 64, DateTimeKind.Local).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 27, 17, 26, 8, 64, DateTimeKind.Local).AddTicks(5573));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 27, 17, 26, 8, 64, DateTimeKind.Local).AddTicks(5575));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 27, 17, 26, 8, 64, DateTimeKind.Local).AddTicks(5578));

            migrationBuilder.CreateIndex(
                name: "IX_VillasNumber_VillaID",
                table: "VillasNumber",
                column: "VillaID");

            migrationBuilder.AddForeignKey(
                name: "FK_VillasNumber_Villas_VillaID",
                table: "VillasNumber",
                column: "VillaID",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillasNumber_Villas_VillaID",
                table: "VillasNumber");

            migrationBuilder.DropIndex(
                name: "IX_VillasNumber_VillaID",
                table: "VillasNumber");

            migrationBuilder.DropColumn(
                name: "VillaID",
                table: "VillasNumber");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 27, 0, 46, 56, 669, DateTimeKind.Local).AddTicks(6978));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 27, 0, 46, 56, 669, DateTimeKind.Local).AddTicks(7029));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 27, 0, 46, 56, 669, DateTimeKind.Local).AddTicks(7031));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 27, 0, 46, 56, 669, DateTimeKind.Local).AddTicks(7034));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 27, 0, 46, 56, 669, DateTimeKind.Local).AddTicks(7036));
        }
    }
}
