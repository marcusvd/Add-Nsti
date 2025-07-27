using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AU_ProfileUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AU_ProfileUsers", x => x.Id);
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
                name: "MN_Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    ContactId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MN_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MN_Companies_SD_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "SD_Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MN_Companies_SD_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "SD_Contacts",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AU_MyUsers",
                columns: table => new
                {
                    UserAccoutnId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    ContactId = table.Column<int>(type: "int", nullable: true),
                    ProfileId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AU_MyUsers", x => x.UserAccoutnId);
                    table.ForeignKey(
                        name: "FK_AU_MyUsers_AU_ProfileUsers_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "AU_ProfileUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AU_MyUsers_MN_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "MN_Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AU_MyUsers_SD_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "SD_Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AU_MyUsers_SD_Contacts_ContactId",
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
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TradeName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CNPJ = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EntityType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    ContactId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MN_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MN_Customers_MN_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "MN_Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "SD_socialnetworks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SD_socialnetworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SD_socialnetworks_MN_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "MN_Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SD_socialnetworks_SD_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "SD_Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AU_MyUsers_AddressId",
                table: "AU_MyUsers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AU_MyUsers_CompanyId",
                table: "AU_MyUsers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AU_MyUsers_ContactId",
                table: "AU_MyUsers",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_AU_MyUsers_ProfileId",
                table: "AU_MyUsers",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_MN_Companies_AddressId",
                table: "MN_Companies",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_MN_Companies_ContactId",
                table: "MN_Companies",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_MN_Customers_AddressId",
                table: "MN_Customers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_MN_Customers_CompanyId",
                table: "MN_Customers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MN_Customers_ContactId",
                table: "MN_Customers",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_SD_socialnetworks_CompanyId",
                table: "SD_socialnetworks",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SD_socialnetworks_ContactId",
                table: "SD_socialnetworks",
                column: "ContactId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AU_MyUsers");

            migrationBuilder.DropTable(
                name: "MN_Customers");

            migrationBuilder.DropTable(
                name: "SD_socialnetworks");

            migrationBuilder.DropTable(
                name: "AU_ProfileUsers");

            migrationBuilder.DropTable(
                name: "MN_Companies");

            migrationBuilder.DropTable(
                name: "SD_Addresses");

            migrationBuilder.DropTable(
                name: "SD_Contacts");
        }
    }
}
