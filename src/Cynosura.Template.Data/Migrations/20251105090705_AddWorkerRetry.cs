using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cynosura.Template.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkerRetry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NeedRetry",
                table: "WorkerRuns",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TriesLeft",
                table: "WorkerRuns",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RetryCount",
                table: "WorkerInfos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "RetryInterval",
                table: "WorkerInfos",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NeedRetry",
                table: "WorkerRuns");

            migrationBuilder.DropColumn(
                name: "TriesLeft",
                table: "WorkerRuns");

            migrationBuilder.DropColumn(
                name: "RetryCount",
                table: "WorkerInfos");

            migrationBuilder.DropColumn(
                name: "RetryInterval",
                table: "WorkerInfos");
        }
    }
}
