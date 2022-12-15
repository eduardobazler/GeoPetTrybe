using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoPet.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeoPet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Localization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoPet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    PetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Breed = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.PetId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "PetId", "Age", "Breed", "Name", "Size", "UserId" },
                values: new object[,]
                {
                    { 1, 4, 16, "Pequenino", "Large", 3 },
                    { 2, 2, 5, "Zangado", "Small", 2 },
                    { 3, 3, 11, "Pandora", "Medium", 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Cep", "Created", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "18037-300", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "teste1@teste.com.br", "Adriano", "123456" },
                    { 2, "18037-310", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "teste2@teste.com.br", "Eduardo", "654321" },
                    { 3, "18037-320", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "teste3@teste.com.br", "Astolfo", "321654" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeoPet");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
