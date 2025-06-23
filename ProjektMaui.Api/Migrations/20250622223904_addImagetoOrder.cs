using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektMaui.Api.Migrations
{
    /// <inheritdoc />
    public partial class addImagetoOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Orders");
        }
    }
}
