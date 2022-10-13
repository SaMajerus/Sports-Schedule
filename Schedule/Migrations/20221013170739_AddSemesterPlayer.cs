using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Schedule.Migrations
{
    public partial class AddSemesterPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Sports_SportId",
                table: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_Semesters_SportId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "SportId",
                table: "Semesters");

            migrationBuilder.CreateTable(
                name: "SemesterPlayer",
                columns: table => new
                {
                    SemesterPlayerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SemesterId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterPlayer", x => x.SemesterPlayerId);
                    table.ForeignKey(
                        name: "FK_SemesterPlayer_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterPlayer_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "SemesterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SemesterPlayer_PlayerId",
                table: "SemesterPlayer",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterPlayer_SemesterId",
                table: "SemesterPlayer",
                column: "SemesterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SemesterPlayer");

            migrationBuilder.AddColumn<int>(
                name: "SportId",
                table: "Semesters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_SportId",
                table: "Semesters",
                column: "SportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Sports_SportId",
                table: "Semesters",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "SportId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
