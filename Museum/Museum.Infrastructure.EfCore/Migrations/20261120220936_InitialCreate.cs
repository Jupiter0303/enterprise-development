using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Museum.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "excursions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    starts_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    duration = table.Column<TimeSpan>(type: "time(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_excursions", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "exhibitions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    theme = table.Column<int>(type: "int", nullable: false),
                    hall_number = table.Column<int>(type: "int", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exhibitions", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "visitors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    first_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    middle_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    phone_number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_visitors", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "excursion_exhibitions",
                columns: table => new
                {
                    excursion_id = table.Column<int>(type: "int", nullable: false),
                    exhibition_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_excursion_exhibitions", x => new { x.excursion_id, x.exhibition_id });
                    table.ForeignKey(
                        name: "FK_excursion_exhibitions_excursions_excursion_id",
                        column: x => x.excursion_id,
                        principalTable: "excursions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_excursion_exhibitions_exhibitions_exhibition_id",
                        column: x => x.exhibition_id,
                        principalTable: "exhibitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    excursion_id = table.Column<int>(type: "int", nullable: false),
                    visitor_id = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.id);
                    table.ForeignKey(
                        name: "FK_tickets_excursions_excursion_id",
                        column: x => x.excursion_id,
                        principalTable: "excursions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tickets_visitors_visitor_id",
                        column: x => x.visitor_id,
                        principalTable: "visitors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "excursions",
                columns: new[] { "id", "duration", "starts_at" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 1, 30, 0, 0), new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 3, 10, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new TimeSpan(0, 1, 45, 0, 0), new DateTime(2024, 5, 20, 11, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new TimeSpan(0, 2, 10, 0, 0), new DateTime(2024, 7, 5, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new TimeSpan(0, 1, 30, 0, 0), new DateTime(2024, 9, 15, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new TimeSpan(0, 2, 50, 0, 0), new DateTime(2024, 11, 10, 15, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new TimeSpan(0, 1, 30, 0, 0), new DateTime(2024, 2, 5, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new TimeSpan(0, 2, 0, 0, 0), new DateTime(2024, 4, 10, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new TimeSpan(0, 1, 45, 0, 0), new DateTime(2024, 6, 20, 11, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new TimeSpan(0, 2, 15, 0, 0), new DateTime(2024, 8, 5, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, new TimeSpan(0, 1, 30, 0, 0), new DateTime(2024, 10, 15, 13, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "exhibitions",
                columns: new[] { "id", "end_date", "hall_number", "name", "start_date", "theme" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 2, 10), 101, "Импрессионисты", new DateOnly(2024, 1, 10), 0 },
                    { 2, new DateOnly(2024, 4, 5), 209, "Древний Египет", new DateOnly(2024, 3, 5), 1 },
                    { 3, new DateOnly(2024, 6, 15), 212, "Выставка динозавров", new DateOnly(2024, 5, 15), 2 },
                    { 4, new DateOnly(2024, 8, 1), 110, "Фауна Северной Америки", new DateOnly(2024, 7, 1), 3 },
                    { 5, new DateOnly(2024, 10, 10), 103, "Русский авангард", new DateOnly(2024, 9, 10), 0 },
                    { 6, new DateOnly(2024, 12, 20), 206, "Римская империя", new DateOnly(2024, 11, 20), 1 },
                    { 7, new DateOnly(2024, 2, 25), 211, "Космос", new DateOnly(2024, 1, 25), 2 },
                    { 8, new DateOnly(2024, 4, 15), 108, "Тропические леса", new DateOnly(2024, 3, 15), 3 },
                    { 9, new DateOnly(2024, 6, 5), 103, "Выставка архитектуры", new DateOnly(2024, 5, 5), 0 },
                    { 10, new DateOnly(2024, 8, 20), 200, "Выставка древних цивилизаций", new DateOnly(2024, 7, 20), 1 }
                });

            migrationBuilder.InsertData(
                table: "visitors",
                columns: new[] { "id", "birthday", "first_name", "last_name", "middle_name", "phone_number" },
                values: new object[,]
                {
                    { 1, new DateOnly(1990, 5, 12), "Иван", "Петров", null, "+79001112233" },
                    { 2, new DateOnly(1985, 3, 21), "Мария", "Сидорова", null, null },
                    { 3, new DateOnly(2000, 7, 15), "Алексей", "Иванов", null, "+79005556677" },
                    { 4, new DateOnly(1995, 11, 30), "Екатерина", "Смирнова", null, null },
                    { 5, new DateOnly(1988, 2, 5), "Дмитрий", "Кузнецов", null, "+79008889900" },
                    { 6, new DateOnly(1992, 9, 18), "Ольга", "Попова", null, null },
                    { 7, new DateOnly(1998, 6, 10), "Сергей", "Васильев", null, "+79002223344" },
                    { 8, new DateOnly(1993, 4, 25), "Анастасия", "Морозова", null, null },
                    { 9, new DateOnly(1987, 12, 1), "Николай", "Новиков", null, "+79003334455" },
                    { 10, new DateOnly(1991, 8, 14), "Татьяна", "Федорова", null, null },
                    { 11, new DateOnly(1989, 10, 3), "Владимир", "Соколов", null, "+79004445566" },
                    { 12, new DateOnly(1994, 1, 22), "Елена", "Козлова", null, null }
                });

            migrationBuilder.InsertData(
                table: "excursion_exhibitions",
                columns: new[] { "excursion_id", "exhibition_id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 4 },
                    { 1, 5 },
                    { 2, 2 },
                    { 2, 5 },
                    { 2, 6 },
                    { 3, 3 },
                    { 3, 5 },
                    { 3, 9 },
                    { 4, 1 },
                    { 4, 9 },
                    { 4, 10 },
                    { 5, 1 },
                    { 5, 2 },
                    { 5, 6 },
                    { 6, 1 },
                    { 6, 7 },
                    { 7, 1 },
                    { 7, 2 },
                    { 7, 7 },
                    { 8, 5 },
                    { 8, 7 },
                    { 9, 1 },
                    { 10, 1 },
                    { 10, 2 },
                    { 10, 9 },
                    { 11, 5 }
                });

            migrationBuilder.InsertData(
                table: "tickets",
                columns: new[] { "id", "excursion_id", "price", "type", "visitor_id" },
                values: new object[,]
                {
                    { 1, 1, 500m, 0, 1 },
                    { 2, 2, 300m, 1, 2 },
                    { 3, 5, 600m, 0, 3 },
                    { 4, 7, 400m, 1, 4 },
                    { 5, 8, 550m, 0, 5 },
                    { 6, 1, 350m, 1, 6 },
                    { 7, 2, 500m, 0, 7 },
                    { 8, 3, 300m, 1, 8 },
                    { 9, 4, 600m, 0, 9 },
                    { 10, 8, 400m, 1, 10 },
                    { 11, 6, 550m, 0, 11 },
                    { 12, 10, 350m, 1, 12 },
                    { 13, 11, 500m, 0, 1 },
                    { 14, 11, 300m, 1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_excursion_exhibitions_exhibition_id",
                table: "excursion_exhibitions",
                column: "exhibition_id");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_excursion_id",
                table: "tickets",
                column: "excursion_id");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_visitor_id",
                table: "tickets",
                column: "visitor_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "excursion_exhibitions");

            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "exhibitions");

            migrationBuilder.DropTable(
                name: "excursions");

            migrationBuilder.DropTable(
                name: "visitors");
        }
    }
}
