using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoService.Migrations
{
    /// <inheritdoc />
    public partial class authorreactionkamikadze : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubCommentId",
                table: "AuthorReactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthorReactions_SubCommentId",
                table: "AuthorReactions",
                column: "SubCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorReactions_SubComments_SubCommentId",
                table: "AuthorReactions",
                column: "SubCommentId",
                principalTable: "SubComments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorReactions_SubComments_SubCommentId",
                table: "AuthorReactions");

            migrationBuilder.DropIndex(
                name: "IX_AuthorReactions_SubCommentId",
                table: "AuthorReactions");

            migrationBuilder.DropColumn(
                name: "SubCommentId",
                table: "AuthorReactions");
        }
    }
}
