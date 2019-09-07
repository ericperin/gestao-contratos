using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContractManager.Data.Migrations
{
    public partial class ChangedFileType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "File",
                table: "Contract",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "File",
                table: "Contract",
                nullable: true,
                oldClrType: typeof(byte[]));
        }
    }
}
