using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Livraria.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Autores",
                columns: new[] { "Id", "Active", "DataAtualizacao", "DataCriacao", "Nome" },
                values: new object[,]
                {
                    { new Guid("0c1fa73a-91e9-456b-91ae-90e03c83d841"), true, null, new DateTime(2025, 6, 9, 20, 34, 12, 329, DateTimeKind.Utc).AddTicks(2121), "George Orwell" },
                    { new Guid("14a3de89-71ea-47dc-bfd2-2139de3d2fd0"), true, null, new DateTime(2025, 6, 9, 20, 34, 12, 329, DateTimeKind.Utc).AddTicks(2133), "J.K. Rowling" },
                    { new Guid("845b1ad1-9d84-4e56-bc1e-94eb55f71656"), true, null, new DateTime(2025, 6, 9, 20, 34, 12, 329, DateTimeKind.Utc).AddTicks(2136), "J.R.R. Tolkien" }
                });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "Active", "DataAtualizacao", "DataCriacao", "Nome" },
                values: new object[,]
                {
                    { new Guid("2cb8b46e-6ad6-4b5e-9104-fbfe25b7d836"), true, null, new DateTime(2025, 6, 9, 20, 34, 12, 329, DateTimeKind.Utc).AddTicks(2232), "Fantasia" },
                    { new Guid("4fbd0572-d395-4dfb-ae7c-4e73887e1869"), true, null, new DateTime(2025, 6, 9, 20, 34, 12, 329, DateTimeKind.Utc).AddTicks(2229), "Ficção Científica" },
                    { new Guid("e77ebac7-9af4-4e9f-8e57-5367fefadcd2"), true, null, new DateTime(2025, 6, 9, 20, 34, 12, 329, DateTimeKind.Utc).AddTicks(2234), "Distopia" }
                });

            migrationBuilder.InsertData(
                table: "Livros",
                columns: new[] { "Id", "Active", "AutorId", "DataAtualizacao", "DataCriacao", "Descricao", "GeneroId", "Titulo" },
                values: new object[,]
                {
                    { new Guid("2f8ea68e-f3d9-4d4a-9e87-ecab72a3fdf2"), true, new Guid("14a3de89-71ea-47dc-bfd2-2139de3d2fd0"), null, new DateTime(2025, 6, 9, 20, 34, 12, 329, DateTimeKind.Utc).AddTicks(2271), "Início da saga mágica", new Guid("2cb8b46e-6ad6-4b5e-9104-fbfe25b7d836"), "Harry Potter e a Pedra Filosofal" },
                    { new Guid("a4fd96be-7607-41f0-a2c6-b30b338efcec"), true, new Guid("845b1ad1-9d84-4e56-bc1e-94eb55f71656"), null, new DateTime(2025, 6, 9, 20, 34, 12, 329, DateTimeKind.Utc).AddTicks(2274), "Uma jornada épica pela Terra Média", new Guid("2cb8b46e-6ad6-4b5e-9104-fbfe25b7d836"), "O Senhor dos Anéis" },
                    { new Guid("ffb6cf11-9b86-4b86-8f23-d00ff6d04fd4"), true, new Guid("0c1fa73a-91e9-456b-91ae-90e03c83d841"), null, new DateTime(2025, 6, 9, 20, 34, 12, 329, DateTimeKind.Utc).AddTicks(2257), "Uma distopia totalitária", new Guid("e77ebac7-9af4-4e9f-8e57-5367fefadcd2"), "1984" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("4fbd0572-d395-4dfb-ae7c-4e73887e1869"));

            migrationBuilder.DeleteData(
                table: "Livros",
                keyColumn: "Id",
                keyValue: new Guid("2f8ea68e-f3d9-4d4a-9e87-ecab72a3fdf2"));

            migrationBuilder.DeleteData(
                table: "Livros",
                keyColumn: "Id",
                keyValue: new Guid("a4fd96be-7607-41f0-a2c6-b30b338efcec"));

            migrationBuilder.DeleteData(
                table: "Livros",
                keyColumn: "Id",
                keyValue: new Guid("ffb6cf11-9b86-4b86-8f23-d00ff6d04fd4"));

            migrationBuilder.DeleteData(
                table: "Autores",
                keyColumn: "Id",
                keyValue: new Guid("0c1fa73a-91e9-456b-91ae-90e03c83d841"));

            migrationBuilder.DeleteData(
                table: "Autores",
                keyColumn: "Id",
                keyValue: new Guid("14a3de89-71ea-47dc-bfd2-2139de3d2fd0"));

            migrationBuilder.DeleteData(
                table: "Autores",
                keyColumn: "Id",
                keyValue: new Guid("845b1ad1-9d84-4e56-bc1e-94eb55f71656"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("2cb8b46e-6ad6-4b5e-9104-fbfe25b7d836"));

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: new Guid("e77ebac7-9af4-4e9f-8e57-5367fefadcd2"));
        }
    }
}
