﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vk.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Authors",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                    table.UniqueConstraint("AK_Authors_AuthorNumber", x => x.AuthorNumber);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryNumber = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.UniqueConstraint("AK_Categories_CategoryNumber", x => x.CategoryNumber);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserNumber = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LastActivityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PasswordRetryCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookNumber = table.Column<int>(type: "int", precision: 10, nullable: false),
                    HeadLine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PageCount = table.Column<int>(type: "int", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ISNB = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "dbo",
                        principalTable: "Authors",
                        principalColumn: "AuthorNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "dbo",
                        principalTable: "Categories",
                        principalColumn: "CategoryNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authors_AuthorNumber",
                schema: "dbo",
                table: "Authors",
                column: "AuthorNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                schema: "dbo",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookNumber",
                schema: "dbo",
                table: "Books",
                column: "BookNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                schema: "dbo",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryNumber",
                schema: "dbo",
                table: "Categories",
                column: "CategoryNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Authors",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "dbo");
        }
    }
}
