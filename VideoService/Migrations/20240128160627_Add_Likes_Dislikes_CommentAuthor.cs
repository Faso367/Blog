using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoService.Migrations
{
    /// <inheritdoc />
    public partial class Add_Likes_Dislikes_CommentAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "SubComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DislikesCount",
                table: "SubComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LikesCount",
                table: "SubComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "MainComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DislikesCount",
                table: "MainComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LikesCount",
                table: "MainComments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "SubComments");

            migrationBuilder.DropColumn(
                name: "DislikesCount",
                table: "SubComments");

            migrationBuilder.DropColumn(
                name: "LikesCount",
                table: "SubComments");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "MainComments");

            migrationBuilder.DropColumn(
                name: "DislikesCount",
                table: "MainComments");

            migrationBuilder.DropColumn(
                name: "LikesCount",
                table: "MainComments");
        }
    }
}
