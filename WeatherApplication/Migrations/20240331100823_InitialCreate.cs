using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherApplication.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weathers",
                columns: table => new
                {
                    Data = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    T = table.Column<int>(type: "int", nullable: false),
                    Humidity = table.Column<double>(type: "float", nullable: false),
                    Td = table.Column<double>(type: "float", nullable: false),
                    Pressure = table.Column<int>(type: "int", nullable: false),
                    Direction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Velocity = table.Column<int>(type: "int", nullable: false),
                    Cloudy = table.Column<int>(type: "int", nullable: false),
                    h = table.Column<int>(type: "int", nullable: false),
                    VV = table.Column<int>(type: "int", nullable: false),
                    Phenomenon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weathers", x => x.Data);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weathers");
        }
    }
}
