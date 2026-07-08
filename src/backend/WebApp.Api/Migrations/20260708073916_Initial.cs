using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Endpoint = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Difficulty = table.Column<int>(type: "integer", nullable: false),
                    IsImplemented = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExecutionResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UtilityId = table.Column<int>(type: "integer", nullable: false),
                    Input = table.Column<string>(type: "text", nullable: false),
                    Output = table.Column<string>(type: "text", nullable: false),
                    ExecutedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutionResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExecutionResults_Utilities_UtilityId",
                        column: x => x.UtilityId,
                        principalTable: "Utilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Utilities",
                columns: new[] { "Id", "Category", "Description", "Difficulty", "Endpoint", "IsImplemented", "Name" },
                values: new object[,]
                {
                    { 1, "Числа", "Суммирует числа, переданные по одному на строку.", 1, "sum-numbers", true, "Сумма чисел" },
                    { 2, "Текст", "Заменяет несколько подстрок одновременно по словарю замен.", 2, "multi-replace", false, "Множественная замена в тексте" },
                    { 3, "Данные", "Преобразует CSV-данные в SQL-скрипт для создания временной таблицы и вставки строк.", 2, "csv-to-sql", false, "CSV в SQL INSERT" },
                    { 4, "Код", "Преобразует строки (по одной на линию) в переменную-список на C# или JavaScript.", 1, "text-to-list", false, "Текст в список C# / JS" },
                    { 5, "Безопасность", "Генерирует надёжный пароль заданной длины с выбором наборов символов.", 1, "password-gen", false, "Генератор паролей" },
                    { 6, "Текст", "Считает символы, слова, строки и предложения в тексте.", 1, "text-stats", false, "Подсчёт символов / слов" },
                    { 7, "Текст", "Преобразует текст в UPPER, lower, Title Case, camelCase, snake_case.", 2, "case-converter", false, "Конвертер регистров" },
                    { 8, "Кодирование", "Кодирует и декодирует строки в формат Base64.", 1, "base64", false, "Base64 кодер / декодер" },
                    { 9, "Данные", "Форматирует (pretty-print) или минифицирует JSON-строку.", 2, "json-formatter", false, "JSON-форматировщик" },
                    { 10, "Безопасность", "Вычисляет MD5 / SHA1 / SHA256 хеш переданной строки.", 1, "hash-calc", false, "Хеш-калькулятор" },
                    { 11, "Код", "Генерирует один или несколько UUID и копирует в буфер обмена.", 1, "uuid-gen", false, "Генератор UUID / GUID" },
                    { 12, "Текст", "Сортирует строки по алфавиту, длине, в обратном порядке, с удалением дубликатов.", 1, "string-sort", false, "Сортировщик строк" },
                    { 13, "Дата и время", "Вычисляет разницу между датами, добавляет дни / месяцы / годы к дате.", 2, "date-calc", false, "Калькулятор дат" },
                    { 14, "Числа", "Переводит числа между DEC, HEX, BIN, OCT с произвольной точностью.", 2, "number-base", false, "Конвертер чисел (системы счисления)" },
                    { 15, "Кодирование", "Экранирует / деэкранирует строки для HTML, JSON, SQL, URL.", 2, "string-escape", false, "Экранирование строк" },
                    { 16, "Текст", "Генерирует заданное число абзацев / слов / символов текста-рыбы.", 1, "lorem-ipsum", false, "Генератор Lorem Ipsum" },
                    { 17, "Данные", "Проверяет корректность структуры и форматирует с отступами.", 3, "yaml-json", false, "Валидатор и форматировщик JSON / YAML" },
                    { 18, "Данные", "Конвертирует CSV в массив JSON-объектов и обратно.", 2, "csv-json", false, "Преобразователь CSV ↔ JSON" },
                    { 19, "Код", "Помогает составить и протестировать регулярное выражение на тестовых строках.", 3, "regex-tester", false, "Построитель регулярных выражений" },
                    { 20, "Дата и время", "Конвертирует Unix timestamp в читаемую дату и обратно.", 1, "unix-time", false, "Unix Timestamp конвертер" },
                    { 21, "Безопасность", "Декодирует JWT-токен, показывает Header/Payload в читаемом виде, подсвечивает истекшие токены, генерирует тестовые JWT.", 2, "jwt-debugger", false, "JWT Decoder & Debugger" },
                    { 22, "Текст", "Показывает ASCII/Unicode-коды каждого символа текста и собирает строку из кодов обратно. Помогает найти невидимые символы.", 1, "char-inspector", false, "Инспектор символов (ASCII/Unicode)" },
                    { 23, "Кодирование", "Кодирует/декодирует URL-строки, парсит query-параметры в таблицу ключ-значение с авто-декодированием и генерирует JSON-объект.", 2, "url-tools", false, "URL Encoder / Decoder + Query Parser" },
                    { 24, "Дизайн", "Конвертирует цвета между HEX, RGB, HSL, CMYK. Определяет контрастные пары, проверяет на доступность (color-blindness).", 2, "color-converter", false, "Конвертер цветов" },
                    { 25, "Код", "Форматирует SQL-запросы с отступами (beautify) или сжимает в одну строку (minify).", 2, "sql-formatter", false, "SQL Formatter / Minifier" },
                    { 26, "Текст", "Сравнивает два блока текста и подсвечивает добавленные, удалённые и изменённые строки.", 2, "text-diff", false, "Текстовый Diff-инструмент" },
                    { 27, "Числа", "Вычисляет: «X это Y% от чего?», «На сколько % изменилось от A до B?», пропорциональное масштабирование.", 1, "percent-calc", false, "Калькулятор пропорций и процентов" },
                    { 28, "Текст", "Шифрование текста", 2, "cipher-text", true, "Шифрование текста" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExecutionResults_UtilityId",
                table: "ExecutionResults",
                column: "UtilityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExecutionResults");

            migrationBuilder.DropTable(
                name: "Utilities");
        }
    }
}
