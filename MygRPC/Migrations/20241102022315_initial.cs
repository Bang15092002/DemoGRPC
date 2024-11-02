using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MygRPC.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CusGender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CusBirthday = table.Column<DateOnly>(type: "date", nullable: false),
                    CusAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CusId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
