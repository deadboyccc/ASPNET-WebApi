using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BGwalks.API.Migrations
{
    /// <inheritdoc />
    public partial class imagesTableDBset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageExtention = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
