using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LivlReviews.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RenameVinerURLbySourcePage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VinerURL",
                table: "Products",
                newName: "SourcePage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SourcePage",
                table: "Products",
                newName: "VinerURL");
        }
    }
}
