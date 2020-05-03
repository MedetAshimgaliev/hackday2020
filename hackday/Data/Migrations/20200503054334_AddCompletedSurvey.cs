using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hackday.Data.Migrations
{
    public partial class AddCompletedSurvey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "DeletedDate",
            //    table: "Lesson",
            //    nullable: true,
            //    oldClrType: typeof(DateTime),
            //    oldType: "TEXT");

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "DeletedDate",
            //    table: "Course",
            //    nullable: true,
            //    oldClrType: typeof(DateTime),
            //    oldType: "TEXT");

            //migrationBuilder.AddColumn<string>(
            //    name: "ImageUrl",
            //    table: "Course",
            //    nullable: true);

            migrationBuilder.CreateTable(
                name: "CompletedSurveys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SurveyResult = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedSurveys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletedSurveys_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompletedSurveys_UserId",
                table: "CompletedSurveys",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedSurveys");

            //migrationBuilder.DropColumn(
            //    name: "ImageUrl",
            //    table: "Course");

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "DeletedDate",
            //    table: "Lesson",
            //    type: "TEXT",
            //    nullable: false,
            //    oldClrType: typeof(DateTime),
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "DeletedDate",
            //    table: "Course",
            //    type: "TEXT",
            //    nullable: false,
            //    oldClrType: typeof(DateTime),
            //    oldNullable: true);
        }
    }
}
