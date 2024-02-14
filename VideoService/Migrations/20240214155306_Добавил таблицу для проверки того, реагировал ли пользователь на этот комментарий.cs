using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoService.Migrations
{
    /// <inheritdoc />
    public partial class Добавилтаблицудляпроверкитогореагироваллипользовательнаэтоткомментарий : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorReactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReactionAuthor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LikeReaction = table.Column<bool>(type: "bit", nullable: false),
                    DislikeReaction = table.Column<bool>(type: "bit", nullable: false),
                    MainCommentId = table.Column<int>(type: "int", nullable: true),
                    SubCommentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorReactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthorReactions_MainComments_MainCommentId",
                        column: x => x.MainCommentId,
                        principalTable: "MainComments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AuthorReactions_SubComments_SubCommentId",
                        column: x => x.SubCommentId,
                        principalTable: "SubComments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorReactions_MainCommentId",
                table: "AuthorReactions",
                column: "MainCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorReactions_SubCommentId",
                table: "AuthorReactions",
                column: "SubCommentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorReactions");
        }
    }
}
