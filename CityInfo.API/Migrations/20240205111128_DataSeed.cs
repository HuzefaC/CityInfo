using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityInfo.API.Migrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "In England", "London" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "In Spain", "Madrid" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "In Germany", "Munich" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 4, "In France", "Paris" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 5, "In Italy", "Milan" });

            migrationBuilder.InsertData(
                table: "PointOfInterests",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 1, 1, "A treasure trove of art and antiquities from around the world.", "The British Museum" });

            migrationBuilder.InsertData(
                table: "PointOfInterests",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 2, 1, "The official residence of the British monarch.", "Buckingham Palace" });

            migrationBuilder.InsertData(
                table: "PointOfInterests",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 3, 2, "One of the world's greatest art galleries, featuring European art from the 12th to early 20th century.", "Prado Museum" });

            migrationBuilder.InsertData(
                table: "PointOfInterests",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 4, 2, "Home to Real Madrid, one of the world's most famous football clubs.", "Santiago Bernabéu Stadium" });

            migrationBuilder.InsertData(
                table: "PointOfInterests",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 5, 3, "A fairy-tale castle in the Bavarian Alps.", "Neuschwanstein Castle" });

            migrationBuilder.InsertData(
                table: "PointOfInterests",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 6, 3, "The world's largest beer festival, held annually.", "Oktoberfest" });

            migrationBuilder.InsertData(
                table: "PointOfInterests",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 7, 4, "The iconic iron tower, a symbol of Paris.", "Eiffel Tower" });

            migrationBuilder.InsertData(
                table: "PointOfInterests",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 8, 4, "A masterpiece of French Gothic architecture.", "Notre-Dame Cathedral" });

            migrationBuilder.InsertData(
                table: "PointOfInterests",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 9, 5, "One of the largest cathedrals in the world, known for its intricate Gothic architecture.", "Duomo di Milano" });

            migrationBuilder.InsertData(
                table: "PointOfInterests",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 10, 5, "One of the world's most famous opera houses.", "La Scala Opera House" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
