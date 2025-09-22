using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AntimicrobialPrescriptions.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "Id", "AntimicrobialName", "Dose", "ExpectedEndDate", "Frequency", "Indication", "PatientId", "PrescriberName", "PrescriberRole", "Route", "StartDate", "Status" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Amoxicillin", "500mg", new DateTime(2025, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "3x/day", "Pneumonia", "123", "Dr. Smith", "Clinician", "oral", new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Ceftriaxone", "1g", new DateTime(2025, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "2x/day", "UTI", "456", "Dr. Jones", "Clinician", "IV", new DateTime(2025, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Vancomycin", "500mg", new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "2x/day", "Sepsis", "789", "Dr. Lee", "InfectionControl", "IV", new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));
        }
    }
}
