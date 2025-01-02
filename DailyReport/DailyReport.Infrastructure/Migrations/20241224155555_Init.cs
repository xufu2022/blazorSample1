using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyReport.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportDailyEmail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoredProcedureName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttachmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    LastProcessedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActiveEndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ActiveEndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ActiveStartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ActiveStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    FreqInterval = table.Column<int>(type: "int", nullable: false),
                    FreqRecurrenceFactor = table.Column<int>(type: "int", nullable: false),
                    FreqSubdayInterval = table.Column<int>(type: "int", nullable: false),
                    FreqSubdayType = table.Column<int>(type: "int", nullable: false),
                    FreqType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportDailyEmail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportDailyMonitor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportDailyEmailId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportDailyMonitor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportDailyMonitor_ReportDailyEmail_ReportDailyEmailId",
                        column: x => x.ReportDailyEmailId,
                        principalTable: "ReportDailyEmail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportDailyMonitor_ReportDailyEmailId",
                table: "ReportDailyMonitor",
                column: "ReportDailyEmailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportDailyMonitor");

            migrationBuilder.DropTable(
                name: "ReportDailyEmail");
        }
    }
}
