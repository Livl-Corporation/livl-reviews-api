using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LivlReviews.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UseIUserAndIdentityUserInsteadOfDomainUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_User_AdminId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_User_UserId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_User_AdminId",
                table: "Stocks");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_AdminId",
                table: "Requests",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_UserId",
                table: "Requests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_AspNetUsers_AdminId",
                table: "Stocks",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_AdminId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_UserId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_AspNetUsers_AdminId",
                table: "Stocks");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    InvitedById = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    isConfirmed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_User_InvitedById",
                        column: x => x.InvitedById,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_InvitedById",
                table: "User",
                column: "InvitedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_User_AdminId",
                table: "Requests",
                column: "AdminId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_User_UserId",
                table: "Requests",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_User_AdminId",
                table: "Stocks",
                column: "AdminId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
