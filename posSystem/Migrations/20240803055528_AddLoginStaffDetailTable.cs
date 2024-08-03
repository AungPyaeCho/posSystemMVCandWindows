using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace posSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddLoginStaffDetailTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "tblLoginStaffDetail",
                columns: table => new
                {
                    ldId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    staffId = table.Column<int>(type: "int", nullable: true),
                    staffName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    staffRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sessionExpired = table.Column<DateTime>(type: "datetime2", nullable: true),
                    loginAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    logOutAt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLoginStaffDetail", x => x.ldId);
                    table.ForeignKey(
                        name: "FK_tblLoginStaffDetail_tblStaff_staffId",
                        column: x => x.staffId,
                        principalTable: "tblStaff",
                        principalColumn: "staffId");
                });

           

            migrationBuilder.CreateIndex(
                name: "IX_tblLoginStaffDetail_staffId",
                table: "tblLoginStaffDetail",
                column: "staffId");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
