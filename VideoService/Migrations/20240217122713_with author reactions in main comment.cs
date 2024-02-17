using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoService.Migrations
{
    /// <inheritdoc />
    public partial class withauthorreactionsinmaincomment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorReactions_MainComments_MainCommentId",
                table: "AuthorReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorReactions_SubComments_SubCommentId",
                table: "AuthorReactions");

            migrationBuilder.DropIndex(
                name: "IX_AuthorReactions_SubCommentId",
                table: "AuthorReactions");

            migrationBuilder.DropColumn(
                name: "SubCommentId",
                table: "AuthorReactions");

            migrationBuilder.AlterColumn<int>(
                name: "MainCommentId",
                table: "AuthorReactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorReactions_MainComments_MainCommentId",
                table: "AuthorReactions",
                column: "MainCommentId",
                principalTable: "MainComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorReactions_MainComments_MainCommentId",
                table: "AuthorReactions");

            migrationBuilder.AlterColumn<int>(
                name: "MainCommentId",
                table: "AuthorReactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
                name: "FK_AuthorReactions_MainComments_MainCommentId",
                table: "AuthorReactions",
                column: "MainCommentId",
                principalTable: "MainComments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorReactions_SubComments_SubCommentId",
                table: "AuthorReactions",
                column: "SubCommentId",
                principalTable: "SubComments",
                principalColumn: "Id");
        }
    }
}
