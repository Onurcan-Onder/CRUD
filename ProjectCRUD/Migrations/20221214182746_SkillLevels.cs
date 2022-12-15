using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectCRUD.Migrations
{
    /// <inheritdoc />
    public partial class SkillLevels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SkillLevelId",
                table: "Employees",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "SkillLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillLevels", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SkillLevelId",
                table: "Employees",
                column: "SkillLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SkillLevels_SkillLevelId",
                table: "Employees",
                column: "SkillLevelId",
                principalTable: "SkillLevels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SkillLevels_SkillLevelId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "SkillLevels");

            migrationBuilder.DropIndex(
                name: "IX_Employees_SkillLevelId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "SkillLevelId",
                table: "Employees",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
