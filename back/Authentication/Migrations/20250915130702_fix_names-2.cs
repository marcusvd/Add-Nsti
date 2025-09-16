using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Migrations
{
    /// <inheritdoc />
    public partial class fix_names2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__AU_CompaniesUsersAccounts_AspNetUsers_UserAccountId",
                table: "_AU_CompaniesUsersAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers__AC_TimedAccessControls_TimedAccessControlId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers__AU_BusinessesAuth_BusinessAuthId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "_Id_UserTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "_Id_Users");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "_Id_UserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "_Id_UserLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "_Id_UserClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "_Id_Roles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "_Id_RoleClaims");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_TimedAccessControlId",
                table: "_Id_Users",
                newName: "IX__Id_Users_TimedAccessControlId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_BusinessAuthId",
                table: "_Id_Users",
                newName: "IX__Id_Users_BusinessAuthId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "_Id_UserRoles",
                newName: "IX__Id_UserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "_Id_UserLogins",
                newName: "IX__Id_UserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "_Id_UserClaims",
                newName: "IX__Id_UserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "_Id_RoleClaims",
                newName: "IX__Id_RoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Id_UserTokens",
                table: "_Id_UserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK__Id_Users",
                table: "_Id_Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Id_UserRoles",
                table: "_Id_UserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK__Id_UserLogins",
                table: "_Id_UserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK__Id_UserClaims",
                table: "_Id_UserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Id_Roles",
                table: "_Id_Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Id_RoleClaims",
                table: "_Id_RoleClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__AU_CompaniesUsersAccounts__Id_Users_UserAccountId",
                table: "_AU_CompaniesUsersAccounts",
                column: "UserAccountId",
                principalTable: "_Id_Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Id_RoleClaims__Id_Roles_RoleId",
                table: "_Id_RoleClaims",
                column: "RoleId",
                principalTable: "_Id_Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Id_UserClaims__Id_Users_UserId",
                table: "_Id_UserClaims",
                column: "UserId",
                principalTable: "_Id_Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Id_UserLogins__Id_Users_UserId",
                table: "_Id_UserLogins",
                column: "UserId",
                principalTable: "_Id_Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Id_UserRoles__Id_Roles_RoleId",
                table: "_Id_UserRoles",
                column: "RoleId",
                principalTable: "_Id_Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Id_UserRoles__Id_Users_UserId",
                table: "_Id_UserRoles",
                column: "UserId",
                principalTable: "_Id_Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Id_Users__AC_TimedAccessControls_TimedAccessControlId",
                table: "_Id_Users",
                column: "TimedAccessControlId",
                principalTable: "_AC_TimedAccessControls",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Id_Users__AU_BusinessesAuth_BusinessAuthId",
                table: "_Id_Users",
                column: "BusinessAuthId",
                principalTable: "_AU_BusinessesAuth",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Id_UserTokens__Id_Users_UserId",
                table: "_Id_UserTokens",
                column: "UserId",
                principalTable: "_Id_Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__AU_CompaniesUsersAccounts__Id_Users_UserAccountId",
                table: "_AU_CompaniesUsersAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK__Id_RoleClaims__Id_Roles_RoleId",
                table: "_Id_RoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK__Id_UserClaims__Id_Users_UserId",
                table: "_Id_UserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK__Id_UserLogins__Id_Users_UserId",
                table: "_Id_UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK__Id_UserRoles__Id_Roles_RoleId",
                table: "_Id_UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK__Id_UserRoles__Id_Users_UserId",
                table: "_Id_UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK__Id_Users__AC_TimedAccessControls_TimedAccessControlId",
                table: "_Id_Users");

            migrationBuilder.DropForeignKey(
                name: "FK__Id_Users__AU_BusinessesAuth_BusinessAuthId",
                table: "_Id_Users");

            migrationBuilder.DropForeignKey(
                name: "FK__Id_UserTokens__Id_Users_UserId",
                table: "_Id_UserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Id_UserTokens",
                table: "_Id_UserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Id_Users",
                table: "_Id_Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Id_UserRoles",
                table: "_Id_UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Id_UserLogins",
                table: "_Id_UserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Id_UserClaims",
                table: "_Id_UserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Id_Roles",
                table: "_Id_Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Id_RoleClaims",
                table: "_Id_RoleClaims");

            migrationBuilder.RenameTable(
                name: "_Id_UserTokens",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "_Id_Users",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "_Id_UserRoles",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "_Id_UserLogins",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "_Id_UserClaims",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "_Id_Roles",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "_Id_RoleClaims",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameIndex(
                name: "IX__Id_Users_TimedAccessControlId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_TimedAccessControlId");

            migrationBuilder.RenameIndex(
                name: "IX__Id_Users_BusinessAuthId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_BusinessAuthId");

            migrationBuilder.RenameIndex(
                name: "IX__Id_UserRoles_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX__Id_UserLogins_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX__Id_UserClaims_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX__Id_RoleClaims_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__AU_CompaniesUsersAccounts_AspNetUsers_UserAccountId",
                table: "_AU_CompaniesUsersAccounts",
                column: "UserAccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers__AC_TimedAccessControls_TimedAccessControlId",
                table: "AspNetUsers",
                column: "TimedAccessControlId",
                principalTable: "_AC_TimedAccessControls",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers__AU_BusinessesAuth_BusinessAuthId",
                table: "AspNetUsers",
                column: "BusinessAuthId",
                principalTable: "_AU_BusinessesAuth",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
