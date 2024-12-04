using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSList.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_belonging",
                columns: table => new
                {
                    game_id = table.Column<int>(type: "INTEGER", nullable: false),
                    list_id = table.Column<int>(type: "INTEGER", nullable: false),
                    position = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_belonging", x => new { x.list_id, x.game_id });
                });

            migrationBuilder.CreateTable(
                name: "tb_game",
                columns: table => new
                {
                    GameId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    game_year = table.Column<int>(type: "INTEGER", nullable: true),
                    genre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    platforms = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    score = table.Column<double>(type: "REAL", nullable: true),
                    img_url = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    short_description = table.Column<string>(type: "TEXT", nullable: true),
                    long_description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_game", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "tb_game_list",
                columns: table => new
                {
                    ListId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_game_list", x => x.ListId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_belonging");

            migrationBuilder.DropTable(
                name: "tb_game");

            migrationBuilder.DropTable(
                name: "tb_game_list");
        }
    }
}
