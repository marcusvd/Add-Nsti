using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Migrations
{
    /// <inheritdoc />
    public partial class many_to_many3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyUserAccount_CompanyAuth_CompanyAuthId",
                table: "CompanyUserAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyUserAccount_Users_UserAccountId",
                table: "CompanyUserAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyUserAccount",
                table: "CompanyUserAccount");

            migrationBuilder.RenameTable(
                name: "CompanyUserAccount",
                newName: "CompaniesUsersAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyUserAccount_UserAccountId",
                table: "CompaniesUsersAccounts",
                newName: "IX_CompaniesUsersAccounts_UserAccountId");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Email",
                keyValue: null,
                column: "Email",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "varchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompaniesUsersAccounts",
                table: "CompaniesUsersAccounts",
                columns: new[] { "CompanyAuthId", "UserAccountId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CompaniesUsersAccounts_CompanyAuth_CompanyAuthId",
                table: "CompaniesUsersAccounts",
                column: "CompanyAuthId",
                principalTable: "CompanyAuth",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompaniesUsersAccounts_Users_UserAccountId",
                table: "CompaniesUsersAccounts",
                column: "UserAccountId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompaniesUsersAccounts_CompanyAuth_CompanyAuthId",
                table: "CompaniesUsersAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CompaniesUsersAccounts_Users_UserAccountId",
                table: "CompaniesUsersAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompaniesUsersAccounts",
                table: "CompaniesUsersAccounts");

            migrationBuilder.RenameTable(
                name: "CompaniesUsersAccounts",
                newName: "CompanyUserAccount");

            migrationBuilder.RenameIndex(
                name: "IX_CompaniesUsersAccounts_UserAccountId",
                table: "CompanyUserAccount",
                newName: "IX_CompanyUserAccount_UserAccountId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "varchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldMaxLength: 256)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyUserAccount",
                table: "CompanyUserAccount",
                columns: new[] { "CompanyAuthId", "UserAccountId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyUserAccount_CompanyAuth_CompanyAuthId",
                table: "CompanyUserAccount",
                column: "CompanyAuthId",
                principalTable: "CompanyAuth",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyUserAccount_Users_UserAccountId",
                table: "CompanyUserAccount",
                column: "UserAccountId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
