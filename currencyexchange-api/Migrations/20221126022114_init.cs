using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace currencyexchangeapi.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "api_users",
                columns: table => new
                {
                    apikey = table.Column<string>(name: "api_key", type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_api_users", x => x.apikey);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "api_users");
        }
    }
}
