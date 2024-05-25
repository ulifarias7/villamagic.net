using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Magic.curso.net.Migrations
{
    /// <inheritdoc />
    public partial class AlimentartableVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacion",
                table: "villas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "villas",
                columns: new[] { "ID", "Amenidad", "Detalle", "FechaActualizacion", "Fechacreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupante", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "detalle de la villa ", new DateTime(2024, 5, 23, 21, 0, 26, 659, DateTimeKind.Local).AddTicks(1182), new DateTime(2024, 5, 23, 21, 0, 26, 659, DateTimeKind.Local).AddTicks(1166), "", 50, "villa real", 5, 200.0 },
                    { 2, "", "detalle de la villa con vista a la piscina", new DateTime(2024, 5, 23, 21, 0, 26, 659, DateTimeKind.Local).AddTicks(1187), new DateTime(2024, 5, 23, 21, 0, 26, 659, DateTimeKind.Local).AddTicks(1186), "", 40, "villa premiun vista a la piscina", 4, 250.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "FechaActualizacion",
                table: "villas");
        }
    }
}
