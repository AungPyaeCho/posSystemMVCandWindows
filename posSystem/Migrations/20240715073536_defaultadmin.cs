using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace posSystem.Migrations
{
    /// <inheritdoc />
    public partial class defaultadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tblAdmin",
                columns: new[] { "id", "adminCreateAt", "adminEmail", "adminName", "adminPassword" },
                values: new object[] { "4dc7d508-b960-4edf-9103-b0ba83af4bbb", "7/15/2024 2:05:35 PM", "admin@pos.com", "Default Admin", "QWRtaW5AMTIz" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tblAdmin",
                keyColumn: "id",
                keyValue: "4dc7d508-b960-4edf-9103-b0ba83af4bbb");
        }
    }
}
