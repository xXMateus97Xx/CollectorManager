using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectorManager.Data.Migrations
{
    public partial class DbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    PasswordSalt = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collection_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollectionAuthor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CollectionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionAuthor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionAuthor_Collection_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollectionFormat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CollectionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionFormat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionFormat_Collection_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollectionItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    FormatId = table.Column<int>(type: "INTEGER", nullable: false),
                    CollectionId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionItem_Collection_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectionItem_CollectionFormat_FormatId",
                        column: x => x.FormatId,
                        principalTable: "CollectionFormat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollectionItemAuthor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    CollectionItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionItemAuthor", x => new { x.Id, x.CollectionItemId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_CollectionItemAuthor_CollectionAuthor_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "CollectionAuthor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectionItemAuthor_CollectionItem_CollectionItemId",
                        column: x => x.CollectionItemId,
                        principalTable: "CollectionItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collection_UserId",
                table: "Collection",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionAuthor_CollectionId",
                table: "CollectionAuthor",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionFormat_CollectionId",
                table: "CollectionFormat",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItem_CollectionId",
                table: "CollectionItem",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItem_FormatId",
                table: "CollectionItem",
                column: "FormatId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItemAuthor_AuthorId",
                table: "CollectionItemAuthor",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItemAuthor_CollectionItemId",
                table: "CollectionItemAuthor",
                column: "CollectionItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectionItemAuthor");

            migrationBuilder.DropTable(
                name: "CollectionAuthor");

            migrationBuilder.DropTable(
                name: "CollectionItem");

            migrationBuilder.DropTable(
                name: "CollectionFormat");

            migrationBuilder.DropTable(
                name: "Collection");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
