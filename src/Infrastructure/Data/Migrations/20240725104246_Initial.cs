using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttachManagement.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    parent_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id);
                    table.ForeignKey(
                        name: "FK_category_category_parent_id",
                        column: x => x.parent_id,
                        principalTable: "category",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "attachment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    file_name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    file_type = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", maxLength: -1, nullable: true),
                    created_date_utc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    modified_date_utc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    category_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attachment", x => x.id);
                    table.ForeignKey(
                        name: "FK_attachment_category_category_id",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_attachment_category_id",
                table: "attachment",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_category_parent_id",
                table: "category",
                column: "parent_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attachment");

            migrationBuilder.DropTable(
                name: "category");
        }
    }
}
