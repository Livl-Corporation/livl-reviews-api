using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LivlReviews.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReceivedAtToReviewableAtInReviewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceivedAt",
                table: "Requests",
                newName: "ReviewableAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReviewableAt",
                table: "Requests",
                newName: "ReceivedAt");
        }
    }
}
