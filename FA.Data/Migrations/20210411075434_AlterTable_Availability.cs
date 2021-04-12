using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FA.Data.Migrations
{
    public partial class AlterTable_Availability : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availability_Availability_TypeId",
                table: "Availability");

            migrationBuilder.DropIndex(
                name: "IX_Availability_TypeId",
                table: "Availability");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Availability");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Availability",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Availability");

            migrationBuilder.AddColumn<Guid>(
                name: "TypeId",
                table: "Availability",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Availability_TypeId",
                table: "Availability",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Availability_Availability_TypeId",
                table: "Availability",
                column: "TypeId",
                principalTable: "Availability",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
