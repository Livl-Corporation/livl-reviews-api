using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LivlReviews.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddMessagesInRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminMessage",
                table: "Requests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserMessage",
                table: "Requests",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminMessage",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "UserMessage",
                table: "Requests");
        }
    }
}
