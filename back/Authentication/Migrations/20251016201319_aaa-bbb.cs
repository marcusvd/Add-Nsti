using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Authentication.Migrations
{
    /// <inheritdoc />
    public partial class aaabbb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_AC_TimedAccessControls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Start = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    End = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    WorkBreakStart = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    WorkBreakEnd = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AC_TimedAccessControls", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_AU_BusinessesAuth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BusinessProfileId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Deleted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AU_BusinessesAuth", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_AU_CompaniesAuth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TradeName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BusinessAuthId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CNPJ = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AU_CompaniesAuth", x => x.Id);
                    table.ForeignKey(
                        name: "FK__AU_CompaniesAuth__AU_BusinessesAuth_BusinessAuthId",
                        column: x => x.BusinessAuthId,
                        principalTable: "_AU_BusinessesAuth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_Id_Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserProfileId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BusinessAuthId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Code2FaSendEmail = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TimedAccessControlId = table.Column<int>(type: "int", nullable: true),
                    WillExpire = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RefreshToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DisplayUserName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Id_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Id_Users__AC_TimedAccessControls_TimedAccessControlId",
                        column: x => x.TimedAccessControlId,
                        principalTable: "_AC_TimedAccessControls",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Id_Users__AU_BusinessesAuth_BusinessAuthId",
                        column: x => x.BusinessAuthId,
                        principalTable: "_AU_BusinessesAuth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_Id_Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DisplayRole = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyAuthId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Id_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Id_Roles__AU_CompaniesAuth_CompanyAuthId",
                        column: x => x.CompanyAuthId,
                        principalTable: "_AU_CompaniesAuth",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_AU_CompaniesUsersAccounts",
                columns: table => new
                {
                    CompanyAuthId = table.Column<int>(type: "int", nullable: false),
                    UserAccountId = table.Column<int>(type: "int", nullable: false),
                    LinkedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AU_CompaniesUsersAccounts", x => new { x.CompanyAuthId, x.UserAccountId });
                    table.ForeignKey(
                        name: "FK__AU_CompaniesUsersAccounts__AU_CompaniesAuth_CompanyAuthId",
                        column: x => x.CompanyAuthId,
                        principalTable: "_AU_CompaniesAuth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__AU_CompaniesUsersAccounts__Id_Users_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "_Id_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_Id_UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Id_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Id_UserClaims__Id_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "_Id_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_Id_UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Id_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK__Id_UserLogins__Id_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "_Id_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_Id_UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Id_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK__Id_UserTokens__Id_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "_Id_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_Id_RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Id_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Id_RoleClaims__Id_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "_Id_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "_Id_UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Id_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK__Id_UserRoles__Id_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "_Id_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Id_UserRoles__Id_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "_Id_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "_Id_Roles",
                columns: new[] { "Id", "CompanyAuthId", "ConcurrencyStamp", "DisplayRole", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "3a62176a-50dc-4601-8733-d9603097b559", "Acesso Total", "HOLDER", "HOLDER" },
                    { 2, null, "a24d75f8-f195-42d7-87be-73318fb13a2b", "Administrador", "SYSADMIN", "SYSADMIN" },
                    { 3, null, "10b933b3-44f9-4856-95be-2f8954a5c158", "Usuário", "USERS", "USERS" }
                });

            migrationBuilder.CreateIndex(
                name: "IX__AU_CompaniesAuth_BusinessAuthId",
                table: "_AU_CompaniesAuth",
                column: "BusinessAuthId");

            migrationBuilder.CreateIndex(
                name: "IX__AU_CompaniesAuth_CNPJ",
                table: "_AU_CompaniesAuth",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__AU_CompaniesUsersAccounts_UserAccountId",
                table: "_AU_CompaniesUsersAccounts",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX__Id_RoleClaims_RoleId",
                table: "_Id_RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX__Id_Roles_CompanyAuthId",
                table: "_Id_Roles",
                column: "CompanyAuthId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "_Id_Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX__Id_UserClaims_UserId",
                table: "_Id_UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX__Id_UserLogins_UserId",
                table: "_Id_UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX__Id_UserRoles_RoleId",
                table: "_Id_UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "_Id_Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX__Id_Users_BusinessAuthId",
                table: "_Id_Users",
                column: "BusinessAuthId");

            migrationBuilder.CreateIndex(
                name: "IX__Id_Users_TimedAccessControlId",
                table: "_Id_Users",
                column: "TimedAccessControlId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "_Id_Users",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_AU_CompaniesUsersAccounts");

            migrationBuilder.DropTable(
                name: "_Id_RoleClaims");

            migrationBuilder.DropTable(
                name: "_Id_UserClaims");

            migrationBuilder.DropTable(
                name: "_Id_UserLogins");

            migrationBuilder.DropTable(
                name: "_Id_UserRoles");

            migrationBuilder.DropTable(
                name: "_Id_UserTokens");

            migrationBuilder.DropTable(
                name: "_Id_Roles");

            migrationBuilder.DropTable(
                name: "_Id_Users");

            migrationBuilder.DropTable(
                name: "_AU_CompaniesAuth");

            migrationBuilder.DropTable(
                name: "_AC_TimedAccessControls");

            migrationBuilder.DropTable(
                name: "_AU_BusinessesAuth");
        }
    }
}
