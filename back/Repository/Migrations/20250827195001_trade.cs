using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class trade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MN_businesses_profiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BusinessAuthId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Deleted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MN_businesses_profiles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SD_Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ZipCode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Street = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    District = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    State = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Complement = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Deleted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SD_Addresses", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SD_Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Site = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cel = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Zap = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Landline = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Deleted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SD_Contacts", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MN_Companies_profiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompanyAuthId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    ContactId = table.Column<int>(type: "int", nullable: true),
                    BusinessProfileId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MN_Companies_profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MN_Companies_profiles_MN_businesses_profiles_BusinessProfile~",
                        column: x => x.BusinessProfileId,
                        principalTable: "MN_businesses_profiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MN_Companies_profiles_SD_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "SD_Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MN_Companies_profiles_SD_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "SD_Contacts",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MN_Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TradeName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CNPJ = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EntityType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    ContactId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MN_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MN_Customers_SD_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "SD_Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MN_Customers_SD_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "SD_Contacts",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PF_UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserAccountId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    ContactId = table.Column<int>(type: "int", nullable: true),
                    BusinessProfileId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PF_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PF_UserProfiles_MN_businesses_profiles_BusinessProfileId",
                        column: x => x.BusinessProfileId,
                        principalTable: "MN_businesses_profiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PF_UserProfiles_SD_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "SD_Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PF_UserProfiles_SD_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "SD_Contacts",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SD_socialnetworks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SD_socialnetworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SD_socialnetworks_SD_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "SD_Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CustomerCompany",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LinkedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCompany", x => new { x.CustomerId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_CustomerCompany_MN_Companies_profiles_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "MN_Companies_profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerCompany_MN_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "MN_Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCompany_CompanyId",
                table: "CustomerCompany",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MN_Companies_profiles_AddressId",
                table: "MN_Companies_profiles",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_MN_Companies_profiles_BusinessProfileId",
                table: "MN_Companies_profiles",
                column: "BusinessProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_MN_Companies_profiles_ContactId",
                table: "MN_Companies_profiles",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_MN_Customers_AddressId",
                table: "MN_Customers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_MN_Customers_ContactId",
                table: "MN_Customers",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_PF_UserProfiles_AddressId",
                table: "PF_UserProfiles",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_PF_UserProfiles_BusinessProfileId",
                table: "PF_UserProfiles",
                column: "BusinessProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_PF_UserProfiles_ContactId",
                table: "PF_UserProfiles",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_SD_socialnetworks_ContactId",
                table: "SD_socialnetworks",
                column: "ContactId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerCompany");

            migrationBuilder.DropTable(
                name: "PF_UserProfiles");

            migrationBuilder.DropTable(
                name: "SD_socialnetworks");

            migrationBuilder.DropTable(
                name: "MN_Companies_profiles");

            migrationBuilder.DropTable(
                name: "MN_Customers");

            migrationBuilder.DropTable(
                name: "MN_businesses_profiles");

            migrationBuilder.DropTable(
                name: "SD_Addresses");

            migrationBuilder.DropTable(
                name: "SD_Contacts");
        }
    }
}
