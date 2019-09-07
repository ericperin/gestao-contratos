using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContractManager.Data.Migrations
{
    public partial class CreateContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ClientName = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    QuantityTraded = table.Column<decimal>(nullable: false),
                    NegotiatedValue = table.Column<decimal>(nullable: false),
                    StartedAt = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    File = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contract");
        }
    }
}
